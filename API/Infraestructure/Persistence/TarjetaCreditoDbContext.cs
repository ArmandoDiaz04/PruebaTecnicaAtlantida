using System;
using System.Collections.Generic;
using API.Core.Domain;
using API.Core.Domain.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Infraestructure.Persistence;

public partial class TarjetaCreditoDbContext : DbContext
{
    public TarjetaCreditoDbContext()
    {
    }

    public TarjetaCreditoDbContext(DbContextOptions<TarjetaCreditoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MovimientosTarjeta> MovimientosTarjeta { get; set; }

    public virtual DbSet<TarjetasCredito> TarjetasCreditos { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Server=localhost;Database=TarjetaCreditoDB;User Id=sa;Password=admin;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovimientosTarjeta>(entity =>
        {
            entity.HasKey(e => e.id);

            entity.Property(e => e.creadoPor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("creadoPor");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.fechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.fechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.FechaMovimiento).HasColumnType("datetime");
            entity.Property(e => e.modificadoPor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("modificadoPor");
            entity.Property(e => e.Monto).HasColumnType("money");
            entity.Property(e => e.status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("status");
            entity.Property(e => e.TipoMovimiento)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.IdTarjetaNavigation).WithMany(p => p.MovimientosTarjeta)
                .HasForeignKey(d => d.IdTarjeta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovimientosTarjeta_TarjetasCredito");
        });

        modelBuilder.Entity<TarjetasCredito>(entity =>
        {
            entity.HasKey(e => e.id);

            entity.ToTable("TarjetasCredito");

            entity.Property(e => e.creadoPor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("creadoPor");
            entity.Property(e => e.fechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.fechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.modificadoPor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("modificadoPor");
            entity.Property(e => e.MontoOtorgado).HasColumnType("money");
            entity.Property(e => e.NombreTitular)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroTarjeta)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PorceInteres).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.PorceInteresMinimo).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("status");
        });

        OnModelCreatingPartial(modelBuilder);
    }


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var idUserCurrent = "ArmandoDiaz";
        foreach (var entry in ChangeTracker.Entries<BaseDomain>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.fechaCreacion = DateTime.Now;
                    entry.Entity.creadoPor = idUserCurrent;
                    break;
                case EntityState.Modified:
                    entry.Entity.fechaModificacion = DateTime.Now;
                    entry.Entity.modificadoPor = idUserCurrent;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
