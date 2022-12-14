
# BluePrismTechnicalTest - Hugo Sousa

## Requirements
 Write a console application in VB.NET/C#.NET that takes the following command-line arguments:
 

 - DictionaryFile - the file name of a text file containing four letter
   words (included in the test pack);
 - StartWord - a four letter word (that you can assume is found in the
   DictionaryFile file);
 - EndWord - a four letter word (that you can assume is found in the
   DictionaryFile file);
 - ResultFile - the file name of a text file that will contain the
   result.
  
Your program should calculate the shortest list of four letter  words, starting with StartWord, and ending with EndWord, with a number of intermediate words that must appear in the DictionaryFile file where each word differs from the previous word by one letter only.

The result should be written to the destination specified by the ResultFile argument. For example, if StartWord = Spin, EndWord = Spot and DictionaryFile file contains:

- Spin
- Spit
- Spat
- Spot
- Span

Then ResultFile should contain
- Spin
- Spit
- Spot

## Solution
To develop this solution I tried to divide my thinking into two distinct parts. One of them was the attempt to implement the Word Ladder algorithm and the other was the structuring of the project/solution.

### Word Ladder Algorithm
For the development of the algorithm I also thought about two things, the first one was trying to understand how it works and what its logic is and how I could transpose this same logic to the code. The second was related to the fact of trying to isolate that same algorithm in a service that could be reused if possible.

For the implementation of this algorithm, the idea was to grab the initial word and from there, try to change each of the characters that compose it and check if with this change the formed word is in the dictionary. If the word has already been checked, we remove it from the same dictionary in order to try the fastest path between the two words.

### Project / Solution Implementation
For the structuring of the project/solution, my thought was to try to separate everything that was possible into small projects or small pieces of code. This thought comes in the sense that in the future we can reuse small projects or small pieces of code.

So I decided to create six different projects.
- BluePrism.TechnicalTest
- BluePrism.TechnicalTest.Common
- BluePrism.TechnicalTest.Contracts
- BluePrism.TechnicalTest.Files
- BluePrism.TechnicalTest.Services
- BluePrism.TechnicalTest.Tests

#### BluePrism.TechnicalTest
This project is the entry point of the solution, that is, this is a Console Application project and it works as a presentation layer for the User. If the User executes the process with the input parameters through the console, they will be used in the process. Otherwise, if the User runs the application without any parameter, it will ask the User which input parameters he wants to use.

In addition, I also tried to take advantage of the latest .NET features, applying Dependency Injection concepts through the creation of HostBuilder when the project is initialized.

As for the operation itself, the process begins with the User entering the input parameters. Then the application validates if the input parameters are valid. If they are not valid, the process ends with the information of the errors found. If the input parameters are valid, the word list will be processed. At the end of this processing, the list of words obtained will be saved in a new file in the same directory as the Input file, but with the name defined by the user.

#### BluePrism.TechnicalTest.Common
The purpose of creating this project is to have a project with operations/constant value that can be shared with different projects and by any other project that may be interested in these features.

#### BluePrism.TechnicalTest.Contracts
This project stores all the contracts that are needed for communication between the presentation layer and the services layer. In this case, we have the interfaces and objects that are important for the services to work. This project is important because if in the future the presentation layer changes, we can easily continue to use the services layer. We will keep the same Contracts. The only thing that should change is the presentation layer.

#### BluePrism.TechnicalTest.Files
The Files project contains Read and Write file operations. This project is isolated and can be reused in any other project. When I was creating this project I was thinking in the idea of Repositories. This is the "Repository" layer who will be responsible to communicate with files. Being in a different project is easy to reuse in another applications.

#### BluePrism.TechnicalTest.Services
In the Services project, two different services were developed, one for file handling and the other for processing itself. The File Service allows access to obtain information from a specific file and allows the writing of information in a specific file defined by the user. In addition, we have the most important service that is related to the processing of the algorithm. In this case we have two different methods, one to validate if all the information provided is valid, where if the information is not valid the process ends with the error information.

Otherwise, the process is started, using the start word and the end word, the system tries to find the shortest path between the two words and returns that same path. That is, which words were used to get from one point to another.

##### DictionaryDataService
- Get data from an specific text file;
- Save data from a List to a specific file;
- Using FluentValidation to validate the different inputs;
- Logging the melhtods on the log file.

##### DictionaryProcessingService
- Validate if the Input is valid for processing through FluentValidation;
- Implementation of the Word Ladder Algorithm;
- Logging the melhtods on the log file.

#### BluePrism.TechnicalTest.Tests
Finally, and not least. Quite the contrary, we have the Tests project. This was the first project to be created and all the rest of the solution was adapted to the tests that were initially developed. This part is important because we can understand in a faster, more accurate and simple way if all the requirements that the application must have are being fulfilled.

In this project we have three different sets of tests, a set of tests against services only, a set against file operations and finally against the presentation layer.

## Future Improvements
For future improvements there are several points that can be implemented:
- Add new features to the file project such as:
	- Update the text file;
	- Remove specific text from a text file;
	- Delete a text file;
	- Currently, file-related features can only read and write strings. This project could be modified to receive some generic type (eg complex objects) and save them in a file.
- Create an intermediate layer that would make a global and centralized management of possible application exceptions (like an exception middleware);
- Implement some object mapping mechanism, it could be customized or some Nuget such as Mapster or AutoMapper;
- Create Nugets for projects that can be reused, for example the Common project could be a Nuget. I think this will make us write less repeated code, versioning parts of the code, making updates to small pieces of code more easily, among others.
- To try to reduce the dependency between objects we could also implement the drawing pattern MediatR;
- Transform the console project into an API services project;
- Minimize the number of possible exceptions. That is, where exceptions are being thrown, replace this throw with validations that allow it to be avoided.

## Usage
There are two ways we can run the process:

- Without Parameters:
	- Open Command Prompt;
	- Write command [cd SolutionFolder\BluePrism.TechnicalTest\bin\publish];
	- Write BluePrism.TechnicalTest and press Enter;
	- The program is going to start and is going to ask for all the information it needs to run like Dictionary File, Start Word, End Word and Result File Name.

- With Parameters:
	- Open Command Prompt;
	- Write command [cd SolutionFolder\BluePrism.TechnicalTest\bin\publish];
	- Write BluePrism.TechnicalTest "DictionaryFilePath" "StartWord" "EndWord" "ResultFileName" and press Enter;
	- The program is going to start and process the dictionary.

## Conclusion
In conclusion, my main thought was to give as much importance to the structuring of the project as to the algorithm itself. In this way I tried to create a project that was flexible, that was easy to understand and maintain and, above all, that was scalable.

Finally, I put the project on GitHub so that if the project was to continue, it would be easy to work as a team and collaborate with other programmers. This way it is much easier to organize the development within the team.

GitHub:
[sousaHugo/BP.CodeChallenge: BP.CodeChallenge (github.com)](https://github.com/sousaHugo/BP.CodeChallenge)
 https://github.com/sousaHugo/BP.CodeChallenge.git
 