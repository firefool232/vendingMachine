using Microsoft.EntityFrameworkCore;
using System;
using VendingMachine.Models;

namespace VendingMachine.EFModels
{
    public class ApplicationContext : DbContext
    {
        public DbSet<VendingMachine.Models.Drink> Drinks { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\App_Data\database.mdf';Integrated Security=True;Connect Timeout=30";
            optionsBuilder.UseSqlServer(conn);
        }
    }
}
