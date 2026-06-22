# VIVUM — Sistema de Gestión de Despacho de Mercadería

Trabajo Práctico Integrador — Programación III BE  
**Integrantes:** Bruno Piri · Valentín Borio · Agustín Beninca

---

## Tecnologías

- ASP.NET Core 10.0 MVC
- Entity Framework Core 10 (Code First)
- SQL Server
- Bootstrap 5 + Select2

---

## Requisitos previos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server (ver configuración abajo)

---

## Configuración y ejecución

### Windows
No requiere configuración extra. Usa `appsettings.json` con SQLEXPRESS por defecto.

```bash
dotnet run
```

### Mac
Requiere SQL Server vía Docker.

**Primera vez:**
```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Admin1234!" \
  -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest
```

Crear `appsettings.Development.json` (ya está en `.gitignore`):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=DBCRUD;User Id=sa;Password=Admin1234!;TrustServerCertificate=True;MultipleActiveResultSets=True;"
  }
}
```

**Desde la segunda vez:**
```bash
docker start sqlserver
dotnet run
```

> Las migraciones se aplican automáticamente al iniciar la aplicación.

**URL:** http://localhost:5212

---

## Credenciales por defecto

| Usuario | Contraseña | Rol   |
|---------|------------|-------|
| admin   | Admin1234  | Admin |

---

## Funcionalidades

- **Despachos:** crear, cargar productos, confirmar o cancelar pedidos
- **Pedidos:** historial de pedidos confirmados con filtro por usuario
- **Clientes:** ABM con gestión de direcciones
- **Productos:** ABM con control de stock
- **Direcciones:** ABM independiente asociado a clientes
- **Usuarios:** ABM protegido (solo Admin) con roles Admin/Operador
- **Autenticación:** login/logout por sesión con contraseñas hasheadas
