using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Comentar/Borrar la siguiente l�nea e incluir el servicio AddControllerWithViews y AddRazorPages
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
    //Configuraci�n para el uso de Blazor
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();

app.UseAuthorization();
//Configuraci�n para el uso de Blazor
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.MapControllers();
app.MapFallbackToFile("index.html");

//Comentar o borrar la siguiente l�nea
//app.MapControllers();

app.Run();
