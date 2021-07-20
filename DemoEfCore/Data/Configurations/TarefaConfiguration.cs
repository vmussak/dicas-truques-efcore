using DemoEfCore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEfCore.Data.Configurations
{
    public class TarefaConfiguration : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.ToTable("Tarefa");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Descricao).HasColumnType("VARCHAR(150)").IsRequired();
            builder.Property(x => x.Concluida).HasDefaultValue(false).IsRequired();

            builder.HasOne(x => x.Pessoa)
                .WithMany(x => x.Tarefas)
                .HasForeignKey(x => x.IdPessoa);
        }
    }
}
