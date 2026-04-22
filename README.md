# VetSoft API - Documentación


### Operaciones Disponibles
- GET (obtener datos)
- POST (crear datos)
- PUT (actualizar datos)
- DELETE (eliminar datos)
-  Búsquedas avanzadas
- Filtros por campo
- Estadísticas

## Inicio

### 1. Verificar Servidor
```bash
curl http://localhost:5000/api/usuarios
```

### 2. Crear Cliente
```bash
curl -X POST http://localhost:5000/api/clientes \
  -H "Content-Type: application/json" \
  -d '{
    "codigo": "CLI001",
    "nombre": "Juan",
    "apellido": "Gómez",
    "correo": "juan@example.com",
    "telefono": "+34 912345678",
    "direccion": "Calle 123",
    "ciudad": "Madrid",
    "estado": true
  }'
```

### 3. Crear Mascota
```bash
curl -X POST http://localhost:5000/api/pacientes \
  -H "Content-Type: application/json" \
  -d '{
    "codigo": "PAC001",
    "nombre": "Misu",
    "especie": "Gato",
    "raza": "Siamés",
    "edad": 3,
    "peso": 4.2,
    "color": "Café",
    "alergias": "Penicilina",
    "estado": true,
    "idCliente": 1
  }'
```

### 4. Agendar Cita
```bash
curl -X POST http://localhost:5000/api/citas \
  -H "Content-Type: application/json" \
  -d '{
    "codigo": "CIT001",
    "fechaHora": "2024-04-25T10:30:00",
    "motivo": "Vacunación",
    "estado": "PROGRAMADA",
    "idPaciente": 1,
    "idVeterinario": 1,
    "idServicio": 1
  }'
```

---

## Modelos de Datos

### USUARIO
```json
{
  "idUsuario": 1,
  "codigo": "USU001",
  "nombre": "Juan",
  "apellido": "Pérez",
  "correo": "juan@example.com",
  "rolString": "USUARIO",  // ADMIN, VETERINARIO, USUARIO
  "estado": true
}
```

### VETERINARIO
```json
{
  "idVeterinario": 1,
  "codigo": "VET001",
  "nombre": "María",
  "apellido": "Rodríguez",
  "numeroColegiado": "COL-2024-001",
  "especialidad": "Cirugía felina",
  "correo": "maria@veterinaria.com",
  "telefono": "+34 912345678",
  "estado": true
}
```

### CLIENTE
```json
{
  "idCliente": 1,
  "codigo": "CLI001",
  "nombre": "Juan",
  "apellido": "Gómez",
  "correo": "juan@example.com",
  "telefono": "+34 912345678",
  "direccion": "Calle Principal 123",
  "ciudad": "Madrid",
  "estado": true
}
```

### PACIENTE
```json
{
  "idPaciente": 1,
  "codigo": "PAC001",
  "nombre": "Misu",
  "especie": "Gato",
  "raza": "Siamés",
  "edad": 3,
  "peso": 4.2,
  "color": "Café",
  "alergias": "Alérgico a la penicilina",
  "estado": true,
  "idCliente": 1
}
```

### SERVICIO
```json
{
  "idServicio": 1,
  "codigo": "SRV001",
  "nombre": "Vacunación básica",
  "descripcion": "Aplicación de vacunas básicas",
  "precio": 45.50,
  "duracionEstimada": 30,
  "estado": true
}
```

### CITA
```json
{
  "idCita": 1,
  "codigo": "CIT001",
  "fechaHora": "2024-04-25T10:30:00",
  "motivo": "Vacunación anual",
  "notas": "Sin complicaciones",
  "diagnostico": "Gato sano",
  "tratamiento": "Vacunas aplicadas",
  "estado": "COMPLETADA",  // PROGRAMADA, COMPLETADA, CANCELADA
  "idPaciente": 1,
  "idVeterinario": 1,
  "idServicio": 1
}
```

---

## Endpoints

### CRUD Básico
```
GET    /api/{recurso}           → Obtener todos
GET    /api/{recurso}/{id}       → Obtener uno
POST   /api/{recurso}            → Crear
PUT    /api/{recurso}/{id}        → Actualizar
DELETE /api/{recurso}/{id}       → Eliminar
```

### Búsquedas
```
GET /api/{recurso}/buscar/{termino}                    → Búsqueda global
GET /api/{recurso}/buscar/nombre?nombre=X             → Por nombre
GET /api/{recurso}/buscar/ciudad?ciudad=X             → Por ciudad
GET /api/{recurso}/correo/{correo}                    → Por correo
GET /api/{recurso}/telefono/{telefono}                → Por teléfono
GET /api/{recurso}/activos                            → Solo activos
GET /api/{recurso}/estadisticas/activos               → Contar activos
```

---

## Casos de Uso

### Caso 1: Registrar Nueva Mascota
```
1. POST /api/clientes          → Crear cliente
2. POST /api/pacientes         → Crear mascota del cliente
3. GET  /api/pacientes/{id}    → Verificar datos
```

### Caso 2: Agendar Cita Veterinaria
```
1. GET  /api/veterinarios      → Listar veterinarios disponibles
2. GET  /api/servicios         → Listar servicios
3. POST /api/citas             → Crear cita
4. GET  /api/citas/{id}        → Confirmar cita
```

### Caso 3: Completar Cita con Diagnóstico
```
1. GET  /api/citas/{id}        → Obtener cita
2. PUT  /api/citas/{id}        → Actualizar con diagnóstico
3. GET  /api/citas/{id}        → Verificar cambios
```

### Caso 4: Búsqueda de Clientes por Ciudad
```
GET /api/clientes/buscar/ciudad?ciudad=Madrid
```

### Caso 5: Obtener Servicios Activos
```
GET /api/servicios/activos
```

---

## Herramientas Recomendadas

### Visual Studio Code + REST Client
**Mejor para**: Desarrollo rápido
```bash
Extensión: REST Client
Archivo: API_ENDPOINTS_PRUEBA.http
```

### Postman
**Mejor para**: Testing profesional y documentación
```
Colecciones: Crear desde ejemplos JSON
Variables: Configurar baseUrl
```

### cURL
**Mejor para**: Scripts y automatización
```bash
Herramienta: Terminal/PowerShell
```

## Seguridad

### Implementado
- CORS configurado
- Content-Type validation
- Database constraints
- Relaciones validadas

### Por Implementar
- [] Autenticación (JWT/OAuth)
- [] Autorización basada en roles
- [] Hash de contraseñas (bcrypt)
- [] Rate limiting
- [] HTTPS en producción
