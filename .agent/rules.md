# Reglas del Proyecto BibliotecaDigital

## 🌐 Idioma de Comunicación
- **Toda la comunicación debe realizarse en español**
- Los comentarios en el código, documentación y mensajes deben estar en español
- Las respuestas del asistente siempre serán en español

## 🏗️ Arquitectura del Proyecto
Este proyecto sigue los principios de **Clean Architecture** y debe mantenerse así en todo momento.

### Estructura de Capas
El proyecto está organizado en las siguientes capas, y esta estructura **NO debe alterarse**:

1. **Domain** (`BibliotecaDigital.Domain`)
   - Contiene las entidades del dominio
   - Contiene los objetos de valor (Value Objects)
   - Contiene las excepciones del dominio
   - **NO debe tener dependencias de otras capas**
   - **NO debe tener dependencias de frameworks externos**

2. **Application** (`BibliotecaDigital.Application`)
   - Contiene la lógica de aplicación
   - Puede depender únicamente de la capa Domain
   - **NO debe depender de Infrastructure ni API**

3. **Infrastructure** (`BibliotecaDigital.Infrastructure`)
   - Implementa la persistencia de datos (Entity Framework Core)
   - Implementa la identidad (ASP.NET Identity)
   - Puede depender de Domain y Application
   - Contiene las implementaciones concretas de interfaces

4. **API** (`BibliotecaDigital.API`)
   - Capa de presentación (ASP.NET Core Web API)
   - Punto de entrada de la aplicación
   - Puede depender de todas las demás capas
   - Configura la inyección de dependencias

### Principios a Respetar
- ✅ **Dependency Rule**: Las dependencias deben apuntar hacia adentro (hacia el Domain)
- ✅ **Separation of Concerns**: Cada capa tiene una responsabilidad específica
- ✅ **Dependency Inversion**: Depender de abstracciones, no de implementaciones concretas
- ❌ **NO crear dependencias circulares entre capas**
- ❌ **NO mezclar responsabilidades entre capas**

## 📦 Gestión de Dependencias

### Regla Crítica
**NO se deben modificar las versiones de las dependencias existentes sin autorización explícita del usuario**

### Dependencias Actuales
Las versiones de los paquetes NuGet instalados deben mantenerse tal como están:
- Entity Framework Core
- ASP.NET Core Identity
- MySQL Connector
- Cualquier otra dependencia del proyecto

### Agregar Nuevas Dependencias
- ✅ Se pueden sugerir nuevas dependencias si son necesarias
- ⚠️ Siempre consultar con el usuario antes de agregar nuevas dependencias
- ⚠️ Justificar por qué la nueva dependencia es necesaria
- ✅ Asegurarse de que la nueva dependencia sea compatible con la arquitectura

## 🔧 Modificaciones Permitidas

### ✅ Permitido
- Agregar nuevas entidades en la capa Domain
- Agregar nuevos casos de uso en la capa Application
- Implementar nuevos repositorios en Infrastructure
- Agregar nuevos controladores/endpoints en API
- Refactorizar código manteniendo la arquitectura
- Agregar pruebas unitarias e integración
- Mejorar la documentación del código

### ❌ NO Permitido
- Cambiar versiones de dependencias sin autorización
- Romper la estructura de Clean Architecture
- Crear dependencias que violen la Dependency Rule
- Mover código entre capas de manera incorrecta
- Agregar lógica de negocio en las capas Infrastructure o API
- Agregar dependencias de frameworks en la capa Domain

## 📝 Convenciones de Código

### Nomenclatura
- Usar nombres descriptivos en español o inglés según el contexto
- Las entidades deben estar en singular (ej: `Libro`, no `Libros`)
- Los repositorios deben seguir el patrón `I{Entidad}Repository`
- Los servicios deben tener nombres descriptivos de su función

### Organización
- Mantener los archivos organizados por funcionalidad
- Agrupar clases relacionadas en carpetas apropiadas
- Seguir el principio de Single Responsibility

## 🚀 Base de Datos
- El proyecto usa **MySQL** como base de datos
- Se utiliza **Entity Framework Core** para el acceso a datos
- Las migraciones deben estar en la capa Infrastructure
- **NO cambiar el proveedor de base de datos sin autorización**

## 🔐 Seguridad e Identidad
- Se utiliza **ASP.NET Core Identity** para autenticación y autorización
- Los usuarios deben tener email único
- La configuración de Identity está en la capa Infrastructure

## ⚠️ Antes de Realizar Cambios Importantes
Siempre consultar con el usuario antes de:
1. Modificar la estructura del proyecto
2. Cambiar versiones de dependencias
3. Agregar nuevas dependencias externas
4. Cambiar el proveedor de base de datos
5. Modificar la configuración de Identity
6. Realizar cambios que afecten múltiples capas

---

**Recordatorio**: Estas reglas están diseñadas para mantener la integridad, consistencia y calidad del proyecto BibliotecaDigital. Cualquier excepción debe ser discutida y aprobada explícitamente.
