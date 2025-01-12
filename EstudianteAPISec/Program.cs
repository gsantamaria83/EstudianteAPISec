using Estudiante.Application.Interfaces;
using Estudiante.Application.Services;
using Estudiante.Repositories.Interfaces;
using Estudiante.Repositories.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Permite la URL del frontend
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEstudianteService, EstudianteService>();
builder.Services.AddScoped<IEstudianteRepository, EstudianteRepository>();

var app = builder.Build();
app.UseCors("AllowSpecificOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
