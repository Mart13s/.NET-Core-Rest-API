using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MartynasDRestAPI.Data.Entities;

namespace MartynasDRestAPI.Data
{
    public class RestAPIContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<InventoryItem> inventoryItems { get; set; }
        public DbSet<StoreItem> storeItems { get; set; }
        public DbSet<Purchase> purchases { get; set; }
        public DbSet<Review> reviews { get; set; }
        public DbSet<Trade> trades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MartynasDRestAPI");
        }
    }
}
