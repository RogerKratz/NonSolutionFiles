using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NonSolutionFiles
{
	public class FilesOnDisk : IFilesOnDisk
	{
		public IEnumerable<string> ProjectFilesInSamePathAsProjectFileRecursive(string projectFilePath)
		{
			var folder = Path.GetDirectoryName(projectFilePath);
			var allCSharpFiles = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories);
			return allCSharpFiles.Where(file => !file.Contains(@"\obj\") && !file.Contains(@"\bin\"));
		}
	}
}