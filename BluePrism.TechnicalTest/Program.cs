using BluePrism.TechnicalTest.Common.Exceptions;
using BluePrism.TechnicalTest.Common.Helper;
using BluePrism.TechnicalTest.Contracts.Dtos;
using BluePrism.TechnicalTest.Contracts.Interfaces.DictionaryProcessing;
using BluePrism.TechnicalTest.Contracts.Interfaces.Dictionary;
using BluePrism.TechnicalTest.Infrastructure;
using BluePrism.TechnicalTest.Models;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text;
using BluePrism.TechnicalTest.Common.Constants;

public class Program
{
    private static IDictionaryDataService _fileService;
    private static IDictionaryProcessing _processingService;
    private static IValidator<ProcessFileInputRequest> _validator;
    private static ILogger _logger;
    static Program()
    {
        //Initialization of the Host with all the necessary configurations for DI
        var builder = ProgramHostBuilder.CreateHostBuilder().Build();
        _fileService = builder.Services.GetService<IDictionaryDataService>();
        _processingService = builder.Services.GetService<IDictionaryProcessing>();
        _validator = builder.Services.GetService<IValidator<ProcessFileInputRequest>>();
        _logger = builder.Services.GetService<ILogger<Program>>();

        _logger.LogInformation("Program initialization started with success.");
    }

    public static void Main(string[] args)
    {
        _logger.LogInformation("Program has started.");

        try
        {
            Process(args);
            _logger.LogInformation("Program has finished.");
        }
        catch (Exception ex)
        {
            if (ex.GetType().Equals(typeof(ArgumentInvalidException)))
            {
                StringBuilder errorMessage = new StringBuilder(ex.Message);
                _logger.LogError(errorMessage.ToString());
            }
            else
            {
                _logger.LogError(ex.Message);
                Console.WriteLine($"An error has ocurrred during the execution. See log file for more information at {LogConstants.LogFileDestination}.");
            }
            _logger.LogError("Program has finished.");
        }

        
    }

    static void Process(string[] args)
    {
       
        Console.WriteLine("Welcome to the Blue Prims Technical Test");

        //Processing and Validation of the Console inputs
        //Start Word, EndWord, Result File Name and Dictionary URL
        var processInputDto = GetUserInputs(args);
        var userInputValidate = _validator.Validate(processInputDto);

        if (!userInputValidate.IsValid)
        {
            StringBuilder errorMessage = new StringBuilder("Program has stopped due the fact of some inputs were wrongly filled in:");
            errorMessage.Append(Environment.NewLine).Append(string.Join("", userInputValidate.Errors.Select(a => a.ErrorMessage + Environment.NewLine).ToList()));
            _logger.LogError(errorMessage.ToString());
            Console.WriteLine(errorMessage.ToString());
            return;
        }
        //Getting (read the dictionary file from the Url provided)
        //and validation of the dictionary
        var processFileInputDto = new ProcessFileInputDto() { StartWord = processInputDto.StartWord, EndWord = processInputDto.EndWord };
        processFileInputDto.WordsDictionary = _fileService.GetDictionaryData(new FileGetDataInformationDto() { File = new FileInfo(processInputDto.DictionaryFileUrl) });

        var dictionaryOfWordsInputValidate = _processingService.ProcessWordDictionaryValidate(processFileInputDto);

        if (!dictionaryOfWordsInputValidate.IsValid)
        {
            StringBuilder errorMessage = new StringBuilder("Program has stopped due the fact of the information could not be processed.");
            errorMessage.Append(Environment.NewLine).Append(string.Join("", dictionaryOfWordsInputValidate.Errors.Select(a => a.ErrorMessage + Environment.NewLine).ToList()));
            _logger.LogError(errorMessage.ToString());
            Console.WriteLine(errorMessage.ToString());
            return;
        }

        _logger.LogInformation("Processing has began...");

        var arrayOfWordsOutput = _processingService.ProcessWordDictionary(processFileInputDto);
        _logger.LogInformation("Processing has finished and will be saved...");

        var savingUrl = URLHelper.Url(processInputDto.DictionaryFileUrl, processInputDto.ResultFileUrl);
        //Saving the result list to the txt file
        _fileService.SaveDictionaryResultData(new FileSaveDataInformationDto() { File = savingUrl, DataInformation = arrayOfWordsOutput });
        Console.WriteLine($"File saved at {savingUrl.FullName} with success.");

        _logger.LogInformation("Processing has finished successfully.");
    }


    /// <summary>
    /// This method is responsible for the ProcessFileInputDto object. This object contains all the information for the processing like
    /// <para>Dictionary, Start and End Word and the Result File Name.</para>
    /// <para>If this method receive args filled in Its using that information. If not the program will ask the user for all the information.<para>
    /// </summary>
    /// <param name="args">The command line args. Must have 4 parameters
    /// <list type="bullet">
    /// <listheader>
    /// <term>Position</term>
    /// <descripton>Description</descripton>
    /// </listheader>
    /// <item>
    /// <term>[0] - Dictionary</term>
    /// <description>The URL for the Word Dictionary</description>
    /// </item>
    /// <item>
    /// <term>[1] - Start Word</term>
    /// <description>Word to Start the Process</description>
    /// </item>
    /// <item>
    /// <term>[2] - End Word</term>
    /// <description>Word to End the Process</description>
    /// </item>
    /// <item>
    /// <term>[3] - Result File Name</term>
    /// <description>Name of the result file. If the name doesn't have the TXT extension the process will included it.</description>
    /// </item>
    /// </list>>
    /// </param>
    /// <returns>The initialized <see cref="ProcessFileInputRequest"/>.</returns>
    static ProcessFileInputRequest GetUserInputs(string[] args)
    {

        var dictionaryFilePath = string.Empty;
        var startingWord = string.Empty;
        var endWord = string.Empty;
        var resultFileName = string.Empty;


        if(args == null || !args.Any())
        {
            Console.WriteLine("Dictionary File Path: ");
            dictionaryFilePath = Console.ReadLine();
            Console.WriteLine("Which is the Starting Word: ");
            startingWord = Console.ReadLine();
            Console.WriteLine("Which is the End Word: ");
            endWord = Console.ReadLine();
            Console.WriteLine("What's the Result File Name: ");
            resultFileName = Console.ReadLine();
        }
        else if(args != null && args.Length == 4)
        {
            dictionaryFilePath = args[0];
            startingWord = args[1];
            endWord = args[2];
            resultFileName = args[3];
        }
        else
        {
            _logger.LogError("Program needs four parameters:");
            _logger.LogError("1 - Dictionary File");
            _logger.LogError("2 - Start Word");
            _logger.LogError("3 - End Word");
            _logger.LogError("4 - Result File Name");
        }

        if (!string.IsNullOrEmpty(resultFileName) && !resultFileName.EndsWith(".txt"))
            resultFileName += ".txt";

        return new ProcessFileInputRequest(dictionaryFilePath, startingWord, endWord, resultFileName);
    }

}