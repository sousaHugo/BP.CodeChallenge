using BluePrism.TechnicalTest;
using BluePrism.TechnicalTest.Common.Helper;
using BluePrism.TechnicalTest.Dtos;
using BluePrism.TechnicalTest.Services.Files;
using BluePrism.TechnicalTest.Services.Processing;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    private static IFileService _fileService;
    private static IDictionaryProcessing _processingService;
    static void Main(string[] args)
    {
        Init(args);
        Console.WriteLine("Welcome to the Blue Prims Technical Test");

        //Processing and Validation of the Console inputs
        //Start Word, EndWord, Result File Name and Dictionary URL
        var processInputDto = GetUserInputs();
        var isProcessInputDtoValid = processInputDto.Validate();

        if (!isProcessInputDtoValid) return;


        //Getting and validation of the dictionary
        var dictionaryOfWordsInput = _fileService.GetFileDataInformation(new FileInfo(processInputDto.DictionaryFileUrl));
        DictionaryOfWordsInputValidation(processInputDto.StartWord, processInputDto.EndWord, dictionaryOfWordsInput);

        var arrayOfWordsOutput = _processingService.findLadders(processInputDto.StartWord, processInputDto.EndWord, dictionaryOfWordsInput.ToHashSet());

        //Saving the result list to the txt file
        _fileService.SaveFileDataInformation(URLHelper.Url(processInputDto.DictionaryFileUrl, processInputDto.ResultFileUrl), arrayOfWordsOutput.SelectMany(a => a));
    }
    static void Init(string[] args)
    {
        var builder = ProgramHostBuilder.CreateHostBuilder(args).Build();
        _fileService = builder.Services.GetService<IFileService>();
        _processingService = builder.Services.GetService<IDictionaryProcessing>();
    }
    static ProcessFileInputDto GetUserInputs()
    {
        Console.WriteLine("Dictionary File Path: ");
        var dictionaryFilePath = Console.ReadLine();
        Console.WriteLine("Which is the Starting Word: ");
        var startingWord = Console.ReadLine();
        Console.WriteLine("Which is the End Word: ");
        var endWord = Console.ReadLine();
        Console.WriteLine("What's the Result File Name: ");
        var resultFileName = Console.ReadLine();

        if (!resultFileName.EndsWith(".txt"))
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