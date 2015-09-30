using System;
using NonSolutionFiles;

namespace FindDeadFiles
{
	class Program
	{
		static void Main(string[] args)
		{
			var solutionPath = args[0];
			var fileReader = new FileReader();
			var findNonSolutionFiles = new FindNonSolutionFiles(new FilesOnDisk(), new FilesInProject(fileReader), new ProjectsInSolution(fileReader));
			foreach (var deadFile in findNonSolutionFiles.Find(solutionPath))
			{
				Console.WriteLine(deadFile);
			}
			Console.ReadKey();
		}
	}
}
