using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Comentar/Borrar la siguiente línea e incluir el servicio AddControllerWithViews y AddRazorPages
//builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //Configuración para el uso de Blazor
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();

app.UseAuthorization();
//Configuración para el uso de Blazor
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.MapControllers();
app.MapFallbackToFile("index.html");

//Comentar o borrar la siguiente línea
//app.MapControllers();

app.Run();
