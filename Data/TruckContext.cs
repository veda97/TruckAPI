using Microsoft.EntityFrameworkCore;
using TruckInfoApi.Controllers.Models;


namespace TruckInfoApi.Data
{
    public class TruckContext : DbContext  // ✅ Make sure it inherits from DbContext
    {
        public TruckContext(DbContextOptions<TruckContext> options) : base(options)
        {
        }

        public DbSet<Truck> Trucks { get; set; }  // ✅ DbSet for your Truck table
    }
}
