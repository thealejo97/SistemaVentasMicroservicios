# Sistema de Ventas - Microservicios en ASP.NET Core + Docker

Este proyecto es una arquitectura de microservicios desarrollada en **ASP.NET Core Web API**, usando **Entity Framework Core** y **Docker** para desplegar tres servicios independientes: `ClientesService`, `ProductosService` y `VentasService`. Cada uno tiene su propia base de datos PostgreSQL.

---

## 🌐 Tecnologías utilizadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- Docker + Docker Compose
- Swagger (OpenAPI)

---

## 📊 Estructura del proyecto

```
SistemaVentasMicroservicios/
├── docker-compose.yml              # Orquestador de servicios
├── ClientesService/                # Microservicio de clientes
│   ├── Controllers/
│   ├── Data/
│   ├── Models/
│   ├── Migrations/
│   ├── Program.cs
│   └── Dockerfile
├── ProductosService/               # Microservicio de productos
├── VentasService/                  # Microservicio de ventas
```

Cada microservicio:
- Tiene su propia API REST
- Tiene su propia base de datos PostgreSQL
- Se comunica con otros servicios por HTTP interno (en el futuro podría evolucionar a eventos)

---

## 🚀 Levantar el proyecto

### 1. Clona el repositorio
```bash
git clone https://github.com/tu-usuario/SistemaVentasMicroservicios.git
cd SistemaVentasMicroservicios
```

### 2. Levanta los servicios con Docker Compose
```bash
docker-compose up --build
```

Esto levanta:
- Clientes API en `http://localhost:5001/swagger`
- Productos API en `http://localhost:5002/swagger`
- Ventas API en `http://localhost:5003/swagger`

Cada uno con su propia base de datos:
- PostgreSQL de clientes en el puerto `5433`
- PostgreSQL de productos en `5434`
- PostgreSQL de ventas en `5435`

---

## 🔧 Migraciones y base de datos (manual)

Si haces cambios en los modelos, puedes ejecutar migraciones desde tu máquina local:

```bash
cd ClientesService
# Asegúrate de tener dotnet-ef instalado
# dotnet tool install --global dotnet-ef

# Cambia temporalmente el host a localhost en appsettings.json
# para apuntar a la base de datos de Docker

# Luego ejecuta:
dotnet ef migrations add NombreDeLaMigracion
dotnet ef database update
```

---

## 📃 Endpoints disponibles

### ClientesService
- `GET /api/clientes`
- `GET /api/clientes/{id}`
- `POST /api/clientes`
- `PUT /api/clientes/{id}`
- `DELETE /api/clientes/{id}`

### ProductosService *(pendiente implementar)*
### VentasService *(pendiente implementar)*

---

## 👩‍💼 Autor

Proyecto desarrollado por **Alejandro**, Jamundí - Valle del Cauca, Colombia.

> Este sistema es parte de mi estudio para pruebas técnicas y portafolio como desarrollador backend orientado a microservicios en .NET.

---

## 🚫 Licencia
Este proyecto es de uso libre con fines educativos y de portafolio personal.

