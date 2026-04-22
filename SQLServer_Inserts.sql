
CREATE DATABASE DB_VETERINARIA;
GO

USE DB_VETERINARIA;

IF OBJECT_ID('citas', 'U') IS NOT NULL DROP TABLE citas;
IF OBJECT_ID('pacientes', 'U') IS NOT NULL DROP TABLE pacientes;
IF OBJECT_ID('clientes', 'U') IS NOT NULL DROP TABLE clientes;
IF OBJECT_ID('veterinarios', 'U') IS NOT NULL DROP TABLE veterinarios;
IF OBJECT_ID('servicios', 'U') IS NOT NULL DROP TABLE servicios;
IF OBJECT_ID('usuario', 'U') IS NOT NULL DROP TABLE usuario;
GO

CREATE TABLE usuario (
    id_usuario BIGINT IDENTITY(1,1) PRIMARY KEY,
    codigo NVARCHAR(20) NOT NULL,
    nombre NVARCHAR(100) NOT NULL,
    apellido NVARCHAR(100) NOT NULL,
    correo NVARCHAR(150) NOT NULL,
    contrasena NVARCHAR(MAX) NOT NULL,
    rol NVARCHAR(20) NOT NULL,
    estado BIT NOT NULL DEFAULT 1
);

CREATE TABLE clientes (
    id_cliente BIGINT IDENTITY(1,1) PRIMARY KEY,
    codigo NVARCHAR(20) NOT NULL,
    nombre NVARCHAR(100) NOT NULL,
    apellido NVARCHAR(100) NOT NULL,
    correo NVARCHAR(150),
    telefono NVARCHAR(20),
    direccion NVARCHAR(255),
    ciudad NVARCHAR(100),
    estado BIT NOT NULL DEFAULT 1
);

CREATE TABLE veterinarios (
    id_veterinario BIGINT IDENTITY(1,1) PRIMARY KEY,
    codigo NVARCHAR(20) NOT NULL,
    nombre NVARCHAR(100) NOT NULL,
    apellido NVARCHAR(100) NOT NULL,
    numero_colegiado NVARCHAR(50),
    especialidad NVARCHAR(100),
    correo NVARCHAR(150),
    telefono NVARCHAR(20),
    estado BIT NOT NULL DEFAULT 1
);

CREATE TABLE servicios (
    id_servicio BIGINT IDENTITY(1,1) PRIMARY KEY,
    codigo NVARCHAR(20) NOT NULL,
    nombre NVARCHAR(100) NOT NULL,
    descripcion NVARCHAR(MAX),
    precio FLOAT NOT NULL,
    duracion_estimada INT,
    estado BIT NOT NULL DEFAULT 1
);

CREATE TABLE pacientes (
    id_paciente BIGINT IDENTITY(1,1) PRIMARY KEY,
    codigo NVARCHAR(20) NOT NULL,
    nombre NVARCHAR(100) NOT NULL,
    especie NVARCHAR(50) NOT NULL,
    raza NVARCHAR(50),
    edad Float,
    peso FLOAT,
    color NVARCHAR(100),
    alergias NVARCHAR(MAX),
    estado BIT NOT NULL DEFAULT 1,
    id_cliente BIGINT NOT NULL,
    CONSTRAINT FK_pacientes_clientes FOREIGN KEY (id_cliente) REFERENCES clientes(id_cliente)
);

CREATE TABLE citas (
    id_cita BIGINT IDENTITY(1,1) PRIMARY KEY,
    codigo NVARCHAR(20) NOT NULL,
    fecha_hora DATETIME2 NOT NULL,
    motivo NVARCHAR(255),
    notas NVARCHAR(MAX),
    diagnostico NVARCHAR(MAX),
    tratamiento NVARCHAR(MAX),
    estado NVARCHAR(50) NOT NULL DEFAULT 'PROGRAMADA',
    id_paciente BIGINT NOT NULL,
    id_veterinario BIGINT NOT NULL,
    id_servicio BIGINT NOT NULL,
    CONSTRAINT FK_citas_pacientes FOREIGN KEY (id_paciente) REFERENCES pacientes(id_paciente),
    CONSTRAINT FK_citas_veterinarios FOREIGN KEY (id_veterinario) REFERENCES veterinarios(id_veterinario),
    CONSTRAINT FK_citas_servicios FOREIGN KEY (id_servicio) REFERENCES servicios(id_servicio)
);
GO

