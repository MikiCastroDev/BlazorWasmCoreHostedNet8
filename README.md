# BlazorWasmCoreHostedNet8

## Presentación
Hasta la versión 7 de .NET se nos permitía generar proyectos Blazor donde tanto el cliente como el servidor se interconectaban (al igual que un proyecto compartido para Modelos y otros) mediante una plantilla que el propio visual studio nos incluía. A partir de la versión 8 de .NET es necesario generar desde el principio. Este proyecto pretende servir de guía para ese proceso.

El proyecto es el resultado final de seguir los próximos pasos y permite ser una línea común la cual, no siempre es necesario seguir totalmente. Por ejemplo, en ocasiones, el proyecto Shared no es necesario para nuestras ideas finales.

**NOTA:** A partir de este momento entendemos que la versión del framework a usar sera .NET 8

## Pasos a seguir.
1. Creamos una aplicación Blazor Web App, la que sera a partir de ahora nuestra aplicación cliente.
2. Creamos en la misma solución un proyecto AspNetCore Web Api
3. **OPCIONAL:** Creamos un proyecto de clases el cúal sera nuestro proyecto Shared
4. Selecionamos nuestro proyecto Server como proyecto de Inicio de Solución (Startup project)
**NOTA:** Al iniciar el proyecto con el proyecto Client como proyecto de inicio se iniciara la aplicación Blazor por defecto del cliente sin conexiones al server. Los datos se obtienen del fichero .json localizado en la carpeta wwwroot. Al configurar el proyecto Server como inicio, se abrira por defecto la Pantalla de Swagger por defecto creada por el proyecto Server/API
5. Establecemos las referencias entre los distintos proyectos. En el servidor el proyecto Client y Shared (si existe). Y en el proyecto Client la referencia a Shared. Recordar que estas referencias dependeran de la arquitectura que queramos tomar, es decir, si por ejemplo los modelos se incluyen dentro del proyecto Client no necesitaremos referencia a Shared (salvo que Shared incluya otros datos que necesitemos)
6. En el proyecto Server, necesitamos agregar el paquete Nuget Microsoft.AspNetCore.Components.WebAssembly.Server de la versión correspondiente a framework de .NET que usemos.
7. Ahora relizamos las configuracciones oportunas dentro del fichero Program.cs de proyecto Server
7.1.1 Sustitumos la línea
    app.Services.AddContollers
7.1.2 Por las siguientes líneas para
    builder.Services.AddControllersWithViews();
    builder.Services.AddRazorPages();
8. Dentro del bloque de configuración para el entorno de desarrollo (IsDevelopment()) agregar la línea
app.UseWebAssemblyDebugging();
9. Despúes de la línea app.UseAuthorizathion() agregamos las siguientes líneas
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.MapControllers();
app.MapFallbackToFile("index.html");
10. Comentamos o borramos la línea app.MapControllers();
11. Ahora configuraremos en el proyecto servidor las launchSettings haciendo que los perfiles que lanzan el proyecto, dejen de iniciar la herramiento swagger y por defecto, utilicen las mismas
rutas que los proyecto con versiones 7 y anteriores lanzaban. Para ello en el fichero launchSettings.json necesitamos cambiar LAS líneas (3) launchUrl : "swagger" por la siguiente

"inspectUri": "{wsProtocol}: //{url.hostname}:{url.port}/_framework/debug/ws-proxy?browser={browserInspectUri}",
12. Ahora para comprobar que todo es correcto, en el proyecto cliente cambiaremos la obtención de datos del fichero json a las peticiones api del servidor. Para esto, procedemos a modificar
el archivo Weather.razor dentro de la carpeta /Components/Pages del proyecto cliente el cual esta obteniendo los datos del fichero weather.json de la carpeta wwwroot del propio cliente. Buscamos
la línea donde se ingesta la variable forecast (que es la que posteriormente se renderizara) y cambiamos la ruta a la petición http de la actual a "api/WeatherForecast" que es la ruta de la petición http

**RECONOMIENTO** El actual proyecto se a generado siguiendo el vídeo de Netcode-Hub al cual se puede acceder desde el siguiente enlace: https://www.youtube.com/watch?v=zch_DI_pXmE