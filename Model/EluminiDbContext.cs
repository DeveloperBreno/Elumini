using Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Model
{
    public class EluminiDbContext : DbContext
    {

        public DbSet<Job>? Jobs { get; set; } 
        public DbSet<Parameters>? Parameters { get; set; }
        public DbSet<Text>? Texts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ConnectionString"));
        }

    }
}
