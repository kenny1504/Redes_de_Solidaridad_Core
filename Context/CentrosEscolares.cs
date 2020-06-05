using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Redes_De_Solidaridad.Models;

namespace Redes_De_Solidaridad.Context
{
    public partial class CentrosEscolares : DbContext
    {
        public CentrosEscolares()
        {
        }

        public CentrosEscolares(DbContextOptions<CentrosEscolares> options)
            : base(options)
        {
        }

        public virtual DbSet<Asignaturas> Asignaturas { get; set; }
        public virtual DbSet<Asistencia> Asistencia { get; set; }
        public virtual DbSet<Detalleasignaturasinstitucion> Detalleasignaturasinstitucion { get; set; }
        public virtual DbSet<Detallematricula> Detallematricula { get; set; }
        public virtual DbSet<Detallematriculainstitucion> Detallematriculainstitucion { get; set; }
        public virtual DbSet<Detallenota> Detallenota { get; set; }
        public virtual DbSet<Detalleofertasinstitucion> Detalleofertasinstitucion { get; set; }
        public virtual DbSet<Docentes> Docentes { get; set; }
        public virtual DbSet<Estudiantes> Estudiantes { get; set; }
        public virtual DbSet<Gradoacademico> Gradoacademico { get; set; }
        public virtual DbSet<Gradoasignaturas> Gradoasignaturas { get; set; }
        public virtual DbSet<Grupos> Grupos { get; set; }
        public virtual DbSet<Institucion> Institucion { get; set; }
        public virtual DbSet<Matriculas> Matriculas { get; set; }
        public virtual DbSet<Notas> Notas { get; set; }
        public virtual DbSet<Ofertas> Ofertas { get; set; }
        public virtual DbSet<Oficios> Oficios { get; set; }
        public virtual DbSet<Parentescos> Parentescos { get; set; }
        public virtual DbSet<Personas> Personas { get; set; }
        public virtual DbSet<Secciones> Secciones { get; set; }
        public virtual DbSet<Situacionmatricula> Situacionmatricula { get; set; }
        public virtual DbSet<Turnos> Turnos { get; set; }
        public virtual DbSet<Tutores> Tutores { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Usuariosinstituciones> UsuariosInstituciones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
               optionsBuilder.UseMySql("server=localhost;port=3306;user=root;database=centrosescolares", x => x.ServerVersion("10.1.10-mariadb"));
               //optionsBuilder.UseMySql("server=sql10.freemysqlhosting.net;port=3306;user=sql10342007;password=pg34A4G6GL;database=sql10342007", x => x.ServerVersion("10.1.10-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asignaturas>(entity =>
            {
                entity.ToTable("asignaturas");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Asistencia>(entity =>
            {
                entity.ToTable("asistencia");

                entity.HasIndex(e => e.IdMatricula)
                    .HasName("RefMatriculas39");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Estado).HasColumnType("bit(1)");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.IdMatricula)
                    .HasColumnName("idMatricula")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Detalleasignaturasinstitucion>(entity =>
            {
                entity.ToTable("detalleasignaturasinstitucion");

                entity.HasIndex(e => e.IdAsignatura)
                    .HasName("RefAsignaturas49");

                entity.HasIndex(e => e.IdInstitucion)
                    .HasName("RefInstitucion48");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdAsignatura)
                    .HasColumnName("idAsignatura")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdInstitucion)
                    .HasColumnName("idInstitucion")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Detallematricula>(entity =>
            {
                entity.ToTable("detallematricula");

                entity.HasIndex(e => e.AsignaturasId)
                    .HasName("FK_AsignaturasDetalleOferta");

                entity.HasIndex(e => e.MatriculasId)
                    .HasName("FK_MatriculasDetalleOferta");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AsignaturasId).HasColumnType("int(11)");

                entity.Property(e => e.MatriculasId).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Detallematriculainstitucion>(entity =>
            {
                entity.ToTable("detallematriculainstitucion");

                entity.HasIndex(e => e.IdInstitucion)
                    .HasName("RefInstitucion44");

                entity.HasIndex(e => e.IdMatricula)
                    .HasName("RefMatriculas45");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdInstitucion)
                    .HasColumnName("idInstitucion")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdMatricula)
                    .HasColumnName("idMatricula")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Detallenota>(entity =>
            {
                entity.ToTable("detallenota");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Orden).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Detalleofertasinstitucion>(entity =>
            {
                entity.ToTable("detalleofertasinstitucion");

                entity.HasIndex(e => e.IdInstitucion)
                    .HasName("RefInstitucion46");

                entity.HasIndex(e => e.IdOferta)
                    .HasName("RefOfertas47");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdInstitucion)
                    .HasColumnName("idInstitucion")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdOferta)
                    .HasColumnName("idOferta")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Docentes>(entity =>
            {
                entity.ToTable("docentes");

                entity.HasIndex(e => e.PersonasId)
                    .HasName("FK_PersonasDocentes");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Estado).HasColumnType("bit(1)");

                entity.Property(e => e.PersonasId).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Estudiantes>(entity =>
            {
                entity.ToTable("estudiantes");

                entity.HasIndex(e => e.ParentescosId)
                    .HasName("FK_ParentescosEstudiantes");

                entity.HasIndex(e => e.PersonasId)
                    .HasName("FK_PersonasEstudiantes");

                entity.HasIndex(e => e.TutorId)
                    .HasName("FK_TutorEstudiantes");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodigoEstudiante)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ParentescosId).HasColumnType("int(11)");

                entity.Property(e => e.PersonasId).HasColumnType("int(11)");

                entity.Property(e => e.TutorId)
                    .HasColumnName("Tutor_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Gradoacademico>(entity =>
            {
                entity.ToTable("gradoacademico");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Grado).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Gradoasignaturas>(entity =>
            {
                entity.ToTable("gradoasignaturas");

                entity.HasIndex(e => e.AsignaturasId)
                    .HasName("FK_AsignaturasGradoAsignaturas");

                entity.HasIndex(e => e.GradoAcademicoId)
                    .HasName("FK_GradoAcademicoGradoAsignaturas");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AsignaturasId).HasColumnType("int(11)");

                entity.Property(e => e.GradoAcademicoId).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Grupos>(entity =>
            {
                entity.ToTable("grupos");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Grupo)
                    .IsRequired()
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Institucion>(entity =>
            {
                entity.ToTable("institucion");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Matriculas>(entity =>
            {
                entity.ToTable("matriculas");

                entity.HasIndex(e => e.EstudiantesId)
                    .HasName("FK_EstudiantesMatriculas");

                entity.HasIndex(e => e.OfertasId)
                    .HasName("FK_OfertasMatriculas");

                entity.HasIndex(e => e.SituacionMatriculaId)
                    .HasName("FK_SituacionMatriculaMatriculas");

                entity.HasIndex(e => e.TurnoId)
                    .HasName("FK_TurnoMatriculas");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.EstudiantesId)
                    .HasColumnName("Estudiantes_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("datetime");

                entity.Property(e => e.OfertasId).HasColumnType("int(11)");

                entity.Property(e => e.SituacionMatriculaId).HasColumnType("int(11)");

                entity.Property(e => e.TurnoId).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Notas>(entity =>
            {
                entity.ToTable("notas");

                entity.HasIndex(e => e.DetalleMatriculaId)
                    .HasName("FK_DetalleOfertaNotas");

                entity.HasIndex(e => e.DetalleNotaId)
                    .HasName("FK_DetalleNotaNotas");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.DetalleMatriculaId).HasColumnType("int(11)");

                entity.Property(e => e.DetalleNotaId).HasColumnType("int(11)");

                entity.Property(e => e.Nota).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Ofertas>(entity =>
            {
                entity.ToTable("ofertas");

                entity.HasIndex(e => e.DocentesId)
                    .HasName("FK_DocentesOfertas");

                entity.HasIndex(e => e.GradoAcademicoId)
                    .HasName("FK_GradoAcademicoOfertas");

                entity.HasIndex(e => e.GruposId)
                    .HasName("FK_GruposOfertas");

                entity.HasIndex(e => e.SeccionesId)
                    .HasName("FK_SeccionesOfertas");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.DocentesId)
                    .HasColumnName("Docentes_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FechaOferta).HasColumnType("date");

                entity.Property(e => e.GradoAcademicoId).HasColumnType("int(11)");

                entity.Property(e => e.GruposId).HasColumnType("int(11)");

                entity.Property(e => e.SeccionesId).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Oficios>(entity =>
            {
                entity.ToTable("oficios");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Parentescos>(entity =>
            {
                entity.ToTable("parentescos");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Parentesco)
                    .IsRequired()
                    .HasColumnName("parentesco")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Personas>(entity =>
            {
                entity.ToTable("personas");

                entity.HasIndex(e => e.IdInstitucion)
                    .HasName("RefInstitucion41");

                entity.Property(e => e.Id).HasColumnType("int(11)");

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
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Correo)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");

                entity.Property(e => e.IdInstitucion)
                    .HasColumnName("idInstitucion")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Sexo)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Telefono).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Secciones>(entity =>
            {
                entity.ToTable("secciones");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Situacionmatricula>(entity =>
            {
                entity.ToTable("situacionmatricula");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Turnos>(entity =>
            {
                entity.ToTable("turnos");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Tutores>(entity =>
            {
                entity.ToTable("tutores");

                entity.HasIndex(e => e.OficiosId)
                    .HasName("FK_oficiostutor");

                entity.HasIndex(e => e.PersonasId)
                    .HasName("FK_PersonasTutor");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.OficiosId)
                    .HasColumnName("oficiosId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PersonasId).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.ToTable("usuarios");

                entity.HasIndex(e => e.IdInstitucion)
                    .HasName("RefInstitucion43");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cedula)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Contraseña)
                    .IsRequired()
                    .HasColumnType("varchar(25)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.IdInstitucion)
                    .HasColumnName("idInstitucion")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Usuario)
                    .IsRequired()
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Usuariosinstituciones>(entity =>
            {
                entity.ToTable("usuariosinstituciones");

                entity.HasIndex(e => e.IdInstitucion)
                    .HasName("RefInstitucion42");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Contraseña)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.IdInstitucion)
                    .HasColumnName("idInstitucion")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Usuario)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
