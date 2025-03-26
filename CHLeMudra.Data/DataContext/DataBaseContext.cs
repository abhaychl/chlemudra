


using CHLeMudra.Entity;
using System.Data.Entity;

namespace CHLeMudra.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext() : base("name=connectionstring")
        { 
        
        }
        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
          
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<DocumentSignHistory> DocumentSignHistory { get; set; }
        public DbSet<DocumentAssignee> DocumentAssigneeHistory { get; set; }
        public DbSet<Usermaster> UserMasters { get; set; }
        public DbSet<TokenManagement> TokenManagements { get; set; }


    }
}
