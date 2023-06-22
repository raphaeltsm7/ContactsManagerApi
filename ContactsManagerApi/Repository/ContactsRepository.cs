using ContactsManagerApi.Data;
using ContactsManagerApi.Models;
using ContactsManagerApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Numerics;

namespace ContactsManagerApi.Repository
{
    public class ContactsRepository : Repository<Contact>, IContactsRepository { 

        private readonly ContactsDbContext _db;

    public ContactsRepository(ContactsDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<Contact> UpdateAsync(Contact contacts)
    {
        _db.Contact.Update(contacts);
        await _db.SaveChangesAsync();
        return contacts;
    }
}
}
