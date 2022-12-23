using Microsoft.EntityFrameworkCore;
using SurveyApp.Models.Entities;

namespace SurveyApp.Data
{
    public class SurveyDbContext : DbContext
    {
        public SurveyDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Choice> Choices { get; set; }
    }
}
