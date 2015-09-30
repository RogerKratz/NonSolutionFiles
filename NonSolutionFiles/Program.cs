using System;

namespace NonSolutionFiles
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length != 1)
			{
				Console.WriteLine("Usage:");
				Console.WriteLine("NonSolutionFiles.exe [absolute path to your c# solution file]");
			}
			else
			{
				var solutionPath = args[0];
				var fileReader = new FileReader();
				var findNonSolutionFiles = new FindNonSolutionFiles(new FilesOnDisk(), new FilesInProject(fileReader), new ProjectsInSolution(fileReader));
				foreach (var deadFile in findNonSolutionFiles.Find(solutionPath))
				{
					Console.WriteLine(deadFile);
				}
			}
		}
	}
}