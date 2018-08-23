using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ApplicationContext : DbContext
    {
        public DbSet<SearchConnector> SearchConnector { get; set; }

        public DbSet<SearchResultPosition> SearchResultPosition { get; set; }

        public ApplicationContext() : base("MuranoTestDb")
        {
            Database.SetInitializer(new ApplicationContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
