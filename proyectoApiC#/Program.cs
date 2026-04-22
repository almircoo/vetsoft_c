using Microsoft.EntityFrameworkCore;
using proyectoApiC_.Data;
using proyectoApiC_.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// DB Context configuration
var connectionString = builder.Configuration.GetConnectionString("DB_VETERINARIA");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// Register services
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<ServicioService>();
builder.Services.AddScoped<VeterinarioService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<CitaService>();

// CORS configuration (equivalent to SecurityConfig.java)
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .WithOrigins("http://localhost:4200", "http://localhost:3000", "http://localhost:8080")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Apply CORS
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
