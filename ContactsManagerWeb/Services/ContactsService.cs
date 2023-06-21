using ContactsManagerUtility;
using ContactsManagerWeb.Models;
using ContactsManagerWeb.Models.DTO;
using ContactsManagerWeb.Services.IServices;

namespace ContactsManagerWeb.Services
{
    public class ContactsService : BaseService, IContactsService
    {

        private readonly IHttpClientFactory _clientFactory;
        private string contactsUrl;
        public ContactsService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            contactsUrl = configuration.GetValue<string>("ServiceUrls:ContactsManagerAPI");
        }

        public Task<T> CreateAsync<T>(ContactsCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = contactsUrl + "/api/ContactsAPI"
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = contactsUrl + "/api/ContactsAPI/" + id
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = contactsUrl + "/api/ContactsAPI"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = contactsUrl + "/api/ContactsAPI/" + id
            });
        }

        public Task<T> UpdateAsync<T>(ContactsUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = contactsUrl + "/api/ContactsAPI/" + dto.Id
            });
        }
    }
}
