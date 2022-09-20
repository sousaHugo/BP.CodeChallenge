using BluePrism.TechnicalTest.Common.Exceptions;
using BluePrism.TechnicalTest.Contracts.Dtos;
using BluePrism.TechnicalTest.Contracts.Interfaces.Dictionary;
using BluePrism.TechnicalTest.Files;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace BluePrism.TechnicalTest.Services.Dictionary
{
    public class DictionaryDataService : IDictionaryDataService
    {
        private readonly IFileOperation _fileOperation;
        private readonly IValidator<FileGetDataInformationDto> _validatorGet;
        private readonly IValidator<FileSaveDataInformationDto> _validatorSave;
        private static ILogger _logger;

        public DictionaryDataService(IFileOperation FileOperation, IValidator<FileGetDataInformationDto> ValidatorGet, IValidator<FileSaveDataInformationDto> ValidatorSave, ILogger<DictionaryDataService> Logger)
        {
            _fileOperation = FileOperation;
            _validatorGet = ValidatorGet;
            _validatorSave = ValidatorSave;
            _logger = Logger;
        }

        ///<inheritdoc cref="IDictionaryDataService.GetDictionaryData(FileGetDataInformationDto)"/>
        public IEnumerable<string> GetDictionaryData(FileGetDataInformationDto FileGetDataDto)
        {
            _logger.LogInformation("Getting Dictionary...");

            var validationResult = _validatorGet.Validate(FileGetDataDto);
            if(!validationResult.IsValid)
                throw new ArgumentInvalidException("Read File Validation",validationResult.Errors.Select(a => new ArgumentInvalidException(a.ErrorMessage)));

            return _fileOperation.Get(FileGetDataDto.File.FullName);
        }

        ///<inheritdoc cref="IDictionaryDataService.SaveDictionaryResultData(FileSaveDataInformationDto)"/>
        public void SaveDictionaryResultData(FileSaveDataInformationDto FileSaveDto)
        {
            _logger.LogInformation("Saving Dictionary Processing Result...");

            var validationResult = _validatorSave.Validate(FileSaveDto);
            if (!validationResult.IsValid)
                throw new ArgumentInvalidException("Save File Validation", validationResult.Errors.Select(a => new ArgumentInvalidException(a.ErrorMessage)));

            _fileOperation.Create(FileSaveDto.File.FullName, FileSaveDto.DataInformation);
        }
    }
}