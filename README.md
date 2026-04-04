# 🏆 SportsLeague API

API REST para gestión de ligas deportivas con .NET 8, Entity Framework Core y SQL Server.

## 📦 Requisitos Previos

- .NET 8 SDK
- SQL Server (Express o Developer)
- Visual Studio 2022 / VS Code

## 🚀 Inicio Rápido

### 1. Clonar repositorio
bash
git clone https://github.com/TU_USUARIO/SportsLeague.git
cd SportsLeague

### 2. Configurar conexión a BD
Editar SportsLeague.API/appsettings.json:

json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=SportsLeagueDb;Trusted_Connection=true;TrustServerCertificate=true;"
}

### 3. Aplicar migraciones
powershell
Update-Database -Project SportsLeague.DataAccess}

### 4. Ejecutar API
bash
- cd SportsLeague.API
- dotnet run
- Abrir: https://localhost:7057/swagger

📁 Estructura del Proyecto
text
SportsLeague/
├── SportsLeague.API/          # Controladores, DTOs, AutoMapper
├── SportsLeague.Domain/       # Entidades, Enums, Interfaces, Services
└── SportsLeague.DataAccess/   # DbContext, Repositorios, Migraciones
🔧 Tecnologías
.NET 8

_Entity Framework Core

_SQL Server

_AutoMapper

_Swagger

# 📌 Endpoints Principales

## Método	Ruta	Descripción
- GET	/api/Team	Listar equipos
- POST	/api/Team	Crear equipo
- GET	/api/Player	Listar jugadores
- POST	/api/Tournament	Crear torneo
- GET	/api/Sponsor	Listar patrocinadores
## 🧪 Probar con Swagger
# Ejecutar dotnet run

Ir a https://localhost:7057/swagger

Probar los endpoints directamente desde la interfaz
