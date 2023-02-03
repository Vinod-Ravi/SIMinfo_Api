using Microsoft.EntityFrameworkCore;
using SIMinfo.API.Models;

namespace SIMinfo.API.DataAccessLayer
{
    public class SimInfoDbContext : DbContext
    {
        public SimInfoDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<SimInformation> SimInformation { get; set; }
        public DbSet<MobileCountryCode> MobileCountryCode { get; set; }
    }
}
