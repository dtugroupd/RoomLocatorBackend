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
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}