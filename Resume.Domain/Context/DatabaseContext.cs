using Microsoft.EntityFrameworkCore;
using Resume.Domain.Entities.Education;
using Resume.Domain.Entities.Experience;
using Resume.Domain.Entities.Project;
using Resume.Domain.Entities.Skills;
using Resume.Domain.Entities.User;

namespace Resume.Domain.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        #region User

        public DbSet<User> Users { get; set; }

        #endregion

        #region Resume

        #region Education

        public DbSet<Education> Educations { get; set; }

        #endregion

        #region Experience

        public DbSet<Experience> Experiences { get; set; }

        #endregion

        #region Skills

        public DbSet<Skills> Skills { get; set; }

        #endregion

        #endregion

        #region Project

        public DbSet<Project> Projects { get; set; }

        #endregion

        #region On Model Creating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s=>s.GetForeignKeys()))
            {
                
            }

            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
