using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomLocator.Domain;

namespace RoomLocator.Data.Config
{
    public class SensorFluentConfig : IEntityTypeConfiguration<Sensor>
    {
        public void Configure(EntityTypeBuilder<Sensor> builder)
        {
            builder.HasIndex(x => x.Id).IsUnique();
        }
    }
}