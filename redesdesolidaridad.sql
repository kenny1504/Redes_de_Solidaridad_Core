-- phpMyAdmin SQL Dump
-- version 4.9.0.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 17-02-2020 a las 16:14:37
-- Versión del servidor: 10.4.6-MariaDB
-- Versión de PHP: 7.3.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `redesdesolidaridad`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `asignaturas`
--

CREATE TABLE `asignaturas` (
  `id` int(10) UNSIGNED NOT NULL,
  `Nombre` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Volcado de datos para la tabla `asignaturas`
--

INSERT INTO `asignaturas` (`id`, `Nombre`) VALUES
(1, 'Matematicas'),
(2, 'Lengua y Literatura'),
(3, 'ECA'),
(4, 'Educacion Fisica'),
(5, 'Computación'),
(6, 'Inglés'),
(7, 'OTV'),
(8, 'Geografía'),
(26, 'Programacion'),
(27, 'Html5'),
(28, 'xasmdasmdm,');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detallematriculas`
--

CREATE TABLE `detallematriculas` (
  `id` int(10) UNSIGNED NOT NULL,
  `Asignaturaid` int(10) UNSIGNED NOT NULL,
  `Matriculaid` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detallenotas`
--

CREATE TABLE `detallenotas` (
  `id` int(10) UNSIGNED NOT NULL,
  `Descripcion` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `docentes`
--

CREATE TABLE `docentes` (
  `id` int(10) UNSIGNED NOT NULL,
  `personasid` int(10) UNSIGNED NOT NULL,
  `Estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Volcado de datos para la tabla `docentes`
--

INSERT INTO `docentes` (`id`, `personasid`, `Estado`) VALUES
(1, 15, 1),
(2, 16, 1),
(3, 18, 1),
(4, 19, 1),
(5, 20, 1),
(6, 21, 1),
(7, 22, 1),
(8, 23, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `estudiantes`
--

CREATE TABLE `estudiantes` (
  `id` int(10) UNSIGNED NOT NULL,
  `personasid` int(10) UNSIGNED NOT NULL,
  `CodigoEstudiante` int(10) UNSIGNED NOT NULL,
  `parentescoid` int(10) UNSIGNED NOT NULL,
  `tutorid` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Volcado de datos para la tabla `estudiantes`
--

INSERT INTO `estudiantes` (`id`, `personasid`, `CodigoEstudiante`, `parentescoid`, `tutorid`) VALUES
(1, 2, 11003522, 2, 1),
(2, 9, 11102856, 7, 4),
(3, 10, 11035265, 4, 7),
(4, 11, 11025845, 3, 3),
(5, 12, 11015856, 3, 2),
(6, 13, 11032565, 6, 5),
(7, 14, 11026556, 2, 6),
(8, 17, 11022552, 2, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `funcionesasignada`
--

CREATE TABLE `funcionesasignada` (
  `Id_FuncionAcceso` int(11) NOT NULL,
  `Id` int(11) NOT NULL,
  `FechaDeVencimiento` date NOT NULL,
  `Id_Usuarios` int(11) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `funcionesasignada`
--

INSERT INTO `funcionesasignada` (`Id_FuncionAcceso`, `Id`, `FechaDeVencimiento`, `Id_Usuarios`) VALUES
(1, 1, '2021-02-27', 1),
(1, 2, '2021-12-23', 2),
(2, 3, '2021-09-15', 3),
(2, 4, '2021-09-15', 4),
(2, 5, '2021-09-15', 5),
(2, 6, '2021-09-15', 6),
(3, 7, '2019-11-25', 8);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `funcionesdeacceso`
--

CREATE TABLE `funcionesdeacceso` (
  `Id_FuncionAcceso` int(11) NOT NULL,
  `Descripcion` varchar(50) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `funcionesdeacceso`
--

INSERT INTO `funcionesdeacceso` (`Id_FuncionAcceso`, `Descripcion`) VALUES
(1, 'Administrador'),
(2, 'Invitado'),
(3, 'Docente');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `gradoaasignaturas`
--

CREATE TABLE `gradoaasignaturas` (
  `id` int(10) UNSIGNED NOT NULL,
  `Gradoid` int(10) UNSIGNED NOT NULL,
  `Asignaturaid` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Volcado de datos para la tabla `gradoaasignaturas`
--

INSERT INTO `gradoaasignaturas` (`id`, `Gradoid`, `Asignaturaid`) VALUES
(1, 1, 1),
(2, 1, 2),
(3, 1, 4),
(4, 1, 7),
(5, 1, 3),
(6, 1, 6),
(7, 2, 1),
(8, 2, 2),
(9, 2, 3),
(10, 2, 4),
(11, 2, 5),
(12, 2, 7),
(13, 2, 6),
(14, 3, 6),
(15, 3, 7),
(16, 3, 5),
(17, 3, 4),
(18, 3, 3),
(19, 3, 2),
(20, 3, 1),
(21, 4, 1),
(22, 4, 2),
(23, 4, 3),
(24, 4, 4),
(25, 4, 5),
(26, 4, 6),
(27, 4, 7),
(28, 4, 8),
(29, 5, 1),
(30, 5, 2),
(31, 5, 3),
(32, 5, 4),
(33, 5, 5),
(34, 5, 6),
(35, 5, 7),
(36, 5, 8),
(37, 6, 8),
(38, 6, 7),
(39, 6, 6),
(40, 6, 5),
(41, 6, 4),
(42, 6, 3),
(43, 6, 2),
(44, 6, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `grados`
--

CREATE TABLE `grados` (
  `id` int(10) UNSIGNED NOT NULL,
  `Grado` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Volcado de datos para la tabla `grados`
--

INSERT INTO `grados` (`id`, `Grado`) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `grupos`
--

CREATE TABLE `grupos` (
  `id` int(10) UNSIGNED NOT NULL,
  `Grupo` varchar(10) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Volcado de datos para la tabla `grupos`
--

INSERT INTO `grupos` (`id`, `Grupo`) VALUES
(1, 'A         '),
(2, 'B         '),
(3, 'C         ');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `matriculas`
--

CREATE TABLE `matriculas` (
  `id` int(10) UNSIGNED NOT NULL,
  `Fecha` date NOT NULL,
  `Ofertaid` int(10) UNSIGNED NOT NULL,
  `Turnoid` int(10) UNSIGNED NOT NULL,
  `SituacionMatriculaid` int(10) UNSIGNED NOT NULL,
  `Estudianteid` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `migrations`
--

CREATE TABLE `migrations` (
  `id` int(10) UNSIGNED NOT NULL,
  `migration` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `batch` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Volcado de datos para la tabla `migrations`
--

INSERT INTO `migrations` (`id`, `migration`, `batch`) VALUES
(1, '2019_09_04_192109_crear_tabla_personas', 1),
(2, '2019_09_04_194154_crear_tabla_oficios', 2),
(3, '2019_09_04_192717_crear_tabla_tutores', 3),
(4, '2019_09_04_194438_crear_tabla_docentes', 4),
(5, '2019_09_04_195319_crear_tabla_parentescos', 5),
(6, '2019_09_04_194743_crear_tabla_estudiantes', 6),
(7, '2019_09_04_200318_crear_tabla_secciones', 7),
(8, '2019_09_04_200418_crear_tabla_grupos', 8),
(9, '2019_09_04_200519_crear_tabla_grados', 9),
(10, '2019_09_04_195523_crear_tabla_ofertas', 10),
(11, '2019_09_04_200954_crear_tabla_asignaturas', 11),
(12, '2019_09_04_200644_crear_tabla_grado_asignaturas', 12),
(13, '2019_09_04_201841_crear_tabla_detalle_notas', 13),
(14, '2019_09_04_202713_crear_tabla_turnos', 14),
(15, '2019_09_04_202623_crear_tabla_situacion_matriculas', 15),
(16, '2019_09_04_202053_crear_tabla_matriculas', 16),
(17, '2019_09_04_201125_crear_tabla_detalle_matriculas', 17),
(18, '2019_09_04_201427_crear_tabla_notas', 18);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `notas`
--

CREATE TABLE `notas` (
  `id` int(10) UNSIGNED NOT NULL,
  `Nota` int(10) UNSIGNED NOT NULL,
  `DetalleNotaid` int(10) UNSIGNED NOT NULL,
  `DetalleMatriculaid` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ofertas`
--

CREATE TABLE `ofertas` (
  `id` int(10) UNSIGNED NOT NULL,
  `Descripcion` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `FechaOferta` year(4) NOT NULL,
  `Seccionid` int(10) UNSIGNED NOT NULL,
  `Gradoid` int(10) UNSIGNED NOT NULL,
  `Grupoid` int(10) UNSIGNED NOT NULL,
  `Docenteid` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Volcado de datos para la tabla `ofertas`
--

INSERT INTO `ofertas` (`id`, `Descripcion`, `FechaOferta`, `Seccionid`, `Gradoid`, `Grupoid`, `Docenteid`) VALUES
(1, 'Primer Grado A - Año Electivo 2019', 2019, 1, 1, 1, 6),
(2, 'Segundo Grado B - Año Electivo 2019', 2019, 2, 2, 2, 7),
(3, 'Tercer Grado C - Año Electivo 2019', 2019, 3, 3, 3, 8),
(4, 'Cuarto Grado A - Año Electivo 2019', 2019, 4, 4, 1, 4),
(5, 'Quinto Grado A - Año Electivo 2019', 2019, 1, 5, 1, 2),
(6, 'Sexto Grado A - Año Electivo 2019', 2019, 6, 6, 1, 5);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `oficios`
--

CREATE TABLE `oficios` (
  `id` int(10) UNSIGNED NOT NULL,
  `Nombre` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Volcado de datos para la tabla `oficios`
--

INSERT INTO `oficios` (`id`, `Nombre`) VALUES
(1, 'Empresario  Independiente                         '),
(2, 'Psicolog@                                         '),
(3, 'Dentista'),
(4, 'Vendedor'),
(5, 'Otro');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `parentescos`
--

CREATE TABLE `parentescos` (
  `id` int(10) UNSIGNED NOT NULL,
  `Parentesco` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Volcado de datos para la tabla `parentescos`
--

INSERT INTO `parentescos` (`id`, `Parentesco`) VALUES
(2, 'Madre'),
(3, 'Tio'),
(4, 'Abuela'),
(5, 'Abuelo'),
(6, 'Padre'),
(7, 'Tía'),
(8, 'Otro');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `personas`
--

CREATE TABLE `personas` (
  `id` int(10) UNSIGNED NOT NULL,
  `Cedula` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Nombre` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Apellido1` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Apellido2` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Sexo` varchar(1) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Direccion` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Correo` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Telefono` int(10) UNSIGNED NOT NULL,
  `FechaNacimiento` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Volcado de datos para la tabla `personas`
--

INSERT INTO `personas` (`id`, `Cedula`, `Nombre`, `Apellido1`, `Apellido2`, `Sexo`, `Direccion`, `Correo`, `Telefono`, `FechaNacimiento`) VALUES
(1, '001-010981-0111J', 'Lesbia', 'Bonilla', 'Vargas', 'F', 'Cine Salinas\r\nManagua', 'lesbia@gmail.com', 85452565, '1981-09-01'),
(2, '000-000000-0000P', 'Guissell Aleman', 'Aleman', 'Bonilla', 'F', 'Cine Salinas\r\nManagua', 'Estudiante@estudiante.com', 85452154, '2006-07-12'),
(3, '001-010880-1165K', 'Marcos', 'Obando', 'Perez', 'M', 'Salinas,Tola', 'Marcos@gmail.com', 85452565, '1980-08-01'),
(4, '001-010880-1165L', 'Andrea', 'Vanesa', 'López', 'M', 'Barrio Riguero', 'Vanesa@gmail.com', 78525654, '1980-08-01'),
(5, '001-010885-1165P', 'Reina', 'Isabel', 'Martinez', 'F', 'Barrio San Judas.', 'Isabel@gmail.com', 85269542, '1985-08-01'),
(6, '001-010881-1165M', 'Marvin', 'Isamel', 'Martinez', 'M', 'Barrio San Judas.', 'Marvin@gmail.com', 78524541, '1981-08-01'),
(7, '001-050680-1165J', 'Federico', 'Israel', 'Aleman', 'M', 'La colina.', 'Federico@gmail.com', 85452563, '1980-06-05'),
(8, '001-150680-1165M', 'María', 'Regina', 'López', 'F', 'La colina.', 'Maria@gmail.com', 78526366, '1980-06-15'),
(9, '000-000000-0000P', 'Kathya', 'Blanco', 'Martinez', 'F', 'Barrio San Judas.', 'Estudiante@estudiante.com', 87452156, '1998-08-12'),
(10, '000-000000-0000P', 'Esther', 'Palacio', 'Lopez', 'F', 'La Colina.', 'Estudiante@estudiante.com', 85456632, '1999-07-06'),
(11, '000-000000-0000P', 'Dina', 'Aleman', 'Perez', 'F', 'Barrio Riguero', 'Estudiante@estudiante.com', 85635425, '1999-05-12'),
(12, '000-000000-0000P', 'Roger', 'Mairena', 'Vargas', 'M', 'Barrio Riguero.', 'Estudiante@estudiante.com', 87526525, '1999-12-22'),
(13, '000-000000-0000P', 'Kevin', 'Amador', 'Sanchez', 'M', 'Cine Salinas\r\nManagua', 'Estudiante@estudiante.com', 85452156, '1999-04-06'),
(14, '000-000000-0000P', 'Alexander', 'Aleman', 'Nicolas', 'M', 'Lomas de Guadalupe', 'Estudiante@estudiante.com', 85462222, '2005-03-23'),
(15, '001-250880-1010K', 'Ezequiel', 'Fajardo', 'Guadamuz', 'M', 'Lomas de Guadalupe', 'Ezequiel@gmail.com', 85216565, '1980-08-25'),
(16, '001-120580-1010M', 'Carlos', 'Manuel', 'Bonilla', 'M', 'San Cristobal', 'Carlos@gmail.com', 84752136, '1980-05-13'),
(17, '000-000000-0000P', 'Karla', 'Aleman', 'Bonilla', 'F', 'Cine Salinas\r\nManagua', 'Estudiante@estudiante.com', 85452654, '2005-07-11'),
(18, '001-150380-0222L', 'Masiel', 'Perez', 'Soza', 'F', 'El Dorado', 'Masiel@gmail.com', 85422151, '1980-03-15'),
(19, '001-050781-0333M', 'Fernanda', 'Aburto', 'Valle', 'F', 'Las Brisas', 'Fernanda@gmail.com', 85163132, '1981-07-05'),
(20, '001-100982-0555L', 'Karmen', 'Cuarezma', 'Torres', 'F', 'San Jose Oriental', 'Karmen@gmail.com', 81316565, '1982-09-10'),
(21, '001-030191-1011M', 'Rosa', 'Espinoza', 'Sanchez', 'F', 'Camilo Zapata', 'Rosa@gmail.com', 81631231, '1991-01-03'),
(22, '001-151180-0222K', 'Mario', 'Betanco', 'Gonzalez', 'M', 'La colonia Managua', 'Mario@gmail.com', 84161362, '1980-11-15'),
(23, '001-250880-1010L', 'Jose', 'Mendez', 'Aguilar', 'M', 'Barrio Campo Bruce', 'Mendez@gmail.com', 84163132, '1980-08-25');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `secciones`
--

CREATE TABLE `secciones` (
  `id` int(10) UNSIGNED NOT NULL,
  `Codigo` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Volcado de datos para la tabla `secciones`
--

INSERT INTO `secciones` (`id`, `Codigo`) VALUES
(1, '001A                                              '),
(2, '002A                                              '),
(3, '003A                                              '),
(4, '004A                                              '),
(5, '005A                                              '),
(6, '006A                                              ');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `situacionmatriculas`
--

CREATE TABLE `situacionmatriculas` (
  `id` int(10) UNSIGNED NOT NULL,
  `Descripcion` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `turnos`
--

CREATE TABLE `turnos` (
  `id` int(10) UNSIGNED NOT NULL,
  `Nombre` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tutores`
--

CREATE TABLE `tutores` (
  `id` int(10) UNSIGNED NOT NULL,
  `personasid` int(10) UNSIGNED NOT NULL,
  `Oficiosid` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Volcado de datos para la tabla `tutores`
--

INSERT INTO `tutores` (`id`, `personasid`, `Oficiosid`) VALUES
(1, 1, 1),
(2, 3, 1),
(3, 4, 2),
(4, 5, 2),
(5, 6, 1),
(6, 7, 1),
(7, 8, 3);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `Id_Usuarios` int(11) NOT NULL,
  `ClaveDeUsuario` varchar(30) NOT NULL,
  `Nombre` varchar(18) NOT NULL,
  `Cedula` varchar(18) NOT NULL,
  `NombreDeUsuario` varchar(30) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`Id_Usuarios`, `ClaveDeUsuario`, `Nombre`, `Cedula`, `NombreDeUsuario`) VALUES
(1, '123', 'kenny saenz', '453-040498-0000F', 'kenny1504'),
(2, '1234', 'Jose Sandino', '001-150998-0000F', 'jsan98');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `asignaturas`
--
ALTER TABLE `asignaturas`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `detallematriculas`
--
ALTER TABLE `detallematriculas`
  ADD PRIMARY KEY (`id`),
  ADD KEY `Fk_detalleMatriculas_asignaturas` (`Asignaturaid`),
  ADD KEY `Fk_detalleMatriculas_matriculas` (`Matriculaid`);

--
-- Indices de la tabla `detallenotas`
--
ALTER TABLE `detallenotas`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `docentes`
--
ALTER TABLE `docentes`
  ADD PRIMARY KEY (`id`),
  ADD KEY `Fk_docentes_personas` (`personasid`);

--
-- Indices de la tabla `estudiantes`
--
ALTER TABLE `estudiantes`
  ADD PRIMARY KEY (`id`),
  ADD KEY `Fk_estudiantes_personas` (`personasid`),
  ADD KEY `Fk_estudiantes_parentescos` (`parentescoid`),
  ADD KEY `Fk_estudiantes_tutores` (`tutorid`);

--
-- Indices de la tabla `funcionesasignada`
--
ALTER TABLE `funcionesasignada`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `RefUsuarios13` (`Id_Usuarios`),
  ADD KEY `RefFuncionesDeAcceso15` (`Id_FuncionAcceso`);

--
-- Indices de la tabla `funcionesdeacceso`
--
ALTER TABLE `funcionesdeacceso`
  ADD PRIMARY KEY (`Id_FuncionAcceso`);

--
-- Indices de la tabla `gradoaasignaturas`
--
ALTER TABLE `gradoaasignaturas`
  ADD PRIMARY KEY (`id`),
  ADD KEY `Fk_gradoAasignatura_grados` (`Gradoid`),
  ADD KEY `Fk_gradoAasignaturas_asignaturas` (`Asignaturaid`);

--
-- Indices de la tabla `grados`
--
ALTER TABLE `grados`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `grupos`
--
ALTER TABLE `grupos`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `matriculas`
--
ALTER TABLE `matriculas`
  ADD PRIMARY KEY (`id`),
  ADD KEY `Fk_matriculas_ofertas` (`Ofertaid`),
  ADD KEY `Fk_matriculas_turnos` (`Turnoid`),
  ADD KEY `Fk_matriculas_situacionMatriculas` (`SituacionMatriculaid`),
  ADD KEY `Fk_matriculas_estudiantes` (`Estudianteid`);

--
-- Indices de la tabla `migrations`
--
ALTER TABLE `migrations`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `notas`
--
ALTER TABLE `notas`
  ADD PRIMARY KEY (`id`),
  ADD KEY `Fk_notas_DetalleNotas` (`DetalleNotaid`),
  ADD KEY `Fk_notas_DetalleMatriculas` (`DetalleMatriculaid`);

--
-- Indices de la tabla `ofertas`
--
ALTER TABLE `ofertas`
  ADD PRIMARY KEY (`id`),
  ADD KEY `Fk_ofertas_secciones` (`Seccionid`),
  ADD KEY `Fk_ofertas_grados` (`Gradoid`),
  ADD KEY `Fk_ofertas_grupos` (`Grupoid`),
  ADD KEY `Fk_ofertas_docentes` (`Docenteid`);

--
-- Indices de la tabla `oficios`
--
ALTER TABLE `oficios`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `parentescos`
--
ALTER TABLE `parentescos`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `personas`
--
ALTER TABLE `personas`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `secciones`
--
ALTER TABLE `secciones`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `situacionmatriculas`
--
ALTER TABLE `situacionmatriculas`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `turnos`
--
ALTER TABLE `turnos`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `tutores`
--
ALTER TABLE `tutores`
  ADD PRIMARY KEY (`id`),
  ADD KEY `Fk_tutores_personas` (`personasid`),
  ADD KEY `Fk_tutores_oficios` (`Oficiosid`);

--
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`Id_Usuarios`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `asignaturas`
--
ALTER TABLE `asignaturas`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

--
-- AUTO_INCREMENT de la tabla `detallematriculas`
--
ALTER TABLE `detallematriculas`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `detallenotas`
--
ALTER TABLE `detallenotas`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `docentes`
--
ALTER TABLE `docentes`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT de la tabla `estudiantes`
--
ALTER TABLE `estudiantes`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT de la tabla `funcionesasignada`
--
ALTER TABLE `funcionesasignada`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT de la tabla `funcionesdeacceso`
--
ALTER TABLE `funcionesdeacceso`
  MODIFY `Id_FuncionAcceso` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `gradoaasignaturas`
--
ALTER TABLE `gradoaasignaturas`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=45;

--
-- AUTO_INCREMENT de la tabla `grados`
--
ALTER TABLE `grados`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT de la tabla `grupos`
--
ALTER TABLE `grupos`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT de la tabla `matriculas`
--
ALTER TABLE `matriculas`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `migrations`
--
ALTER TABLE `migrations`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT de la tabla `notas`
--
ALTER TABLE `notas`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `ofertas`
--
ALTER TABLE `ofertas`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT de la tabla `oficios`
--
ALTER TABLE `oficios`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `parentescos`
--
ALTER TABLE `parentescos`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT de la tabla `personas`
--
ALTER TABLE `personas`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- AUTO_INCREMENT de la tabla `secciones`
--
ALTER TABLE `secciones`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT de la tabla `situacionmatriculas`
--
ALTER TABLE `situacionmatriculas`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `turnos`
--
ALTER TABLE `turnos`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `tutores`
--
ALTER TABLE `tutores`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `Id_Usuarios` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `detallematriculas`
--
ALTER TABLE `detallematriculas`
  ADD CONSTRAINT `Fk_detalleMatriculas_asignaturas` FOREIGN KEY (`Asignaturaid`) REFERENCES `asignaturas` (`id`),
  ADD CONSTRAINT `Fk_detalleMatriculas_matriculas` FOREIGN KEY (`Matriculaid`) REFERENCES `matriculas` (`id`);

--
-- Filtros para la tabla `docentes`
--
ALTER TABLE `docentes`
  ADD CONSTRAINT `Fk_docentes_personas` FOREIGN KEY (`personasid`) REFERENCES `personas` (`id`);

--
-- Filtros para la tabla `estudiantes`
--
ALTER TABLE `estudiantes`
  ADD CONSTRAINT `Fk_estudiantes_parentescos` FOREIGN KEY (`parentescoid`) REFERENCES `parentescos` (`id`),
  ADD CONSTRAINT `Fk_estudiantes_personas` FOREIGN KEY (`personasid`) REFERENCES `personas` (`id`),
  ADD CONSTRAINT `Fk_estudiantes_tutores` FOREIGN KEY (`tutorid`) REFERENCES `tutores` (`id`);

--
-- Filtros para la tabla `gradoaasignaturas`
--
ALTER TABLE `gradoaasignaturas`
  ADD CONSTRAINT `Fk_gradoAasignatura_grados` FOREIGN KEY (`Gradoid`) REFERENCES `grados` (`id`),
  ADD CONSTRAINT `Fk_gradoAasignaturas_asignaturas` FOREIGN KEY (`Asignaturaid`) REFERENCES `asignaturas` (`id`);

--
-- Filtros para la tabla `matriculas`
--
ALTER TABLE `matriculas`
  ADD CONSTRAINT `Fk_matriculas_estudiantes` FOREIGN KEY (`Estudianteid`) REFERENCES `estudiantes` (`id`),
  ADD CONSTRAINT `Fk_matriculas_ofertas` FOREIGN KEY (`Ofertaid`) REFERENCES `ofertas` (`id`),
  ADD CONSTRAINT `Fk_matriculas_situacionMatriculas` FOREIGN KEY (`SituacionMatriculaid`) REFERENCES `situacionmatriculas` (`id`),
  ADD CONSTRAINT `Fk_matriculas_turnos` FOREIGN KEY (`Turnoid`) REFERENCES `turnos` (`id`);

--
-- Filtros para la tabla `notas`
--
ALTER TABLE `notas`
  ADD CONSTRAINT `Fk_notas_DetalleMatriculas` FOREIGN KEY (`DetalleMatriculaid`) REFERENCES `detallematriculas` (`id`),
  ADD CONSTRAINT `Fk_notas_DetalleNotas` FOREIGN KEY (`DetalleNotaid`) REFERENCES `detallenotas` (`id`);

--
-- Filtros para la tabla `ofertas`
--
ALTER TABLE `ofertas`
  ADD CONSTRAINT `Fk_ofertas_docentes` FOREIGN KEY (`Docenteid`) REFERENCES `docentes` (`id`),
  ADD CONSTRAINT `Fk_ofertas_grados` FOREIGN KEY (`Gradoid`) REFERENCES `grados` (`id`),
  ADD CONSTRAINT `Fk_ofertas_grupos` FOREIGN KEY (`Grupoid`) REFERENCES `grupos` (`id`),
  ADD CONSTRAINT `Fk_ofertas_secciones` FOREIGN KEY (`Seccionid`) REFERENCES `secciones` (`id`);

--
-- Filtros para la tabla `tutores`
--
ALTER TABLE `tutores`
  ADD CONSTRAINT `Fk_tutores_oficios` FOREIGN KEY (`Oficiosid`) REFERENCES `oficios` (`id`),
  ADD CONSTRAINT `Fk_tutores_personas` FOREIGN KEY (`personasid`) REFERENCES `personas` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
