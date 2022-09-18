using BluePrism.TechnicalTest;
using BluePrism.TechnicalTest.Common.Helper;
using BluePrism.TechnicalTest.Contracts.Dtos;
using BluePrism.TechnicalTest.Contracts.Interfaces.DictionaryProcessing;
using BluePrism.TechnicalTest.Contracts.Interfaces.Files;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    private static IFileService _fileService;
    private static IDictionaryProcessing _processingService;

    static Program()
    {
        //Initialization of the Host with all the necessary configurations for DI
        var builder = ProgramHostBuilder.CreateHostBuilder().Build();
        _fileService = builder.Services.GetService<IFileService>();
        _processingService = builder.Services.GetService<IDictionaryProcessing>();
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Blue Prims Technical Test");

        //Processing and Validation of the Console inputs
        //Start Word, EndWord, Result File Name and Dictionary URL
        var processInputDto = GetUserInputs(args);
        var isProcessInputDtoValid = processInputDto.Validate();

        if (!isProcessInputDtoValid) return;


        //Getting (read the dictionary file from the Url provided)
        //and validation of the dictionary
        var dictionaryOfWordsInput = _fileService.GetFileDataInformation(new FileInfo(processInputDto.DictionaryFileUrl));
        DictionaryOfWordsInputValidation(processInputDto.StartWord, processInputDto.EndWord, dictionaryOfWordsInput);

        var arrayOfWordsOutput = _processingService.ProcessWordDictionary(processInputDto.StartWord, processInputDto.EndWord, dictionaryOfWordsInput.ToHashSet());
        
        //Saving the result list to the txt file
        _fileService.SaveFileDataInformation(URLHelper.Url(processInputDto.DictionaryFileUrl, processInputDto.ResultFileUrl), arrayOfWordsOutput);
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
    /// <returns>The initialized <see cref="ProcessFileInputDto"/>.</returns>
    static ProcessFileInputDto GetUserInputs(string[] args)
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
            Console.WriteLine("Program needs four parameters:");
            Console.WriteLine("1 - Dictionary File");
            Console.WriteLine("2 - Start Word");
            Console.WriteLine("3 - End Word");
            Console.WriteLine("4 - Result File Name");
        }

        if (!string.IsNullOrEmpty(resultFileName) && !resultFileName.EndsWith(".txt"))
            resultFileName += ".txt";

        return new ProcessFileInputDto(dictionaryFilePath, startingWord, endWord, resultFileName);
    }
    static void DictionaryOfWordsInputValidation(string StartWord, string EndWord, IEnumerable<string> DictionaryOfWordsInput)
    {
        var dictionaryOfWordsInputValid = _processingService.ProcessWordDictionaryValidate(StartWord, EndWord, DictionaryOfWordsInput.ToHashSet());

        if (dictionaryOfWordsInputValid.Any())
        {
            Console.WriteLine("Dictionary is not Valid:");
            Console.WriteLine("Errors:");
            foreach (var item in dictionaryOfWordsInputValid) Console.WriteLine(item);

            return;
        }

    }
}