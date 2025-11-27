using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace inzynierka.Models
{
    public class CloudContext : DbContext
    {
        public DbSet<FileModel> Files { get; set; }
        public DbSet<UserModel> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data source=cloud2.sqlite");
        }

    }
}
