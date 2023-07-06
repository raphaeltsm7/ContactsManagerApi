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
            if (response.IsValid())
            {
                listContacts = JsonConvert.DeserializeObject<List<ContactsDTO>>(Convert.ToString(response.Result));
            }
            return View(listContacts);
        }

        public async Task<IActionResult> DeleteContact(int contactId)
        {
            var response = await _contactsService.GetAsync<APIResponse>(contactId);
            if (response.IsValid())
            {
                ContactsDTO model = JsonConvert.DeserializeObject<ContactsDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<ContactsDTO>(model));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteContact(ContactsDTO model)
        {
            var response = await _contactsService.DeleteAsync<APIResponse>(model.Id);
            if (response.IsValid())
            {
                TempData["success"] = "Contact was deleted successfully!";
                return RedirectToAction(nameof(IndexContacts));
            }
            TempData["error"] = "Contact was not deleted!";
            return View(model);
        }

        public async Task<IActionResult> UpdateContact(int contactId)
        {
            var response = await _contactsService.GetAsync<APIResponse>(contactId);
            if (response.IsValid())
            {
                ContactsDTO model = JsonConvert.DeserializeObject<ContactsDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<ContactsUpdateDTO>(model));
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateContact(ContactsUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _contactsService.UpdateAsync<APIResponse>(model);
                if (response.IsValid())
                {
                    TempData["success"] = "Contact was edited successfully!";
                    return RedirectToAction(nameof(IndexContacts));
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

            return View(model);
        }

        public async Task<IActionResult> CreateContact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateContact(ContactsCreateDTO contact)
        {
            if (ModelState.IsValid)
            {
                var response = await _contactsService.CreateAsync<APIResponse>(contact);
                if (response.IsValid())
                {
                    TempData["success"] = "Contact was created successfully!";
                    return RedirectToAction(nameof(IndexContacts));
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