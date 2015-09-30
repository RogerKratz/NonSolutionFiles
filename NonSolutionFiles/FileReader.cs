using System.Collections.Generic;
using System.IO;

namespace NonSolutionFiles
{
	public class FileReader : IFileReader
	{
		public IEnumerable<string> ReadRows(string path)
		{
			return File.ReadLines(path);
		}
	}
}