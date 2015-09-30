using System.Collections.Generic;
using NonSolutionFiles;

namespace NonSolutionFilesTest.Stubs
{
	public class FilesOnDiskStub : IFilesOnDisk
	{
		private readonly IEnumerable<string> _filesOnDisk;

		public FilesOnDiskStub(IEnumerable<string> filesOnDisk)
		{
			_filesOnDisk = filesOnDisk;
		}

		public IEnumerable<string> ProjectFilesInSamePathAsProjectFileRecursive(string path)
		{
			return _filesOnDisk;
		}
	}
}