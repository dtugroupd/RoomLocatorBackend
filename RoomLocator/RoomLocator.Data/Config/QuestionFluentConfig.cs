using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomLocator.Domain.Models;

namespace RoomLocator.Data.Config
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    /// </summary>
    public class QuestionFluentConfig : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder
                .HasMany(x => x.QuestionAnswers)
                .WithOne(x => x.Question)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}