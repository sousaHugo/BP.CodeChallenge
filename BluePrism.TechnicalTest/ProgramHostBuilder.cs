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
        public static IHostBuilder CreateHostBuilder(string[] args)
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
