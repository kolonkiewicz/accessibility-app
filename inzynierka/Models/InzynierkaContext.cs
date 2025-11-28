using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace inzynierka.Models
{
    public class InzynierkaContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }

        public DbSet<ViolationModel> Vialations { get; set; }

        public DbSet<ScanModel> Scan { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data source=inzynierka.sqlite");
        }

    }
}
