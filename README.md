# Nombre del Proyecto

## Descripción
Este proyecto es una aplicación web desarrollada utilizando Visual Studio con tecnologías como C#, Angular y MVC. Proporciona.

## Requisitos Previos
Antes de ejecutar la aplicación, asegúrate de tener instalados los siguientes componentes:

- [Visual Studio](https://visualstudio.microsoft.com/)
- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli)

## Instrucciones de Configuración

### Configuración del Proyecto C# (Backend)
1. Abre la solución en Visual Studio.
2. Asegúrate de tener la cadena de conexión de la base de datos configurada correctamente en `appsettings.json`.
3. Ejecuta la migración de la base de datos para crear las tablas necesarias:
    ```bash
    dotnet ef database update
    ```
4. Inicia la aplicación C#.

### Configuración del Proyecto Angular (Frontend)
1. Navega al directorio `ClientApp` en la terminal.
    ```bash
    cd YourProjectName/ClientApp
    ```
2. Instala las dependencias de Node.js.
    ```bash
    npm install
    ```
3. Inicia la aplicación Angular.
    ```bash
    ng serve
    ```

La Web estará disponible en `http://localhost:4200/`.
La Api estará disponible en `http://localhost:5258/api`.
