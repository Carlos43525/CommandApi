using CommandApi.Models;
using Microsoft.EntityFrameworkCore; 

namespace CommandApi.App.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Command> Commands { get; set; }
    }
}
