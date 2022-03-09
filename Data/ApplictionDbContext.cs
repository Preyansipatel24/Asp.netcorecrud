using ASPNETCOREDEMO.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCOREDEMO.Data
{
    public class ApplictionDbContext : DbContext
    {
        public ApplictionDbContext(DbContextOptions<ApplictionDbContext> options) : base(options)
        {

        }

        public DbSet<Category> categories { get; set; }
    }
}
