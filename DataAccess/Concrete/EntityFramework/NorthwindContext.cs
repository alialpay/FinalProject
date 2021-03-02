using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    // Context : Db tabloları ile proje classlarını bağlamak
    public class NorthwindContext:DbContext
    {
        // burası projenin hangi veritabanıyla ilişkili olduğunu belirteceğimiz kısım "Configuring"
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {                               // başına @ konduğunda tesslashı slash olarak algıla demek
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Northwind;Trusted_Connection=true"); //bura önemli 8.gün videosu
        }
        // hangi tablo neye karşılık gelecek?
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

    }
}
