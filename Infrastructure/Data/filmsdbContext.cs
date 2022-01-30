using System;
using filmsapi.domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace filmsapi.domain.infrastrucutre.data
{
    public partial class filmsdbContext : DbContext
    {
        public filmsdbContext()
        {
        }

        public filmsdbContext(DbContextOptions<filmsdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Director> Directors { get; set; }
        public virtual DbSet<Film> Films { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("workstation id=filmsdb.mssql.somee.com;packet size=4096;user id=lordviernes_SQLLogin_1;pwd=z8ui8d2ccj;data source=filmsdb.mssql.somee.com;persist security info=False;initial catalog=filmsdb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Director>(entity =>
            {
                entity.ToTable("directors");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("apellido");

                entity.Property(e => e.Nacionalidad)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nacionalidad");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.ToTable("films");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DirectorId).HasColumnName("director_id");

                entity.Property(e => e.Genero)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("genero");

                entity.Property(e => e.Puntuacion)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("puntuacion");

                entity.Property(e => e.Rating)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("rating");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("titulo");

                entity.Property(e => e.YearPublicacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("year_publicacion");

                entity.HasOne(d => d.Director)
                    .WithMany(p => p.Films)
                    .HasForeignKey(d => d.DirectorId)
                    .HasConstraintName("FK__films__year_publ__267ABA7A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
