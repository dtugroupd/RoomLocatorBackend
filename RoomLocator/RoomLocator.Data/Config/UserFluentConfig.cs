using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomLocator.Domain;

namespace RoomLocator.Data.Config
{
    /// <summary>
    ///     <author>Anders Wiberg Olsen, s165241</author>
    /// </summary>
    public class UserFluentConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(x => x.StudentId).IsUnique();
        }
    }
}