-- Insertar Usuarios
INSERT INTO usuario (codigo, nombre, apellido, contrasena, correo, rol, estado) VALUES
('US-0001', 'Admin', 'Principal', 'US-0001', 'admin@clinicavet.com', 'ADMIN', 1),
('US-0002', 'Elena', 'Rodriguez', 'US-0002', 'erodriguez.vet@clinicavet.com', 'VETERINARIO', 1),
('US-0003', 'Miguel', 'Sanchez', 'US-0003', 'msanchez.vet@clinicavet.com', 'VETERINARIO', 1),
('US-0004', 'Laura', 'Fernandez', 'US-0004', 'lfernandez.vet@clinicavet.com', 'VETERINARIO', 1),
('US-0011', 'Ana', 'Gomez', 'US-0011', 'ana.gomez@email.com', 'USUARIO', 1),
('US-0012', 'Luis', 'Martinez', 'US-0012', 'luis.martinez@email.com', 'USUARIO', 1);

-- Insertar Clientes
INSERT INTO clientes (codigo, nombre, apellido, correo, telefono, direccion, ciudad, estado) VALUES
('CL-0001', 'Ana', 'Gomez', 'ana.gomez@email.com', '987654321', 'Av. Los Girasoles 123', 'Lima', 1),
('CL-0002', 'Luis', 'Martinez', 'luis.martinez@email.com', '912345678', 'Calle Las Begonias 456', 'Arequipa', 1),
('CL-0003', 'Sofia', 'Torres', 'sofia.t@email.com', '955511223', 'Jr. Las Palmeras 789', 'Trujillo', 1),
('CL-0004', 'Carlos', 'Diaz', 'carlos.d@email.com', '998877665', 'Av. El Sol 101', 'Cusco', 1),
('CL-0005', 'Maria', 'Huaman', 'maria.h@email.com', '944332211', 'Calle Grau 222', 'Piura', 1);

-- Insertar Veterinarios
INSERT INTO veterinarios (codigo, nombre, apellido, especialidad, numero_colegiado, estado) VALUES
('VE-0001', 'Elena', 'Rodriguez', 'Cirugía General', 'CMVP-1234', 1),
('VE-0002', 'Miguel', 'Sanchez', 'Medicina Interna Felina', 'CMVP-5678', 1),
('VE-0003', 'Laura', 'Fernandez', 'Dermatología Veterinaria', 'CMVP-9101', 1),
('VE-0004', 'Ricardo', 'Flores', 'Cardiología Veterinaria', 'CMVP-2345', 1),
('VE-0005', 'Mateo', 'Rojas', 'Animales Exóticos', 'CMVP-6789', 1);

-- Insertar Servicios
INSERT INTO servicios (codigo, nombre, descripcion, precio, estado) VALUES
('S-0001', 'Consulta General', 'Revisión completa del estado de salud de la mascota.', 70.00, 1),
('S-0002', 'Vacunación Anual Canina', 'Incluye vacuna quíntuple y antirrábica.', 120.50, 1),
('S-0003', 'Desparasitación Interna', 'Tratamiento para eliminar parásitos intestinales.', 45.00, 1),
('S-0004', 'Corte de Pelo y Baño', 'Servicio completo para perros y gatos.', 85.00, 1),
('S-0005', 'Limpieza Dental', 'Eliminación de sarro bajo sedación.', 250.00, 1);


-- Insertar Pacientes
-- Inserción 1
INSERT INTO pacientes (alergias, codigo, color, edad, especie, estado, nombre, peso, raza, id_cliente)
VALUES ('Ninguna', 'PAC-001', 'Blanco y Negro', 5, 'Canino', 1, 'Firulais', 15.4, 'Border Collie', 1);

-- Inserción 2
INSERT INTO pacientes (alergias, codigo, color, edad, especie, estado, nombre, peso, raza, id_cliente)
VALUES ('Pollo', 'PAC-002', 'Gris', 2, 'Felino', 0, 'Mishi', 3.5, 'Siamés', 2);

-- Inserción 3
INSERT INTO pacientes (alergias, codigo, color, edad, especie, estado, nombre, peso, raza, id_cliente)
VALUES ('Ninguna', 'PAC-003', 'Dorado', 4, 'Canino', 1, 'Max', 28.0, 'Golden Retriever', 3);

-- Insertar Citas (Relacionados con pacientes, veterinarios y servicios ingresados)
INSERT INTO citas (id_paciente, id_veterinario, id_servicio, codigo, fecha_hora, estado, motivo, diagnostico) VALUES
(6, 1, 1, 'C-0001', '2024-06-10T10:00:00', 'Programada', 'Cojera', 'Evaluación por cojera.'),
(7, 2, 2, 'C-0002', '2024-06-10T11:30:00', 'Confirmada', 'Vacunas', 'Refuerzo vacunación.'),
(8, 3, 4, 'C-0003', '2024-06-11T09:00:00', 'Programada', 'Alergia', 'Servicio de grooming.')

GO

select * from pacientes
