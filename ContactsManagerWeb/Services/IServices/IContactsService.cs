using ContactsManagerWeb.Models.DTO;

namespace ContactsManagerWeb.Services.IServices
{
    public interface IContactsService
    {

        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(ContactsCreateDTO dto);
        Task<T> UpdateAsync<T>(ContactsUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
