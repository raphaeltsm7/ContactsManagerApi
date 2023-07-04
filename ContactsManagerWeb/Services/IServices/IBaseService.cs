using ContactsManagerWeb.Models;
using static ContactsManagerUtility.SD;
using static ContactsManagerWeb.Models.APIRequest;

namespace ContactsManagerWeb.Services.IServices {
    public interface IBaseService {
        APIResponse responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
        Task<T> ConsumeAPI<T>(ApiType method, string url, object data, APIParams headers = null);

    }
}
