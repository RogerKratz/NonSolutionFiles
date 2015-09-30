using System.Collections.Generic;
using NonSolutionFiles;

namespace NonSolutionFilesTest.Stubs
{
	public class ProjectsInSolutionStub : IProjectsInSolution
	{
		private readonly IEnumerable<string> _projectPaths;

		public ProjectsInSolutionStub(IEnumerable<string> projectPaths)
		{
			_projectPaths = projectPaths;
		}

		public IEnumerable<string> ProjectPaths(string solutionPath)
		{
			return _projectPaths;
		}
	}
}