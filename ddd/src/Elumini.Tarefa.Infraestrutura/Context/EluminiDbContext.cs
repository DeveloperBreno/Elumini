using Elumini.Tarefa.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace Elumini.Tarefa.Infraestrutura.Context
{
    public interface IEluminiDbContext
    {
        DbSet<Domain.Entites.Tarefa> Tarefas { get; set; }
        DbSet<Parametro> Parametros { get; set; }
        DbSet<Observacao> Observacaos { get; set; }
    }
    public class EluminiDbContext : DbContext, IEluminiDbContext
    {
        public DbSet<Domain.Entites.Tarefa> Tarefas { get; set; }
        public DbSet<Parametro> Parametros { get; set; }
        public DbSet<Observacao> Observacaos { get; set; }

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
