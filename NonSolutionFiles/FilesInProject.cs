using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace NonSolutionFiles
{
	public class FilesInProject : IFilesInProject
	{
		private readonly IFileReader _fileReader;

		public FilesInProject(IFileReader fileReader)
		{
			_fileReader = fileReader;
		}

		public IEnumerable<string> FilePaths(string projectPath)
		{
			XNamespace msbuild = "http://schemas.microsoft.com/developer/msbuild/2003";
			var projDefinition = XDocument.Parse(string.Join(Environment.NewLine, _fileReader.ReadRows(projectPath)));
			var compileElements = projDefinition
					.Element(msbuild + "Project")
					.Elements(msbuild + "ItemGroup")
					.Elements(msbuild + "Compile");
			var includePaths = compileElements.Attributes("Include").Select(a => a.Value);
			var excludePaths = compileElements.Attributes("Exclude").Select(a => a.Value);
			var deadFiles =  includePaths.Except(excludePaths);
			return deadFiles.Select(deadFile => Path.Combine(Path.GetDirectoryName(projectPath), deadFile));
		}
	}
}