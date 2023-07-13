using ContactsManagerApi.Models.DTO;
using ContactsManagerApi.Models;
using ContactsManagerApi.Repository.IRepository;
using ContactsManagerApi.Data;

namespace ContactsManagerApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ContactsDbContext _db;
        public UserRepository(ContactsDbContext db)
        {
            _db = db;
        }

        public bool IsUniqueUser(string username)
        {
            var  user =  _db.LocalUsers.FirstOrDefault(x => x.UserName == username);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginResquestDTO loginResquestDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<LocalUser> Register(RegistrationResquestDTO registrationResquestDTO)
        {
            LocalUser user = new LocalUser()
            {
                UserName = registrationResquestDTO.UserName,
                Password = registrationResquestDTO.Password,
                Name = registrationResquestDTO.Name,
                Role = registrationResquestDTO.Role
            };
            _db.LocalUsers.Add(user);
            await _db.SaveChangesAsync();
            user.Password = "";
            return user;
        }
    }
}

