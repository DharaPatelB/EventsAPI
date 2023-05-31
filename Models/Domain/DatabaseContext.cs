using Microsoft.EntityFrameworkCore;

namespace EventsAPI.Models.Domain
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
        {

        }
        public DbSet<Event> Event { get; set; }


    }
}
