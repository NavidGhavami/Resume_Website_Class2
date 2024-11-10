using Microsoft.EntityFrameworkCore;
using Resume.Domain.Entities.User;

namespace Resume.Domain.Context
{
    public class DatabaseContext : DbContext
    {

	    #region Constructor

	    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

		#endregion

		#region User

		public DbSet<User> Users { get; set; }

		#endregion


		#region On Model Creating

		protected override void OnModelCreating(ModelBuilder modelBuilder)
	    {
		    foreach (var relation in modelBuilder.Model.GetEntityTypes().SelectMany(x=>x.GetForeignKeys()))
		    {
			    relation.DeleteBehavior = DeleteBehavior.Cascade;
		    }
		    base.OnModelCreating(modelBuilder);
	    }

	    #endregion

    }
}
