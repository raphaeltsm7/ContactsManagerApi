using AutoMapper;
using ContactsManagerWeb.Models;
using ContactsManagerWeb.Models.DTO;
using ContactsManagerWeb.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ContactsManagerWeb.Controllers {
    public class ContactsController : Controller {

        private readonly IContactsService _contactsService;
        private readonly IMapper _mapper;

        public ContactsController(IContactsService contactsService, IMapper mapper) {
            _contactsService = contactsService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexContacts() {

            List<ContactsDTO> list = new();

            var response = await _contactsService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess) {
                list = JsonConvert.DeserializeObject<List<ContactsDTO>>(Convert.ToString(response.Result));
            }
            
            return View(list);
        }
    }
}
