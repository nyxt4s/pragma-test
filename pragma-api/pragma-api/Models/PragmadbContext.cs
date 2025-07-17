using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace pragma_api.Models;

public partial class PragmadbContext : DbContext
{
    public PragmadbContext()
    {
    }

    public PragmadbContext(DbContextOptions<PragmadbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Usuario> Usuarios { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC07726B395B");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Rut, "UQ__Usuario__CAF0366054A18796").IsUnique();

            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Rut).HasMaxLength(12);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
