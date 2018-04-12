
  
# Facturama-SDK
Libreria para consumir la API Web de Facturama.

## Compatibilidad
* .Net Framework 4.5 ó superior

## Dependecias
* [Newtonsoft.Json](http://james.newtonking.com/json)
* [RestSharp](http://restsharp.org/)
* [System.ValueTuple](https://www.nuget.org/packages/System.ValueTuple/)

## Inicio Rapido

#### Instalación #####

Es recomendable utilizar [NuGet](http://docs.nuget.org) para instalar la librería. ó puedes hacer fork y modificar a tu conveniencia.
```net
Install-Package Facturama
```

#### Configuración  #####

Al no recibir otro parametro aparte de usuario y contraseña la libreria esta en modo sandbox
```cs
var facturama = new FacturamaApi("usuario", "contraseña");
```
y especificando la propiedad isDevelopment en false esta en modo producción
```cs
var facturama = new FacturamaApi("usuario", "contraseña", isDevelopment: false);
```

## Operaciones Web API

- Crear, Consultar Cancelar CFDI así como descargar XML, PDF y envió de
   estos por mail.
- Consultar Perfil y Suscripción actual
- Carga de Logo y Certificados Digitales
- CRUD de Productos, Clientes, Sucursales y Series.

Algunos ejemplos: [aquí](https://github.com/Facturama/facturama-dotnet-sdk/wiki/API-Web)

*Todas las operaciones son reflejadas en la plataforma web.*

## Operaciones API Multiemisor

- Crear, Consultar, Cancelar descarga de XML
- CRUD de CSD (Certificados de los Sellos Digitales).

Algunos ejemplos: [aquí](https://github.com/Facturama/facturama-dotnet-sdk/wiki/API-Multiemisor)

*Las operaciones no se reflejan en la plataforma web.*
