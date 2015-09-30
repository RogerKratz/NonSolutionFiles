using System.Collections.Generic;

namespace NonSolutionFiles
{
	public interface IFileReader
	{
		IEnumerable<string> ReadRows(string path);
	}
}