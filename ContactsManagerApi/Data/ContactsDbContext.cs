using ContactsManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsManagerApi.Data {
    public class ContactsDbContext : DbContext {
        public DbSet<Contact> Contacts { get; set; }

        public ContactsDbContext(DbContextOptions<ContactsDbContext> options) : base(options) {
        }


    }
}
