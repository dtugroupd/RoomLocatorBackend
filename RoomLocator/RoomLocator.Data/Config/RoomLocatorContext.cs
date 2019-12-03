using Microsoft.EntityFrameworkCore;
using RoomLocator.Domain;
using RoomLocator.Domain.Models;

namespace RoomLocator.Data.Config
{
    /// <summary>
    ///     This is the database integration
    ///     <author>Anders Wiberg Olsen, s165241, main structure</author>
    ///     <author>Most members working on backend have contributed</author>
    /// </summary>
    public class RoomLocatorContext : DbContext
    {
        public RoomLocatorContext(DbContextOptions options) : base(options) { }
        
        public DbSet<MazeMapSection> MazeMapSections { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<Value> Values { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Coordinates> Coordinates { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserFluentConfig());
            builder.ApplyConfiguration(new RoleFluentConfig());
            builder.ApplyConfiguration(new UserRoleFluentConfig());
            builder.ApplyConfiguration(new SensorFluentConfig());
            
            
            base.OnModelCreating(builder);
            
            //builder.ApplyConfiguration(new MazeMapSectionWithSensorsFluentConfig());

            builder.Entity<MazeMapSection>().HasMany(x => x.Coordinates).WithOne(x => x.MazeMapSection).HasForeignKey(x => x.MazeMapSectionId);
            builder.Entity<Survey>().HasMany(x => x.MazeMapSections).WithOne(x => x.Survey).IsRequired(required: false);
            builder.Entity<Survey>().HasMany(x => x.Questions).WithOne(x => x.Survey).HasForeignKey(x => x.SurveyId);
            builder.Entity<Survey>().HasMany(x => x.SurveyAnswers).WithOne(x => x.Survey).HasForeignKey(x => x.SurveyId);
            builder.Entity<SurveyAnswer>().HasMany(x => x.QuestionAnswers).WithOne(x => x.SurveyAnswer);
            builder.Entity<Question>().HasMany(x => x.QuestionAnswers).WithOne(x => x.Question).OnDelete(DeleteBehavior.Restrict);
        }
    }
}