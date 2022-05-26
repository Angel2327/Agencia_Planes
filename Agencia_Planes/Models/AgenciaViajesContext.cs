using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Agencia_Planes.Models
{
    public partial class AgenciaViajesContext : DbContext
    {
        public AgenciaViajesContext()
        {
        }

        public AgenciaViajesContext(DbContextOptions<AgenciaViajesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ciudade> Ciudades { get; set; } = null!;
        public virtual DbSet<Compra> Compras { get; set; } = null!;
        public virtual DbSet<Persona> Personas { get; set; } = null!;
        public virtual DbSet<PlanViaje> PlanViajes { get; set; } = null!;
        public virtual DbSet<TransporteEnAvion> TransporteEnAvion { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=EQUIPO-ANGEL-MA\\SQLEXPRESS; Database=Agencia Viajes; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ciudade>(entity =>
            {
                entity.HasKey(e => e.CodigoCiudad)
                    .HasName("PK_ciudades_Codigo_Ciudad");

                entity.ToTable("ciudades");

                entity.HasIndex(e => e.CodigoCiudad, "ciudades$Codigo_Ciudad_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.CodigoCiudad)
                    .HasMaxLength(45)
                    .HasColumnName("Codigo_Ciudad");

                entity.Property(e => e.Geolocalizacion).HasMaxLength(45);

                entity.Property(e => e.NombreCiudad)
                    .HasMaxLength(45)
                    .HasColumnName("Nombre_Ciudad");
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasKey(e => e.FechaCompra)
                    .HasName("PK_compras_Fecha_Compra");

                entity.ToTable("compras");

                entity.HasIndex(e => e.CodigoDeVuelo, "Codigo_de_vuelo_idx");

                entity.HasIndex(e => e.CodigoPlan, "fk_Compras_Plan_Viaje_idx");

                entity.HasIndex(e => e.Cedula, "fk_persona_has_plan_viaje_persona1_idx");

                entity.Property(e => e.FechaCompra)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Compra");

                entity.Property(e => e.CodigoDeVuelo)
                    .HasMaxLength(45)
                    .HasColumnName("Codigo_De_Vuelo");

                entity.Property(e => e.CodigoPlan).HasColumnName("Codigo_Plan");

                entity.Property(e => e.NumeroPersonas).HasColumnName("Numero_Personas");

                entity.Property(e => e.PrecioPagado).HasColumnName("Precio_Pagado");

                entity.HasOne(d => d.Cedula_Compra)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.Cedula)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_compras_persona");

                entity.HasOne(d => d.CodigoDeVuelo_Compra)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.CodigoDeVuelo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_compras_transporte_en_avion");

                entity.HasOne(d => d.CodigoPlan_Compra)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.CodigoPlan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("compras$fk_Compras_Plan_Viaje");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.Cedula)
                    .HasName("PK_persona_Cedula");

                entity.ToTable("persona");

                entity.HasIndex(e => e.Cedula, "persona$Cedula_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Cedula).ValueGeneratedNever();

                entity.Property(e => e.Contraseña).HasMaxLength(45);

                entity.Property(e => e.Correo).HasMaxLength(45);

                entity.Property(e => e.Nombre).HasMaxLength(45);

                entity.Property(e => e.Usuario).HasMaxLength(45);
            });

            modelBuilder.Entity<PlanViaje>(entity =>
            {
                entity.HasKey(e => e.CodigoPlan)
                    .HasName("PK_plan_viaje_Codigo_Plan");

                entity.ToTable("plan_viaje");

                entity.HasIndex(e => e.CodigoCiudad, "Codigo_Ciudad_idx");

                entity.Property(e => e.CodigoPlan)
                    .ValueGeneratedNever()
                    .HasColumnName("Codigo_Plan");

                entity.Property(e => e.ActividadesIncluidas).HasColumnName("Actividades_Incluidas");

                entity.Property(e => e.CodigoCiudad)
                    .HasMaxLength(45)
                    .HasColumnName("Codigo_Ciudad");

                entity.Property(e => e.FechaFin)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Fin");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Inicio");

                entity.Property(e => e.IncluyeHospedaje)
                    .HasMaxLength(2)
                    .HasColumnName("Incluye_Hospedaje");

                entity.Property(e => e.NombrePlan)
                    .HasMaxLength(45)
                    .HasColumnName("Nombre_Plan");

                entity.HasOne(d => d.CodigoCiudad_PlanViaje)
                    .WithMany(p => p.PlanViajes)
                    .HasForeignKey(d => d.CodigoCiudad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_plan_viaje_ciudades");
            });

            modelBuilder.Entity<TransporteEnAvion>(entity =>
            {
                entity.HasKey(e => e.CodigoDeVuelo)
                    .HasName("PK_transporte_en_avion_Codigo_De_Vuelo");

                entity.ToTable("transporte_en_avion");

                entity.Property(e => e.CodigoDeVuelo)
                    .HasMaxLength(45)
                    .HasColumnName("Codigo_De_Vuelo");

                entity.Property(e => e.Abordo).HasMaxLength(2);

                entity.Property(e => e.Aeroliena).HasMaxLength(40);

                entity.Property(e => e.HoraLlegada)
                    .HasPrecision(0)
                    .HasColumnName("Hora_Llegada");

                entity.Property(e => e.HoraSalida)
                    .HasPrecision(0)
                    .HasColumnName("Hora_Salida");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
