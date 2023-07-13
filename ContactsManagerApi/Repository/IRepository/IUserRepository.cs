using ContactsManagerApi.Models;
using ContactsManagerApi.Models.DTO;

namespace ContactsManagerApi.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO>Login(LoginResquestDTO loginResquestDTO);
        Task<LocalUser>Register(RegistrationResquestDTO registrationResquestDTO);
        

    }
}
