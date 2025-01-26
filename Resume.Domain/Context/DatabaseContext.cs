using Microsoft.EntityFrameworkCore;
using Resume.Domain.Entities.Education;
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
