using System.Text.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BibliotecaLibro.Server.Data;
using BibliotecaLibro.Server.Repository;
using BibliotecaLibro.Server.Mapping;

namespace BibliotecaLibro.Server
{
    public class Program
    {
        private const string CorsPolicy = "AllowAllOrigins";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers()
                .AddJsonOptions(o => o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);

            var provider = builder.Configuration["DatabaseProvider"] ?? "Local";
            var connectionString = provider switch
            {
                "Local" => builder.Configuration.GetConnectionString("LocalConnection"),
                "Remote" => builder.Configuration.GetConnectionString("RemoteConnection"),
                _ => throw new Exception("Proveedor de base de datos inválido")
            } ?? throw new Exception("Connection string no configurada");

            builder.Services.AddDbContext<BibliotecaDbContext>(opt =>
                opt.UseSqlServer(connectionString, sql => sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)));

            // AutoMapper: registrar UNA sola vez apuntando a los assemblies de tus profiles
            builder.Services.AddAutoMapper(
                typeof(ProfileLibro).Assembly,
                typeof(UsuarioProfile).Assembly
            );

            builder.Services.AddScoped<UsuarioRepositorio>();
            builder.Services.AddScoped<LibroRepositorio>();

            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy(CorsPolicy, p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                // Validación de perfiles (si algo sigue mal, te dice cuál)
                using var scope = app.Services.CreateScope();
                var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
                mapper.ConfigurationProvider.AssertConfigurationIsValid();
            }

            if (builder.Configuration.GetValue("UseHttpsRedirection", defaultValue: app.Environment.IsDevelopment()))
                app.UseHttpsRedirection();

            app.UseCors(CorsPolicy);
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
