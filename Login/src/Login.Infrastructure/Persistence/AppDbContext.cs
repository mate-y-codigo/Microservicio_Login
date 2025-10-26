using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Login.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Login.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ====== Usuario ======
            modelBuilder.Entity<Usuario>(static entity =>
            {
                entity.ToTable("Usuario");
                entity.HasKey(u => u.Id);


                entity.Property(u => u.email).HasMaxLength(50).IsRequired();
                entity.Property(u => u.password).HasMaxLength(100).IsRequired();
                entity.Property(u => u.nombre).HasMaxLength(50);
                entity.Property(u => u.apellido).HasMaxLength(50);

                entity.Property(u => u.celular).HasMaxLength(50);

                entity.Property(u => u.creado_en)
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(u => u.actualizado_en)
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(u => u.activo)
                  .HasDefaultValue(true);
                // Relaciones
                entity.HasOne(u => u.Rol)
                        .WithMany(r => r.Usuarios)
                        .HasForeignKey(u => u.rol_id);

                // Relación 1-a-0..1 (Usuario puede tener un Alumno, Alumno DEBE ser un Usuario)
                
                entity.HasOne(u => u.Alumno) 
                        .WithOne(a => a.Usuario)
                        .HasForeignKey<Alumno>(a => a.UsuarioId);

                // Relación 1-a-Muchos (Entrenador a Alumnos)
                entity.HasMany(u => u.AlumnosEntrenados)
                        .WithOne(a => a.Entrenador)
                        .HasForeignKey(a => a.entrenador_id)
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired(false);
            });

            // ====== Rol ======
            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("Rol");
                entity.HasKey(r => r.Id);
                entity.Property(r => r.nombre).HasMaxLength(25).IsRequired();

                entity.HasData(
                    new Rol { Id = Guid.NewGuid(), nombre = "Admin" },
                    new Rol { Id = Guid.NewGuid(), nombre = "Entrenador" },
                    new Rol { Id = Guid.NewGuid(), nombre = "Alumno" }
                );

            });

            // ====== Alumno ======
            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.ToTable("Alumno");
                entity.HasKey(a => a.Id);

                entity.Property(a => a.altura_cm).HasColumnType("numeric(5,2)");
                entity.Property(a => a.peso_kg).HasColumnType("numeric(5,2)");
                entity.Property(a => a.fecha_nacimiento).IsRequired();
                entity.Property(a => a.Direccion).HasColumnType("text").IsRequired();
                entity.Property(a => a.notas).HasColumnType("text").IsRequired(false);

            });
        }
    }
}
