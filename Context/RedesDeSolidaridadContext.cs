using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Redes_De_Solidaridad.Models;

namespace Redes_De_Solidaridad.Context
{
    public partial class RedesDeSolidaridadContext : DbContext
    {
        public RedesDeSolidaridadContext()
        {
        }

        public RedesDeSolidaridadContext(DbContextOptions<RedesDeSolidaridadContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Asignaturas> Asignaturas { get; set; }
        public virtual DbSet<Detallematriculas> Detallematriculas { get; set; }
        public virtual DbSet<Detallenotas> Detallenotas { get; set; }
        public virtual DbSet<Docentes> Docentes { get; set; }
        public virtual DbSet<Estudiantes> Estudiantes { get; set; }
        public virtual DbSet<Funcionesasignada> Funcionesasignada { get; set; }
        public virtual DbSet<Funcionesdeacceso> Funcionesdeacceso { get; set; }
        public virtual DbSet<Gradoaasignaturas> Gradoaasignaturas { get; set; }
        public virtual DbSet<Grados> Grados { get; set; }
        public virtual DbSet<Grupos> Grupos { get; set; }
        public virtual DbSet<Matriculas> Matriculas { get; set; }
        public virtual DbSet<Migrations> Migrations { get; set; }
        public virtual DbSet<Notas> Notas { get; set; }
        public virtual DbSet<Ofertas> Ofertas { get; set; }
        public virtual DbSet<Oficios> Oficios { get; set; }
        public virtual DbSet<Parentescos> Parentescos { get; set; }
        public virtual DbSet<Personas> Personas { get; set; }
        public virtual DbSet<Secciones> Secciones { get; set; }
        public virtual DbSet<Situacionmatriculas> Situacionmatriculas { get; set; }
        public virtual DbSet<Turnos> Turnos { get; set; }
        public virtual DbSet<Tutores> Tutores { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;database=redesdesolidaridad", x => x.ServerVersion("10.1.10-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asignaturas>(entity =>
            {
                entity.ToTable("asignaturas");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Detallematriculas>(entity =>
            {
                entity.ToTable("detallematriculas");

                entity.HasIndex(e => e.Asignaturaid)
                    .HasName("Fk_detalleMatriculas_asignaturas");

                entity.HasIndex(e => e.Matriculaid)
                    .HasName("Fk_detalleMatriculas_matriculas");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Asignaturaid).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Matriculaid).HasColumnType("int(10) unsigned");

                entity.HasOne(d => d.Asignatura)
                    .WithMany(p => p.Detallematriculas)
                    .HasForeignKey(d => d.Asignaturaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_detalleMatriculas_asignaturas");

                entity.HasOne(d => d.Matricula)
                    .WithMany(p => p.Detallematriculas)
                    .HasForeignKey(d => d.Matriculaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_detalleMatriculas_matriculas");
            });

            modelBuilder.Entity<Detallenotas>(entity =>
            {
                entity.ToTable("detallenotas");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Docentes>(entity =>
            {
                entity.ToTable("docentes");

                entity.HasIndex(e => e.Personasid)
                    .HasName("Fk_docentes_personas");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Personasid)
                    .HasColumnName("personasid")
                    .HasColumnType("int(10) unsigned");

                entity.HasOne(d => d.Personas)
                    .WithMany(p => p.Docentes)
                    .HasForeignKey(d => d.Personasid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_docentes_personas");
            });

            modelBuilder.Entity<Estudiantes>(entity =>
            {
                entity.ToTable("estudiantes");

                entity.HasIndex(e => e.Parentescoid)
                    .HasName("Fk_estudiantes_parentescos");

                entity.HasIndex(e => e.Personasid)
                    .HasName("Fk_estudiantes_personas");

                entity.HasIndex(e => e.Tutorid)
                    .HasName("Fk_estudiantes_tutores");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CodigoEstudiante).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Parentescoid)
                    .HasColumnName("parentescoid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Personasid)
                    .HasColumnName("personasid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Tutorid)
                    .HasColumnName("tutorid")
                    .HasColumnType("int(10) unsigned");

                entity.HasOne(d => d.Parentesco)
                    .WithMany(p => p.Estudiantes)
                    .HasForeignKey(d => d.Parentescoid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_estudiantes_parentescos");

                entity.HasOne(d => d.Personas)
                    .WithMany(p => p.Estudiantes)
                    .HasForeignKey(d => d.Personasid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_estudiantes_personas");

                entity.HasOne(d => d.Tutor)
                    .WithMany(p => p.Estudiantes)
                    .HasForeignKey(d => d.Tutorid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_estudiantes_tutores");
            });

            modelBuilder.Entity<Funcionesasignada>(entity =>
            {
                entity.ToTable("funcionesasignada");

                entity.HasIndex(e => e.IdFuncionAcceso)
                    .HasName("RefFuncionesDeAcceso15");

                entity.HasIndex(e => e.IdUsuarios)
                    .HasName("RefUsuarios13");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.FechaDeVencimiento).HasColumnType("date");

                entity.Property(e => e.IdFuncionAcceso)
                    .HasColumnName("Id_FuncionAcceso")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUsuarios)
                    .HasColumnName("Id_Usuarios")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Funcionesdeacceso>(entity =>
            {
                entity.HasKey(e => e.IdFuncionAcceso)
                    .HasName("PRIMARY");

                entity.ToTable("funcionesdeacceso");

                entity.Property(e => e.IdFuncionAcceso)
                    .HasColumnName("Id_FuncionAcceso")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Gradoaasignaturas>(entity =>
            {
                entity.ToTable("gradoaasignaturas");

                entity.HasIndex(e => e.Asignaturaid)
                    .HasName("Fk_gradoAasignaturas_asignaturas");

                entity.HasIndex(e => e.Gradoid)
                    .HasName("Fk_gradoAasignatura_grados");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Asignaturaid).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Gradoid).HasColumnType("int(10) unsigned");

                entity.HasOne(d => d.Asignatura)
                    .WithMany(p => p.Gradoaasignaturas)
                    .HasForeignKey(d => d.Asignaturaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_gradoAasignaturas_asignaturas");

                entity.HasOne(d => d.Grado)
                    .WithMany(p => p.Gradoaasignaturas)
                    .HasForeignKey(d => d.Gradoid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_gradoAasignatura_grados");
            });

            modelBuilder.Entity<Grados>(entity =>
            {
                entity.ToTable("grados");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Grado).HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<Grupos>(entity =>
            {
                entity.ToTable("grupos");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Grupo)
                    .IsRequired()
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Matriculas>(entity =>
            {
                entity.ToTable("matriculas");

                entity.HasIndex(e => e.Estudianteid)
                    .HasName("Fk_matriculas_estudiantes");

                entity.HasIndex(e => e.Ofertaid)
                    .HasName("Fk_matriculas_ofertas");

                entity.HasIndex(e => e.SituacionMatriculaid)
                    .HasName("Fk_matriculas_situacionMatriculas");

                entity.HasIndex(e => e.Turnoid)
                    .HasName("Fk_matriculas_turnos");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Estudianteid).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Ofertaid).HasColumnType("int(10) unsigned");

                entity.Property(e => e.SituacionMatriculaid).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Turnoid).HasColumnType("int(10) unsigned");

                entity.HasOne(d => d.Estudiante)
                    .WithMany(p => p.Matriculas)
                    .HasForeignKey(d => d.Estudianteid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_matriculas_estudiantes");

                entity.HasOne(d => d.Oferta)
                    .WithMany(p => p.Matriculas)
                    .HasForeignKey(d => d.Ofertaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_matriculas_ofertas");

                entity.HasOne(d => d.SituacionMatricula)
                    .WithMany(p => p.Matriculas)
                    .HasForeignKey(d => d.SituacionMatriculaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_matriculas_situacionMatriculas");

                entity.HasOne(d => d.Turno)
                    .WithMany(p => p.Matriculas)
                    .HasForeignKey(d => d.Turnoid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_matriculas_turnos");
            });

            modelBuilder.Entity<Migrations>(entity =>
            {
                entity.ToTable("migrations");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Batch)
                    .HasColumnName("batch")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Migration)
                    .IsRequired()
                    .HasColumnName("migration")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Notas>(entity =>
            {
                entity.ToTable("notas");

                entity.HasIndex(e => e.DetalleMatriculaid)
                    .HasName("Fk_notas_DetalleMatriculas");

                entity.HasIndex(e => e.DetalleNotaid)
                    .HasName("Fk_notas_DetalleNotas");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.DetalleMatriculaid).HasColumnType("int(10) unsigned");

                entity.Property(e => e.DetalleNotaid).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Nota).HasColumnType("int(10) unsigned");

                entity.HasOne(d => d.DetalleMatricula)
                    .WithMany(p => p.Notas)
                    .HasForeignKey(d => d.DetalleMatriculaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_notas_DetalleMatriculas");

                entity.HasOne(d => d.DetalleNota)
                    .WithMany(p => p.Notas)
                    .HasForeignKey(d => d.DetalleNotaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_notas_DetalleNotas");
            });

            modelBuilder.Entity<Ofertas>(entity =>
            {
                entity.ToTable("ofertas");

                entity.HasIndex(e => e.Docenteid)
                    .HasName("Fk_ofertas_docentes");

                entity.HasIndex(e => e.Gradoid)
                    .HasName("Fk_ofertas_grados");

                entity.HasIndex(e => e.Grupoid)
                    .HasName("Fk_ofertas_grupos");

                entity.HasIndex(e => e.Seccionid)
                    .HasName("Fk_ofertas_secciones");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Docenteid).HasColumnType("int(10) unsigned");

                entity.Property(e => e.FechaOferta).HasColumnType("date");

                entity.Property(e => e.Gradoid).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Grupoid).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Seccionid).HasColumnType("int(10) unsigned");

                entity.HasOne(d => d.Docente)
                    .WithMany(p => p.Ofertas)
                    .HasForeignKey(d => d.Docenteid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_ofertas_docentes");

                entity.HasOne(d => d.Grado)
                    .WithMany(p => p.Ofertas)
                    .HasForeignKey(d => d.Gradoid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_ofertas_grados");

                entity.HasOne(d => d.Grupo)
                    .WithMany(p => p.Ofertas)
                    .HasForeignKey(d => d.Grupoid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_ofertas_grupos");

                entity.HasOne(d => d.Seccion)
                    .WithMany(p => p.Ofertas)
                    .HasForeignKey(d => d.Seccionid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_ofertas_secciones");
            });

            modelBuilder.Entity<Oficios>(entity =>
            {
                entity.ToTable("oficios");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Parentescos>(entity =>
            {
                entity.ToTable("parentescos");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Parentesco)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Personas>(entity =>
            {
                entity.ToTable("personas");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Apellido1)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Apellido2)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Cedula)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Sexo)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Telefono).HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<Secciones>(entity =>
            {
                entity.ToTable("secciones");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Situacionmatriculas>(entity =>
            {
                entity.ToTable("situacionmatriculas");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Turnos>(entity =>
            {
                entity.ToTable("turnos");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Tutores>(entity =>
            {
                entity.ToTable("tutores");

                entity.HasIndex(e => e.Oficiosid)
                    .HasName("Fk_tutores_oficios");

                entity.HasIndex(e => e.Personasid)
                    .HasName("Fk_tutores_personas");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Oficiosid).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Personasid)
                    .HasColumnName("personasid")
                    .HasColumnType("int(10) unsigned");

                entity.HasOne(d => d.Oficios)
                    .WithMany(p => p.Tutores)
                    .HasForeignKey(d => d.Oficiosid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_tutores_oficios");

                entity.HasOne(d => d.Personas)
                    .WithMany(p => p.Tutores)
                    .HasForeignKey(d => d.Personasid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_tutores_personas");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.IdUsuarios)
                    .HasName("PRIMARY");

                entity.ToTable("usuarios");

                entity.Property(e => e.IdUsuarios)
                    .HasColumnName("Id_Usuarios")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cedula)
                    .IsRequired()
                    .HasColumnType("varchar(18)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ClaveDeUsuario)
                    .IsRequired()
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(18)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreDeUsuario)
                    .IsRequired()
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
