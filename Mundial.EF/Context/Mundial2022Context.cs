using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Mundial.Entidades;

namespace Mundial.EF
{
    public partial class Mundial2022Context : DbContext
    {
        public Mundial2022Context()
        {
        }

        public Mundial2022Context(DbContextOptions<Mundial2022Context> options)
            : base(options)
        {
        }

        public virtual DbSet<CentrosCosto> CentrosCostos { get; set; } = null!;
        public virtual DbSet<Club> Clubes { get; set; } = null!;
        public virtual DbSet<Configuracion> Configuracions { get; set; } = null!;
        public virtual DbSet<Continente> Continentes { get; set; } = null!;
        public virtual DbSet<Deporte> Deportes { get; set; } = null!;
        public virtual DbSet<Empresa> Empresas { get; set; } = null!;
        public virtual DbSet<Equipo> Equipos { get; set; } = null!;
        public virtual DbSet<Estadio> Estadios { get; set; } = null!;
        public virtual DbSet<Fase> Fases { get; set; } = null!;
        public virtual DbSet<Hobby> Hobbies { get; set; } = null!;
        public virtual DbSet<JugadorPuntaje> JugadorPuntajes { get; set; } = null!;
        public virtual DbSet<Jugador> Jugadores { get; set; } = null!;
        public virtual DbSet<LogAccione> LogAcciones { get; set; } = null!;
        public virtual DbSet<LogVisita> LogVisitas { get; set; } = null!;
        public virtual DbSet<Persona> Personas { get; set; } = null!;
        public virtual DbSet<Personal> Personals { get; set; } = null!;
        public virtual DbSet<Pregunta> Preguntas { get; set; } = null!;
        public virtual DbSet<PreguntasCompuesta> PreguntasCompuestas { get; set; } = null!;
        public virtual DbSet<Proveedores> Proveedores { get; set; } = null!;
        public virtual DbSet<RespuestasCompuesta> RespuestasCompuestas { get; set; } = null!;
        public virtual DbSet<Sectores> Sectores { get; set; } = null!;
        public virtual DbSet<Tecnico> Tecnicos { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<UsuariosRespuesta> UsuariosRespuestas { get; set; } = null!;

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Data Source=ARES;Initial Catalog=Mundial2022;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CentrosCosto>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpresa, e.CentroCosto });

                entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");

                entity.Property(e => e.CentroCosto).HasColumnName("centroCosto");

                entity.Property(e => e.CantidadDeEmpleados).HasColumnName("cantidadDeEmpleados");

