# 📚 Biblioteca – Gestión de Inventario

Este proyecto es una aplicación de **gestión de inventario para una biblioteca**, diseñada con una arquitectura en capas que promueve la **separación de responsabilidades**.  

El sistema permite la manipulación de dos entidades principales:  

- 👤 **Usuario** (gestión de usuarios de la biblioteca).  
- 📖 **Libro** (gestión de inventario de libros).  

La aplicación está construida en **.NET (C#)** con soporte para **API REST**, consumo desde interfaz de usuario y conexión a **base de datos SQL Server**.  

---

## 🚀 Características principales

- CRUD completo (**Crear, Leer, Actualizar, Eliminar**) para **Usuarios** y **Libros**.  
- Arquitectura por capas para facilitar el mantenimiento y escalabilidad.  
- Separación clara entre **interfaz, servicios, controladores, repositorios y base de datos**.  
- Uso de **Entity Framework Core** para el mapeo objeto-relacional (ORM).  
- Exposición de endpoints REST mediante **Web API**.  
- Documentación interactiva con **Swagger**.  

---

## 🏗️ Arquitectura del Proyecto

La aplicación sigue el patrón de **separación de responsabilidades**, dividiendo el sistema en capas:  

1. **🎨 Interfaz de Usuario (UI)**  
   - Frontend implementado con **Blazor** (o HTML/JS según módulo).  
   - Formularios para la gestión de usuarios y libros.  

2. **🛠️ Servicios (Services)**  
   - Contienen la lógica de negocio.  
   - Se comunican con los repositorios para acceder a los datos.  

3. **🎮 Controladores (Controllers)**  
   - Exponen la **API REST**.  
   - Reciben solicitudes del cliente y devuelven respuestas en JSON.  

4. **📦 Repositorios (Repositories)**  
   - Encargados de interactuar directamente con la base de datos.  
   - Implementan los métodos CRUD utilizando **Entity Framework Core**.  

5. **🗄️ Base de Datos (SQL Server)**  
   - Tablas principales:  
     - `Usuario`  
     - `Libro`  

---

## 📊 Modelo de Datos

### Usuario  
- `IdUsuario` (int, PK)  
- `NombreUsuario` (string)  
- `Contraseña` (string)  
- `Privilegio` (string)  
- Otros campos complementarios (ej: correo, imagen, etc.)  

### Libro  
- `IdLibro` (int, PK)  
- `Titulo` (string)  
- `Autor` (string)  
- `Genero` (string)  
- `Cantidad` (int)  

---

## ⚙️ Tecnologías utilizadas

- **C# / .NET 8** – Lenguaje y framework principal.  
- **ASP.NET Core Web API** – Creación de endpoints REST.  
- **Entity Framework Core** – ORM para acceso a datos.  
- **SQL Server** – Base de datos relacional.  
- **Swagger** – Documentación y pruebas de la API.  
- **Blazor / HTML-CSS-JS** – Interfaz de usuario.  

---

## 📂 Estructura del Proyecto

```bash
/Biblioteca
│── Biblioteca.Client     # Interfaz de usuario (Blazor/Frontend)
│── Biblioteca.Server     # API (Controllers, Configuración, DbContext)
│── Biblioteca.Shared     # Modelos compartidos (DTOs, entidades)
│
├── Controllers/          # Controladores API
├── Services/             # Servicios de negocio
├── Repository/           # Repositorios de datos
├── Models/               # Entidades (Usuario, Libro)
└── Data/                 # DbContext y configuración de base de datos


El proyecto funciona en local
