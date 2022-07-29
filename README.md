# Buena Voluntad - OrganizacionesApp
Plataforma para la gestión de oportunidades de voluntariado - Módulo Voluntarios.

Este repositorio fue creado para contener la información relacionada con el desarrollo de la plataforma Buena Voluntad.

# Descripción

La plataforma Buena Voluntad consiste de tres partes fundamentales:

Módulo Voluntarios: Aplicación orientada a los voluntarios, permite la postulación a las oportunidades de voluntariado.  Link: https://github.com/andrescheroni/VoluntariosApp
Módulo Organizaciones: Aplicación orientada a las organizaciones, permite la creación de oportunidades de voluntariado y la gestión de las postulaciones. Link: https://github.com/andrescheroni/OrganizacionesApp
Módulo API: Controla el acceso a la base de datos. Link: https://github.com/andrescheroni/VoluntariosAPI

# Status

% Completado: 80%

# Instrucciones de instalación

- Base de datos

Instalar Microsoft SQL Server 2019. Descargar el archivo db.sql ubicado en el repositorio del Módulo API y ejecutarlo en el servidor local para crear la base de datos. Crear un usuario para la base de datos con las siguientes credenciales:

U: VoluntarioAPI
P: voluntarioAPI1

- API

Clonar el repositorio del Módulo API y compilar. La API debe estar en ejecución durante el uso de las aplicaciones. Para habilitar la conexión a servicios web locales verificar el siguiente artículo: https://docs.microsoft.com/en-us/xamarin/cross-platform/deploy-test/connect-to-local-web-services

- Aplicación Organizaciones

Clonar el repositorio del Módulo Organizaciones y compilar. Ejecutar la aplicación en el emulador de Android.

- Aplicación Voluntarios

Clonar el repositorio del Módulo Voluntarios y compilar. Ejecutar la aplicación en el emulador de Android.

# Video
El siguiente link contiene un video con una breve demostración del uso de la aplicaicón:

https://youtu.be/p8ik4LbSvD8

