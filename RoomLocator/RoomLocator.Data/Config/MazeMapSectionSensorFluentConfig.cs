using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomLocator.Domain.Models;

namespace RoomLocator.Data.Config
{
    public class MazeMapSectionWithSensorsFluentConfig : IEntityTypeConfiguration<MazeMapSectionSensor>
    {
        public void Configure(EntityTypeBuilder<MazeMapSectionSensor> builder)
        {
            /*
            builder.HasKey(x => new {x.SensorId, x.SectionId});
            builder
                .HasOne(x => x.Section)
                .WithMany()
                .HasForeignKey(x => x.SectionId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasOne(x => x.Sensors)
                .WithMany()
                .HasForeignKey(x => x.SensorId)
                .OnDelete(DeleteBehavior.Cascade);
            */
        }
    }
}