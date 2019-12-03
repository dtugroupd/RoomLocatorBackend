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

        public DbSet<Location> Locations { get; set; }
        public DbSet<Section> Sections { get; set; }
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
        
        public DbSet<Event> Events { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<UserDisclaimer> UserDisclaimers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserFluentConfig());
            builder.ApplyConfiguration(new RoleFluentConfig());
            builder.ApplyConfiguration(new UserRoleFluentConfig());
            builder.ApplyConfiguration(new LocationFluentConfig());
            builder.ApplyConfiguration(new SectionFluentConfig());
            builder.ApplyConfiguration(new SurveyFluentConfig());
            builder.ApplyConfiguration(new SurveyAnswerFluentConfig());
            builder.ApplyConfiguration(new QuestionFluentConfig());
            builder.ApplyConfiguration(new EventFluentConfig());

            base.OnModelCreating(builder);
                        
            builder.Entity<Feedback>().HasOne(x => x.User).WithMany(x => x.Feedbacks).HasForeignKey(x => x.UserId);
        }
    }
}