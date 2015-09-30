using System.Collections.Generic;

namespace NonSolutionFiles
{
	public interface IFilesOnDisk
	{
		IEnumerable<string> ProjectFilesInSamePathAsProjectFileRecursive(string path);
	}
}