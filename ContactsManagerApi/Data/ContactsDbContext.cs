using ContactsManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsManagerApi.Data {
    public class ContactsDbContext : DbContext {
        

        public ContactsDbContext(DbContextOptions<ContactsDbContext> options) : base(options) {
        }

        public DbSet<LocalUser> LocalUsers { get; set; }

        public DbSet<Contact> Contacts { get; set; }
    }
}
