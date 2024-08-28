using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entidades;

namespace Data.Configuraciones
{
    public class MedicoConfiguracion : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Apellidos).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Nombres).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Direccion).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Telefono).HasMaxLength(40);
            builder.Property(x => x.Genero).IsRequired().HasColumnType("char").HasMaxLength(1);
            builder.Property(x => x.EspecialidadId).IsRequired();

            builder.HasOne(x => x.Especialidad)
                .WithMany()
                .HasForeignKey(x => x.EspecialidadId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}