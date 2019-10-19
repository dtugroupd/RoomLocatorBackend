using Microsoft.EntityFrameworkCore;
using RoomLocator.Domain;

namespace RoomLocator.Data.Config
{
    public class RoomLocatorContext : DbContext
    {
        public RoomLocatorContext(DbContextOptions options) : base(options) { }
        
        public DbSet<Value> Values { get; set; }
        public DbSet<Sensor> Sensors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SensorFluentConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}