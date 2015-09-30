using System.Collections.Generic;

namespace NonSolutionFiles
{
	public interface IFilesInProject
	{
		IEnumerable<string> FilePaths(string projectPath);
	}
}