using Microsoft.EntityFrameworkCore;

namespace OnlineStore.Models
{
    public class DbOnlineStore : DbContext
    {
        public DbOnlineStore(DbContextOptions<DbOnlineStore> options):base(options) { }
        
        public DbSet<Book> Books { get; set; }
    }

}
