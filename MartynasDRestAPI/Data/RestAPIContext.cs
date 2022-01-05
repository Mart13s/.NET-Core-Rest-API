using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MartynasDRestAPI.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MartynasDRestAPI.Data.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using MartynasDRestAPI.Auth.Model;

namespace MartynasDRestAPI.Data
{
    public class RestAPIContext : IdentityDbContext<RestUser, IdentityRole<int>, int>
    {
        public DbSet<RestUser> users { get; set; }
        //public DbSet<UserInternal> users { get; set; }
        public DbSet<InventoryItem> inventoryItems { get; set; }
        public DbSet<StoreItem> storeItems { get; set; }
        public DbSet<Purchase> purchases { get; set; }
        public DbSet<Review> reviews { get; set; }
        public DbSet<Trade> trades { get; set; }
        public DbSet<PurchaseItem> purchaseItems { get; set; }
        public DbSet<TradeItem> tradeItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=tcp:martynasdrestapidbserver.database.windows.net,1433;Initial Catalog=MartynasDRestAPI_db;User Id=AdminUser@martynasdrestapidbserver;Password=");
            optionsBuilder.UseSqlServer("Data Source=(localDB)\\MSSQLLOCALDB; Initial Catalog=MartynasDApiDatabase");
        }

        // Overriding for composite keys
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PurchaseItem>().HasKey(o => new { o.purchaseID, o.storeItemID });
            modelBuilder.Entity<TradeItem>().HasKey(o => new { o.tradeID, o.itemID });

        }
    }
}
