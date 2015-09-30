using System.Collections.Generic;

namespace NonSolutionFiles
{
	public interface IProjectsInSolution
	{
		IEnumerable<string> ProjectPaths(string solutionPath);
	}
}