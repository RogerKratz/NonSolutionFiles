using System.Collections.Generic;
using NonSolutionFiles;

namespace NonSolutionFilesTest.Stubs
{
	public class FilesInProjectStub : IFilesInProject
	{
		private readonly IEnumerable<string> _filesInProjectFile;

		public FilesInProjectStub(IEnumerable<string> filesInProjectFile)
		{
			_filesInProjectFile = filesInProjectFile;
		}

		public IEnumerable<string> FilePaths(string projectPath)
		{
			return _filesInProjectFile;
		}
	}
}