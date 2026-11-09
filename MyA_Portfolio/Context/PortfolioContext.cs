using Microsoft.EntityFrameworkCore;
using MyA_Portfolio.Entities;

namespace MyA_Portfolio.Context
{
    public class PortfolioContext:DbContext
    {
        public PortfolioContext(DbContextOptions<PortfolioContext> options) : base(options) 
        {
        }
      
        public DbSet<About> Abouts { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<HeroSection> HeroSections { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SkillGroup> SkillGroups { get; set; }
        public DbSet<Setting> Settings { get; set; }
    }
}
