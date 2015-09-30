using System.Collections.Generic;
using NonSolutionFiles;

namespace NonSolutionFilesTest.Stubs
{
	public class FileReaderStub : IFileReader
	{
		private readonly IEnumerable<string> _content;

		public FileReaderStub(IEnumerable<string> content)
		{
			_content = content;
		}

		public IEnumerable<string> ReadRows(string path)
		{
			return _content;
		}
	}
}