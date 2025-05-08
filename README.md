# 🧪 Prueba Técnica – Desarrollador C\#

## 📋 Descripción General

Esta solución está compuesta por 1 solucion y cuatro proyectos:

1. **EmployeeManagementAPI (.NET Core)**
   API REST que permite realizar operaciones CRUD sobre empleados usando **ADO.NET** para el acceso a datos y **LINQ** con base de datos SQLSERVER para las consultas.

2. **EmployeeManagementWeb (Interfaz de Usuario)**
   Aplicación de escritorio construida en **.NET Core o .NET Framework**, que consume la API mediante HttpClient. Permite listar, crear, actualizar y eliminar empleados.

3. **Exercise_3 y Exercise_4  (Consola)**
   Proyecto de consola con resoluciones para los puntos 3 y 4 de la prueba:

   * Impresión recursiva del 0 al 100 de 3 en 3.
   * Rutina que genera todas las combinaciones posibles de las letras en una cadena.

---

## ✅ Requisitos

* Visual Studio 2022 o superior
* .NET 6.0 o superior
* SQL Server (local o remoto)
* Postman (para pruebas de API)
* Git (opcional para clonar)

---

## ⚙️ Estructura del Proyecto

```bash
Solution/
├── EmployeeManagementAPI/       # Proyecto Web API
├── EmployeeManagementWeb/        # Aplicación de escritorio
├── Exercise_3/   # Proyecto de consola
├── Exercise_4/   # Proyecto de consola
└── README.md
```

---

## 🧱 Punto 1 – API REST

* CRUD de empleados usando ADO.NET y LINQ.
* Conexión a SQL Server usando configuración desde `appsettings.json` o `.env`.
* Documentación disponible con Swagger (si habilitado).

### Endpoints incluidos:

| Método | URL                   | Descripción                |
| ------ | --------------------- | -------------------------- |
| GET    | `/api/employees`      | Listar todos los empleados |
| GET    | `/api/employees/{id}` | Obtener empleado por ID    |
| POST   | `/api/employees`      | Crear nuevo empleado       |
| PUT    | `/api/employees/{id}` | Actualizar empleado        |
| DELETE | `/api/employees/{id}` | Eliminar empleado          |

📄 *Incluye colección de Postman para pruebas (ver archivo JSON).*

---

### Ejemplo de request (Create):

```json
POST /api/employees
{
  "firstName": "Michael",
  "lastName": "Johnson",
  "address": "789 Pine St, Village",
  "phoneNumber": "555-555-5555",
  "dateOfBirth": "1988-03-10",
  "identificationNumber": "555555555"
}
```

🧪 **Postman Collection incluida** (ver archivo JSON en la raíz del repositorio).

---

## 🖥️ Punto 2 – Interfaz de Usuario

* Aplicación WinForms o WPF que interactúa con la API.
* Vistas:

  * Listar empleados
  * Crear empleado
  * Actualizar empleado
  * Eliminar empleado

---

## 🔢 Punto 3 – Números del 0 al 100 de 3 en 3 (sin bucles)

Uso de recursividad para imprimir en consola:

```
0
3
6
...
```

---

## 🔤 Punto 4 – Permutaciones de una Cadena

Dado un array como:

```csharp
string[] inputs = { "hat", "abc", "Zu6" };
```

Salida esperada:

```
1 aht,ath,hat,hta,tah,tha
2 abc,acb,bac,bca,cab,cba
3 6Zu,6uZ,Z6u,Zu6,u6Z,uZ6
```

---

## ▶️ Ejecución

1. Ejecutar el script SQL (si se incluye) para crear la base de datos.
2. Iniciar el proyecto `EmployeeManagementAPI`.
3. Probar endpoints con Postman o desde la interfaz de usuario.
4. Ejecutar `EmployeeManagementScripts` para ver puntos 3 y 4.

---

## 🔧 Variables de Entorno

### 📁 `.env.example` – Back (API)

```env
# Database Connection
DB_CONNECTION_STRING=Server=localhost;Database=YourDatabase;User Id=YourId;Password=YourPassword;Trusted_Connection=True;TrustServerCertificate=True;

# API Settings
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=http://localhost:5041

# Swagger Settings
SWAGGER_ENABLED=true
```

### 📁 `.env.example` – Front (Web)

```env
API_BASE_URL=http://localhost:5041/api/employees
```

---

## 💾 Script SQL de Creación

```sql
-- Create the database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'EmployeeManagement')
BEGIN
    CREATE DATABASE EmployeeManagement;
END
GO

USE EmployeeManagement;
GO

-- Create the Employees table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Employees')
BEGIN
    CREATE TABLE Employees (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        FirstName NVARCHAR(50) NOT NULL,
        LastName NVARCHAR(50) NOT NULL,
        Address NVARCHAR(200) NOT NULL,
        PhoneNumber NVARCHAR(20) NOT NULL,
        DateOfBirth DATETIME2 NOT NULL,
        IdentificationNumber NVARCHAR(20) NOT NULL,
        CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
        UpdatedAt DATETIME2 NULL,
        IsActive BIT NOT NULL DEFAULT 1
    );
END
GO

-- Seed initial data
IF NOT EXISTS (SELECT * FROM Employees)
BEGIN
    INSERT INTO Employees (FirstName, LastName, Address, PhoneNumber, DateOfBirth, IdentificationNumber, CreatedAt, IsActive)
    VALUES 
        ('John', 'Doe', '123 Main St, City', '555-123-4567', '1985-05-15', '123456789', GETDATE(), 1),
        ('Jane', 'Smith', '456 Oak Ave, Town', '555-987-6543', '1990-08-22', '987654321', GETDATE(), 1);
END
GO
```

---

## 🚀 Pasos de Instalación y Ejecución

```bash
# 1. Clonar el repositorio
git clone https://github.com/EdgarC97/EmployeeManagementTest
cd employee-management

# 2. Abrir solución en Visual Studio

# 3. Crear la base de datos
# Abrir SQL Server Management Studio y ejecutar:
scripts/create-database.sql

# 4. Configurar archivos .env si es necesario (copiar desde los .env.example)

# 5. Restaurar los paquetes
dotnet restore

# 6. Compilar y ejecutar la solución
dotnet build
dotnet run (en ambas terminales, una para la API otra para la Web) y tambien moverse dentro de los proyectos para los ejercicios 3 y 4
```

> ⚠️ Asegúrate de que la cadena de conexión (`DB_CONNECTION_STRING`) esté correctamente configurada.

---


## 📦 Extras

* \[✔] Código limpio y comentado
* \[✔] Separación de capas y responsabilidades

---
