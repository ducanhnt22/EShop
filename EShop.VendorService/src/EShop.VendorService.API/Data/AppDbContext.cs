using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.VendorService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EShop.VendorService.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreAddress> StoreAddresses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}