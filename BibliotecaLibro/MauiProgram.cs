using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using BibliotecaLibro.Servicios;

namespace BibliotecaLibro
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(f => f.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"));

            builder.Services.AddMauiBlazorWebView();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            const string PORT_HTTPS = "7258";

            static string ResolveApiBaseUrl()
            {
#if DEBUG
#if ANDROID
                // 👈 desde el emulador Android, "localhost" es el emulador; usa 10.0.2.2 para llegar a tu PC
                return $"https://10.0.2.2:{PORT_HTTPS}/api/";
#else   // WINDOWS / MACCATALYST
                return $"https://localhost:{PORT_HTTPS}/api/";
#endif
#else
                return "https://bibliotecalibro.somee.com/api/";
#endif
            }

            builder.Services.AddSingleton(sp =>
            {
                var baseUrl = ResolveApiBaseUrl();

#if DEBUG
                // Aceptar certificado de desarrollo (CN=localhost) que no coincide con 10.0.2.2 en Android
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (HttpRequestMessage _, X509Certificate2? __, X509Chain? ___, SslPolicyErrors ____) => true
                };
                return new HttpClient(handler) { BaseAddress = new Uri(baseUrl) };
#else
                return new HttpClient { BaseAddress = new Uri(baseUrl) };
#endif
            });

            builder.Services.AddSingleton<LibroServicio>();
            builder.Services.AddSingleton<UsuarioServicio>();

            return builder.Build();
        }
    }
}
