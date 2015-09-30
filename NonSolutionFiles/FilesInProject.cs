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
			var itemGroups = projDefinition
				.Element(msbuild + "Project")
				.Elements(msbuild + "ItemGroup")
				.ToArray();

			var itemGroupElements = itemGroups.Elements().ToArray();
			var includedFiles = itemGroupElements.Attributes("Include").Select(a => a.Value)
				.Except(itemGroupElements.Attributes("Exclude").Select(a => a.Value));
			return includedFiles.Select(file => Path.Combine(Path.GetDirectoryName(projectPath), file));

			var compileElements = itemGroups
					.Elements(msbuild + "Compile")
					.ToArray();
			var contentElements = itemGroups
				.Elements(msbuild + "Content")
				.ToArray();

			var deadCompileFiles = compileElements.Attributes("Include").Select(a => a.Value)
				.Except(compileElements.Attributes("Exclude").Select(a => a.Value));
			var deadContentFiles = contentElements.Attributes("Include").Select(a => a.Value)
				.Except(contentElements.Attributes("Exclude").Select(a => a.Value));

			return deadCompileFiles.Union(deadContentFiles)
				.Select(deadFile => Path.Combine(Path.GetDirectoryName(projectPath), deadFile));
		}
	}
}