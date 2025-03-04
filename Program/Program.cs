using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Program.Forms;
using Program.Services.Implementations;
using Program.Services.Interfaces;

namespace Program
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        /// 
        public static IServiceProvider ServiceProvider { get; private set; }
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            // Configurar la inyección de dependencias
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            // Iniciar la aplicación con Form1 inyectado automáticamente
            Application.Run(ServiceProvider.GetRequiredService<MainForm>());
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            // Registrar interfaces y clases concretas en el contenedor de DI
            services.AddScoped<ITcpServerHelper, TcpServerHelper>();
            services.AddScoped<ITcpClientHelper, TcpClientHelper>();

            // Registrar los formularios para que también sean gestionados por el contenedor
            services.AddTransient<MainForm>();
            //services.AddTransient<Form2>();
        }
    }
}
