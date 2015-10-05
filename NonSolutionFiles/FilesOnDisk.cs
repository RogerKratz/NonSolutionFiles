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
			return allCSharpFiles
				.Where(file => 
					!file.ToLower().Contains(@"\obj\") && 
					!file.ToLower().Contains(@"\bin\") && 
					!file.ToLower().EndsWith(".sln") &&
					!file.ToLower().EndsWith(".csproj") &&
					!file.ToLower().EndsWith(".user"));
		}
	}
}