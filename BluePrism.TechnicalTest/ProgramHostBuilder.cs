using BluePrism.TechnicalTest.Contracts.Interfaces.DictionaryProcessing;
using BluePrism.TechnicalTest.Contracts.Interfaces.Files;
using BluePrism.TechnicalTest.Files;
using BluePrism.TechnicalTest.Services.Files;
using BluePrism.TechnicalTest.Services.Processing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BluePrism.TechnicalTest
{
    public static class ProgramHostBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HostBuilder"/> class with pre-configured defaults.
        /// Besides that this method register the DI for the necessary classes, like for FileOperation and for the Services
        /// </summary>
        /// <param name="args">The command line args.</param>
        /// <returns>The initialized <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.SetBasePath(Directory.GetCurrentDirectory());

                }).ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IFileOperation, FileOperation>();
                    services.AddScoped<IFileService, FileService>();
                    services.AddScoped<IDictionaryProcessing, DictionaryProcessing>();
                });

            return hostBuilder;
        }
    }
}
