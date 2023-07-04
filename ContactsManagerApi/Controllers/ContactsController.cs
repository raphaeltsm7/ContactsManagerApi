using AutoMapper;
using ContactsManagerApi.Models;
using ContactsManagerApi.Models.DTO;
using ContactsManagerApi.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManagerApi.Controllers {
    [Route("/ContactsAPI")]
    public class ContactsController : ControllerBase {

        private readonly APIResponse _response;
        private readonly IContactsRepository _dbContacts;
        private readonly ILogger<ContactsController> _logger;
        private readonly IMapper _mapper;

        public ContactsController(ILogger<ContactsController> logger, IContactsRepository db, IMapper mapper) {
            _dbContacts = db;
            _logger = logger;
            _mapper = mapper;
            this._response = new APIResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetContacts() {
            try {
                IEnumerable<Contact> ContactsList = await _dbContacts.GetAllAsync();
                _response.Result = _mapper.Map<List<ContactsDTO>>(ContactsList);
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex) {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetContacts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetContacts(int id) {
            try {
                if (id == 0) {
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string> { "O parâmetro de Id do contato não foi passado corretamente." };
                    return BadRequest(_response);
                }

                var contact = await _dbContacts.GetAsync(contact => contact.Id == id);

                if (contact == null) {
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    _response.ErrorMessages = new List<string> { $"O contato com o Id {id} não foi encontrado." };
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<ContactsDTO>(contact);
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex) {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
            }
            return _response;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> Createcontacts([FromBody] ContactsCreateDTO ContactsDTO) {
            try {
                if (ContactsDTO == null) {
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                if (!ModelState.IsValid) {
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.ErrorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                    return BadRequest(_response);
                }

                if (await _dbContacts.GetAsync(p => p.Name.ToLower() == ContactsDTO.Name.ToLower()) != null) {
                    var message = "Este contato já foi registrado!";
                    ModelState.AddModelError("Erro:", message);
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string> { message };
                    return BadRequest(_response);
                }

                var contact = _mapper.Map<Contact>(ContactsDTO);

                await _dbContacts.CreateAsync(contact);

                _logger.LogInformation("Novo contato adicionado.");

                _response.Result = _mapper.Map<ContactsDTO>(contact);
                _response.StatusCode = System.Net.HttpStatusCode.Created;
                return CreatedAtRoute("GetContacts", new { id = contact.Id }, _response);
            }
            catch (Exception ex) {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
                return _response;
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> Deletecontacts(int id) {
            try {
                if (id <= 0) {
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var contacts = await _dbContacts.GetAsync(p => p.Id == id);

                if (contacts == null) {
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _dbContacts.RemoveAsync(contacts);
                _logger.LogInformation($"contato de Id {id} foi removido.");
                _response.StatusCode = System.Net.HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex) {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
                return _response;
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> Updatecontacts(int id, [FromBody] ContactsUpdateDTO ContactsDTO) {
            try {
                if (ContactsDTO == null || id != ContactsDTO.Id) {
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var contacts = await _dbContacts.GetAsync(p => p.Id == id);

                if (contacts == null) {
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string> { $"O contato com o Id {id} não foi encontrado." };
                    return BadRequest(_response);
                }

                _mapper.Map(ContactsDTO, contacts);

                await _dbContacts.UpdateAsync(contacts);

                _logger.LogInformation($"contato de Id {id} foi atualizado.");
                _response.StatusCode = System.Net.HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex) {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
            }
            return _response;
        }
    }
}
