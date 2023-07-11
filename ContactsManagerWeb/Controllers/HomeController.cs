using AutoMapper;
using ContactsManagerWeb.Extensions;
using ContactsManagerWeb.Models;
using ContactsManagerWeb.Models.DTO;
using ContactsManagerWeb.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ContactsManagerWeb.Controllers {
    public class HomeController : Controller {
        private readonly IContactsService _contactsService;
        private readonly IMapper _mapper;

        public HomeController(IContactsService contactsService, IMapper mapper)
        {
            _contactsService = contactsService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var listContacts = new List<ContactsDTO>();
            var response = await _contactsService.GetAllAsync<APIResponse>();
            if (response.IsValid())
            {
                listContacts = JsonConvert.DeserializeObject<List<ContactsDTO>>(Convert.ToString(response.Result));
            }
            return View(listContacts);
        }
        
        }
    }
