using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomLocator.Domain;
using RoomLocator.Domain.Models;

namespace RoomLocator.Data.Config
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    /// </summary>
    public class LocationFluentConfig : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder
                .HasMany(x => x.Sections)
                .WithOne(x => x.Location)
                .HasForeignKey(x => x.LocationId);

            builder
                .HasMany(x => x.Coordinates)
                .WithOne(x => x.Location)
                .HasForeignKey(x => x.LocationId);
        }
    }
}