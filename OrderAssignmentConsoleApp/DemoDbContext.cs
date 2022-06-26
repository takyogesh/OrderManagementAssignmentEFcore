using System;
using Microsoft.EntityFrameworkCore;
using OrderAssignmentConsoleApp.Entities;

namespace OrderAssignmentConsoleApp
{
    public class DemoDbContext :DbContext
    {
        public DemoDbContext() {
        }
        public DbSet<ItemMaster> itemMasters { get; set; }
        public DbSet<Customers> customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-00LQG0A;Database=OrderAssgnEF;Trusted_Connection=True;");
        }
    }
}
