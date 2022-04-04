# Facturama dotnet  SDK
  
> [NOTE] This document is also available in [English].
>
> Librería para consumir la API Web y API Multiemisor de [Facturama](https://api.facturama.mx/).
>
> Puedes consultar la guía completa de la [API](https://apisandbox.facturama.mx/guias).
## Crear cuenta de usuario

> Crear una cuenta de usuario en el ambiente de prueba [Sandbox](https://dev.facturama.mx/api/login) 
>
> Para API Web, realiza la configuración básica usando RFC de pruebas **"EKU9003173C9"**, más información [aquí](https://apisandbox.facturama.mx/guias/perfil-fiscal).
>
> Sellos digitales de prueba (CSD), [aquí](https://github.com/rafa-dx/facturama-CSD-prueba). 


### Compatibilidad
* .Net Framework 4.5 ó superior

## Dependecias
* [Newtonsoft.Json](http://james.newtonking.com/json)
* [RestSharp](http://restsharp.org/)
* [System.ValueTuple](https://www.nuget.org/packages/System.ValueTuple/)

## Inicio Rapido

## Instalación 

Es recomendable utilizar [NuGet](http://docs.nuget.org) para instalar la librería. ó puedes hacer fork y modificar a tu conveniencia.
```net
Install-Package Facturama
```

### Configuración  

Al no recibir otro parametro aparte de usuario y contraseña la libreria esta en modo sandbox
```cs
var facturama = new FacturamaApi("usuario", "contraseña");
```
y especificando la propiedad isDevelopment en false esta en modo producción
```cs
var facturama = new FacturamaApi("usuario", "contraseña", isDevelopment: false);
```

## API Web

> Creación de CFDIs con un único emisor, (el propietario de la cuenta, cuyo perfil fiscal se tiene configurado)
> 
> *Todas las operaciones son reflejadas en la plataforma web.*
### Operaciones API Web
- Crear, Consultar Cancelar CFDI así como descargar XML, PDF y envió de estos por mail.
- Consultar Perfil y Suscripción actual.
- Carga de Logo y Certificados Digitales.
- CRUD de Productos, Clientes, Sucursales y Series.

Algunos ejemplos: [aquí](https://github.com/Facturama/facturama-dotnet-sdk/wiki/API-Web)


## API Multiemisor

> Creacion de CFDIs con multiples emisores.
>
> *Las operaciones NO se reflejan en la plataforma web.*
### Operaciones API Multiemisor

- Crear, Consultar, Cancelar descarga de XML.
- CRUD de CSD (Certificados de los Sellos Digitales).
- 
Algunos ejemplos: [aquí](https://github.com/Facturama/facturama-dotnet-sdk/wiki/API-Multiemisor)

[English]: ./README-en.md
