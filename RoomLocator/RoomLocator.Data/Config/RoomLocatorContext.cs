using Microsoft.EntityFrameworkCore;
using RoomLocator.Domain;
using RoomLocator.Domain.Models;

namespace RoomLocator.Data.Config
{
    public class RoomLocatorContext : DbContext
    {
        public RoomLocatorContext(DbContextOptions options) : base(options) { }
        
        public DbSet<MazeMapSection> MazeMapSections { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<Value> Values { get; set; }
        public DbSet<Coordinates> Coordinates { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MazeMapSection>().HasMany(x => x.Coordinates).WithOne(x => x.MazeMapSection).HasForeignKey(x => x.MazeMapSectionId);
            builder.Entity<Survey>().HasMany(x => x.MazeMapSections).WithOne(x => x.Survey).IsRequired(required: false);
            builder.Entity<Survey>().HasMany(x => x.Questions).WithOne(x => x.Survey).HasForeignKey(x => x.SurveyId);
            builder.Entity<Survey>().HasOne(x => x.SurveyAnswer).WithOne(x => x.Survey).HasForeignKey<SurveyAnswer>(x => x.SurveyId);
            builder.Entity<SurveyAnswer>().HasMany(x => x.QuestionAnswers).WithOne(x => x.SurveyAnswer).HasForeignKey(x => x.SurveyAnswerId);
            builder.Entity<Question>().HasOne(x => x.QuestionAnswer).WithOne(x => x.Question).HasForeignKey<QuestionAnswer>(x => x.QuestionId);
        }
    }
}