                entity.Property(e => e.CentroCostoPadre).HasColumnName("centroCostoPadre");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.ParticipaEnGrupos)
                    .IsRequired()
                    .HasColumnName("participaEnGrupos")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Club>(entity =>
            {
                entity.HasKey(e => e.IdClub);

                entity.Property(e => e.IdClub).ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Configuracion>(entity =>
            {
                entity.HasKey(e => e.Clave);

                entity.ToTable("Configuracion");

                entity.Property(e => e.Clave)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Valor)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Continente>(entity =>
            {
                entity.HasKey(e => e.IdContinente)
                    .HasName("PK_Continente");

                entity.Property(e => e.IdContinente).ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Deporte>(entity =>
            {
                entity.HasKey(e => e.IdDeporte);

                entity.Property(e => e.IdDeporte).ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasKey(e => e.IdEmpresa);

                entity.ToTable("Empresa");

                entity.Property(e => e.IdEmpresa)
                    .ValueGeneratedNever()
                    .HasColumnName("idEmpresa");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Equipo>(entity =>
            {
                entity.HasKey(e => e.IdEquipo);

                entity.HasIndex(e => e.IdContinente, "IX_Equipos_IdContinente");

                entity.Property(e => e.IdEquipo).ValueGeneratedNever();

                entity.Property(e => e.Grupo)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(N'A')");

                entity.Property(e => e.Imagen)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Tecnico)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdContinenteNavigation)
                    .WithMany(p => p.Equipos)
                    .HasForeignKey(d => d.IdContinente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COntinente");
            });

            modelBuilder.Entity<Estadio>(entity =>
            {
                entity.HasKey(e => e.IdEstadio);

                entity.Property(e => e.IdEstadio).ValueGeneratedNever();

                entity.Property(e => e.Ciudad)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.Imagen)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Fase>(entity =>
            {
                entity.HasKey(e => e.IdFase);

                entity.Property(e => e.IdFase).ValueGeneratedNever();

                entity.Property(e => e.FechaFin).HasColumnType("datetime");

                entity.Property(e => e.FechaInicio).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Hobby>(entity =>
            {
                entity.HasKey(e => e.IdHobby);

                entity.Property(e => e.IdHobby).ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<JugadorPuntaje>(entity =>
            {
                entity.ToTable("JugadorPuntaje");

                entity.Property(e => e.GolAfavorArq).HasColumnName("GolAFavorArq");

                entity.Property(e => e.GolAfavorDef).HasColumnName("GolAFavorDef");

                entity.Property(e => e.GolAfavorDel).HasColumnName("GolAFavorDel");

                entity.Property(e => e.GolAfavorVol).HasColumnName("GolAFavorVol");
            });

            modelBuilder.Entity<Jugador>(entity =>
            {
                entity.HasKey(e => e.IdJugador);

                entity.Property(e => e.IdJugador).ValueGeneratedNever();

                entity.Property(e => e.Imagen)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Puesto)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LogAccione>(entity =>
            {
                entity.HasKey(e => e.IdAccion);

                entity.Property(e => e.IdAccion).ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(64)
                    .IsFixedLength();
            });

            modelBuilder.Entity<LogVisita>(entity =>
            {
                entity.HasKey(e => e.IdLog)
                    .HasName("PK_LogIngreso");

                entity.Property(e => e.Clave)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nick)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserBrowser)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserHostIp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UserHostIP");

                entity.Property(e => e.UserPlatform)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.ToTable("Persona");
                entity.HasKey(e => e.IdPersona);

                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cuit)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RedSocial)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Personal>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Personal");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Cuit)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CUIT");

                entity.Property(e => e.EstadoEstudios)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HijosEdadEscolar)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nick)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NivelEstudios)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sector)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TituloCertificacion)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pregunta>(entity =>
            {
                entity.HasKey(e => e.IdPregunta);

                entity.Property(e => e.IdPregunta).ValueGeneratedNever();

                entity.Property(e => e.Pregunta1)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("Pregunta");
            });

            modelBuilder.Entity<PreguntasCompuesta>(entity =>
            {
                entity.HasKey(e => new { e.IdPregunta, e.Respuesta });
            });

            modelBuilder.Entity<Proveedores>(entity =>
            {
                entity.HasKey(e => e.IdProveedor);

                entity.Property(e => e.IdProveedor).ValueGeneratedNever();

                entity.Property(e => e.Cuit)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CUIT");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(70)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RespuestasCompuesta>(entity =>
            {
                entity.HasKey(e => new { e.IdPregunta, e.Respuesta, e.IdUsuario })
                    .HasName("PK_RespuestasCompuestas_1");
            });

            modelBuilder.Entity<Sectores>(entity =>
            {
                entity.HasKey(e => e.IdSector)
                    .HasName("pkSectores");

                entity.Property(e => e.IdSector).ValueGeneratedNever();

                entity.Property(e => e.Sector)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tecnico>(entity =>
            {
                entity.HasKey(e => e.IdTecnico)
                    .HasName("pkTecnicos");

                entity.Property(e => e.IdTecnico).ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);
            });

            modelBuilder.Entity<UsuariosRespuesta>(entity =>
            {
                entity.HasKey(e => new { e.IdPregunta, e.IdUsuario });

                entity.Property(e => e.FechaGrabacion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
