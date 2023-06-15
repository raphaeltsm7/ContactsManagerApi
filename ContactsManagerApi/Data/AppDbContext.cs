using ContactsManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsManagerApi.Data {
    public class AppDbContext : DbContext {
        public DbSet<Contact> Contacts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
        }
    }
}
