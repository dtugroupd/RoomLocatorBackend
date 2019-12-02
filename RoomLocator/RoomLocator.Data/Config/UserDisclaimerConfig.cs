using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomLocator.Domain.Models;

namespace RoomLocator.Data.Config
{
    public class UserDisclaimerConfig : IEntityTypeConfiguration<UserDisclaimer>
    {
        public void Configure(EntityTypeBuilder<UserDisclaimer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.UserId).IsUnique();
            builder
                .HasOne(x => x.User)
                .WithOne(x => x.UserDisclaimer)
                .HasForeignKey<UserDisclaimer>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
