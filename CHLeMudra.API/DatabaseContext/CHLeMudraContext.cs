
using CHLeMudra.Api.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fairgaze.Data.DatabaseContext
{
    public class CHLeMudraContext : DbContext
    {
        
        public CHLeMudraContext(DbContextOptions<CHLeMudraContext> options) : base(options)
        {

        }
        public DbSet<DocumentSignHistory> DocumentSignHistories { get; set; }
        public DbSet<DocumentAssignee> DocumentAssignees { get; set; }
        public DbSet<Usermaster> Usermasters { get; set; }
        public DbSet<TokenManagement> TokenManagements { get; set; }


    }
}
