using ContactsManagerApi.Models;
using System.Linq.Expressions;

namespace ContactsManagerApi.Repository.IRepository {
    public interface IContactsRepository :  IRepository<Contact> {

        Task<Contact> UpdateAsync(Contact contacts);

    }
}
