using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NonSolutionFiles
{
	public class ProjectsInSolution : IProjectsInSolution
	{
		private readonly IFileReader _fileReader;

		public ProjectsInSolution(IFileReader fileReader)
		{
			_fileReader = fileReader;
		}

		public IEnumerable<string> ProjectPaths(string solutionPath)
		{
			var paths = (from line in _fileReader.ReadRows(solutionPath)
							where line.Trim().StartsWith("Project") && line.Contains(',')
							select line.Split(',')[1].Trim().Trim('"')).ToList();
			var csProjPaths = paths.Where(p => p.EndsWith(".csproj"));
			return csProjPaths.Select(csProjPath => Path.Combine(Path.GetDirectoryName(solutionPath), csProjPath));
		}
	}
}