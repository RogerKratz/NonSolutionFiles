using System;
using System.Linq;

namespace NonSolutionFiles
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				Console.WriteLine("Usage:");
				Console.WriteLine("NonSolutionFiles.exe [absolute path to your c# solution file] [excludeFileContaingString1] [excludeFileContainingString2] [...]");
			}
			else
			{
				var solutionPath = args[0];
				var exludeFilesContaining = args.Skip(1);
				var fileReader = new FileReader();
				var findNonSolutionFiles = new FindNonSolutionFiles(new FilesOnDisk(), new FilesInProject(fileReader), new ProjectsInSolution(fileReader));
				var deadFiles = findNonSolutionFiles.Find(solutionPath, exludeFilesContaining);
        foreach (var deadFile in deadFiles)
				{
					Console.WriteLine(deadFile);
				}
				Console.WriteLine();
				Console.WriteLine($"Found {deadFiles.Count()} dead files!");
			}
		}
	}
}