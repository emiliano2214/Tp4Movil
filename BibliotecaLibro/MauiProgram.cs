using Microsoft.Extensions.Logging;
using System.Net.Http;
using System;

namespace BibliotecaLibro
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            static string ResolveApiBaseUrl()
            {
#if DEBUG
#if ANDROID
                // Emulador Android hablando con el host local
                return "https://10.0.2.2:7258/api/";
#else
                // Windows/Mac en desarrollo
                return "https://localhost:7258/api/";
#endif
#else
                // Producción en Somee (sin :7258)
                return "https://bibliotecalibro.somee.com/api/";
#endif
            }

            builder.Services.AddSingleton(sp =>
            {
                var baseUrl = ResolveApiBaseUrl();

#if ANDROID && DEBUG
                // Solo para desarrollo: aceptar el certificado dev local
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (msg, cert, chain, errors) => true
                };
                return new HttpClient(handler) { BaseAddress = new Uri(baseUrl) };
#else
                return new HttpClient { BaseAddress = new Uri(baseUrl) };
#endif
            });

            // Servicios que usan HttpClient
            builder.Services.AddSingleton<BibliotecaLibro.Servicios.LibroServicio>();
            builder.Services.AddSingleton<BibliotecaLibro.Servicios.UsuarioServicio>();

            return builder.Build();
        }
    }
}
