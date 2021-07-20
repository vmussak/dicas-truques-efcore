using DemoEfCore.Data.Configurations;
using DemoEfCore.Geradores;
using DemoEfCore.Interceptador;
using DemoEfCore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using System;

namespace DemoEfCore.Data
{
    public class DemoContext : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Cidade> Cidades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connectionString = "Data source=localhost\\SQLEXPRESS; Initial Catalog=DemoEfCore;Integrated Security=true;pooling=true;";
            optionsBuilder
                .UseSqlServer(connectionString,
                    x => x.EnableRetryOnFailure(4, TimeSpan.FromSeconds(10), null)
                );
                //.EnableSensitiveDataLogging()
                //.UseLazyLoadingProxies()
                //.LogTo(Console.WriteLine, LogLevel.Information);
                //.AddInterceptors(new ColocarNolock());
                //.ReplaceService<IQuerySqlGeneratorFactory, MeuGeradorDeSqlFactory>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PessoaConfiguration());
            modelBuilder.ApplyConfiguration(new TarefaConfiguration());
            modelBuilder.ApplyConfiguration(new CidadeConfiguration());

            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(DemoContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}