using Microsoft.EntityFrameworkCore;
using BibliotecaLibro.Server.Repository;
using BibliotecaLibro.Server.Data;

namespace BibliotecaLibro.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Controllers
            builder.Services.AddControllers();

            // DbContext
            builder.Services.AddDbContext<BibliotecaDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // AutoMapper 12.x
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Repos
            builder.Services.AddScoped<UsuarioRepositorio>();
            builder.Services.AddScoped<LibroRepositorio>();

            // CORS (abrir todo; afinar en prod si tenés frontend web)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                // Validar config de AutoMapper sin colisión de IConfigurationProvider
                using var scope = app.Services.CreateScope();
                var mapperCfg = scope.ServiceProvider.GetRequiredService<AutoMapper.IConfigurationProvider>();
                mapperCfg.AssertConfigurationIsValid();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAllOrigins");
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}
