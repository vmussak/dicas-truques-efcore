using DemoEfCore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoEfCore.Data.Configurations
{
    public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("Pessoa");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).HasColumnType("VARCHAR(50)").IsRequired();
            builder.Property(x => x.Excluido).HasDefaultValue(false).IsRequired();

            builder.HasQueryFilter(x => !x.Excluido);

            builder
                .HasIndex(x => x.Nome)
                .HasDatabaseName("IDX_PESSOA_NOME");
        }
    }
}
