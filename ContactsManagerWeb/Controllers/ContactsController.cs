using AutoMapper;
using ContactsManagerWeb.Extensions;
using ContactsManagerWeb.Models;
using ContactsManagerWeb.Models.DTO;
using ContactsManagerWeb.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ContactsManagerWeb.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactsService _contactsService;
        private readonly IMapper _mapper;

        public ContactsController(IContactsService contactsService, IMapper mapper)
        {
            _contactsService = contactsService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexContacts()
        {
            var listContacts = new List<ContactsDTO>();
            var response = await _contactsService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                listContacts = JsonConvert.DeserializeObject<List<ContactsDTO>>(Convert.ToString(response.Result));
            }
            return View(listContacts);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var response = await _contactsService.DeleteAsync<APIResponse>(id);

            TempData["success"] = "Contact was deleted!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditContact(int id)
        {
            var response = await _contactsService.GetAsync<APIResponse>(id);
            if (response.IsValid())
            {
                var contact = JsonConvert.DeserializeObject<ContactsDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<ContactsUpdateDTO>(contact));
            }

            return NotFound();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> EditContact(ContactsUpdateDTO contact)
        {
            if (ModelState.IsValid)
            {
                var response = await _contactsService.UpdateAsync<APIResponse>(contact);
                if (response.IsValid())
                {
                    TempData["success"] = "Contact was edited successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (response.ErrorMessage.Count > 0)
                    {
                        TempData["error"] = "Contact was not edited!";
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessage.FirstOrDefault());
                    }
                }
            }

            return View(contact);
        }

        public async Task<IActionResult> CreateContact()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateContact(ContactsCreateDTO contact)
        {
            if (ModelState.IsValid)
            {
                var response = await _contactsService.CreateAsync<APIResponse>(contact);
                if (response.IsValid())
                {
                    TempData["success"] = "Contact was created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (response.ErrorMessage.Count > 0)
                    {
                        TempData["error"] = "Contact was not created!";
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessage.FirstOrDefault());
                    }
                }
            }

            return View(contact);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}