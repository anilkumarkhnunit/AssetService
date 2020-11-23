using AssetService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssetRepository.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public virtual DbSet<Asset> Assets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Asset>().HasData(
                new Asset
                {
                    AssetId = 1,
                    Name = "Asset 1",
                    IsCash = true
                },
                new Asset
                {
                    AssetId = 2,
                    Name = "Asset 2",
                    IsCash = true
                },
                new Asset
                {
                    AssetId = 3,
                    Name = "Asset 3",
                    IsConvertible = true
                },
                  new Asset
                  {
                      AssetId = 4,
                      Name = "Asset 4",
                      IsfixIncome = true
                  },
                    new Asset
                    {
                        AssetId = 5,
                        Name = "Asset 5",
                        IsConvertible = true
                    }
        );

        }
    }
}
