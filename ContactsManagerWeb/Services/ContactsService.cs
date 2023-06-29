using ContactsManagerUtility;
using ContactsManagerWeb.Models;
using ContactsManagerWeb.Models.DTO;
using ContactsManagerWeb.Services.IServices;
using ContactsManagerWeb.Utils;
using Microsoft.Extensions.Options;

namespace ContactsManagerWeb.Services
{
    public class ContactsService : BaseService, IContactsService
    {

        private readonly IHttpClientFactory _clientFactory;
        private readonly ServicesUrlsConfig _servicesUrls;
        public ContactsService(IHttpClientFactory clientFactory, IOptions<ServicesUrlsConfig> servicesUrls) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            _servicesUrls = servicesUrls.Value;
        }

        public Task<T> CreateAsync<T>(ContactsCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{_servicesUrls.ContactsAPI}/ContactsAPI/"
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{_servicesUrls.ContactsAPI}/ContactsAPI/{id}"
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{_servicesUrls.ContactsAPI}/ContactsAPI/"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{_servicesUrls.ContactsAPI}/ContactsAPI/{id}"
            });
        }

        public Task<T> UpdateAsync<T>(ContactsUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{_servicesUrls.ContactsAPI}/ContactsAPI/{dto.Id}"
            });
        }
    }
}
