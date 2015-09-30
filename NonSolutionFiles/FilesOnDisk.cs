using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NonSolutionFiles
{
	public class FilesOnDisk : IFilesOnDisk
	{
		public IEnumerable<string> CSharpFilesInSamePathAsProjectFileRecursive(string projectFilePath)
		{
			var folder = Path.GetDirectoryName(projectFilePath);
			var allCSharpFiles = Directory.GetFiles(folder, "*.cs", SearchOption.AllDirectories);
			return allCSharpFiles.Where(file => !file.Contains(@"\obj\") && !file.Contains(@"\bin\"));
		}
	}
}