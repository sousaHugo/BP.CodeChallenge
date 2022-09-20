using BluePrism.TechnicalTest.Contracts.Dtos;
using BluePrism.TechnicalTest.Contracts.Interfaces.DictionaryProcessing;
using BluePrism.TechnicalTest.Contracts.Interfaces.Dictionary;
using BluePrism.TechnicalTest.Files;
using BluePrism.TechnicalTest.Models;
using BluePrism.TechnicalTest.Services.Dictionary;
using BluePrism.TechnicalTest.Services.Processing;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using BluePrism.TechnicalTest.Common.Constants;

namespace BluePrism.TechnicalTest.Infrastructure
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
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Error().MinimumLevel.Information()
                .WriteTo.File(LogConstants.LogFileDestination)
            .CreateLogger();

            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.SetBasePath(Directory.GetCurrentDirectory());

                }).ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IFileOperation, FileOperation>();
                    services.AddScoped<IDictionaryDataService, DictionaryDataService>();
                    services.AddScoped<IDictionaryProcessing, DictionaryProcessingService>();
                    services.AddScoped<IValidator<ProcessFileInputRequest>, ProcessFileInputRequestValidator>();
                    services.AddScoped<IValidator<ProcessFileInputDto>, ProcessFileInputValidator>();
                    services.AddScoped<IValidator<FileGetDataInformationDto>, FileGetDataInformationValidator>();
                    services.AddScoped<IValidator<FileSaveDataInformationDto>, FileSaveDataInformationValidator>();
                })
                .ConfigureLogging((_, logging) => logging.ClearProviders().AddConsole()).UseSerilog();



            return hostBuilder;
        }
    }
}
