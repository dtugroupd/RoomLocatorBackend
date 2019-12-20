using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomLocator.Domain.Models;

namespace RoomLocator.Data.Config
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    /// </summary>
    public class SurveyFluentConfig : IEntityTypeConfiguration<Survey>
    {
        public void Configure(EntityTypeBuilder<Survey> builder)
        {
            builder
                .HasMany(x => x.Sections)
                .WithOne(x => x.Survey);

            builder
                .HasMany(x => x.Questions)
                .WithOne(x => x.Survey)
                .HasForeignKey(x => x.SurveyId);

            builder
                .HasMany(x => x.SurveyAnswers)
                .WithOne(x => x.Survey)
                .HasForeignKey(x => x.SurveyId);
        }
    }
}