using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomLocator.Domain.Models;

namespace RoomLocator.Data.Config
{
    /// <summary>
    ///     <author>Anders Wiberg Olsen, s165241</author>
    /// </summary>
    public class RoleFluentConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}