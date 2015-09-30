using System.Collections.Generic;

namespace NonSolutionFiles
{
	public interface IFilesOnDisk
	{
		IEnumerable<string> CSharpFilesInSamePathAsProjectFileRecursive(string path);
	}
}