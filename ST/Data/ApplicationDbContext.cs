using Microsoft.EntityFrameworkCore;
using ST.Models;

namespace ST.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Event> Event { get; set; }
        public DbSet<ImageGallery> ImageGallery { get; set; }
        public DbSet<Sponsor> Sponsor { get; set; }
        public DbSet<CompetitionRegistration> CompetitionRegistration { get; set; }
        public DbSet<About> About { get; set; }
    }
}
