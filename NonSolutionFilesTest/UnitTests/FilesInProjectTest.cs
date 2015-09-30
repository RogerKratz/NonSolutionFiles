using System;
using System.IO;
using NonSolutionFiles;
using NonSolutionFilesTest.Stubs;
using NUnit.Framework;
using SharpTestsEx;

namespace NonSolutionFilesTest.UnitTests
{
	public class FilesInProjectTest
	{
		[Test]
		public void ShouldFindFilePath()
		{
			const string doc = @"
<Project xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
  <ItemGroup>	
		<Compile Include=""THIS/that.cs"" />
	</ItemGroup>
</Project>
";
			var folder = "c:\\" + RandomString.Make();
      var path =  folder + "\\someproj.csproj";
			var fileContent = new FileReaderStub(doc.Split(Environment.NewLine.ToCharArray()));
			var target = new FilesInProject(fileContent);
			target.FilePaths(path)
				.Should().Have.SameValuesAs(Path.Combine(folder, @"THIS/that.cs"));
		}

		[Test]
		public void ShouldFindFilePathOnAnyElement()
		{
			const string doc = @"
<Project xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
  <ItemGroup>	
		<SomethingStrange Include=""THIS.txt"" />
	</ItemGroup>
</Project>
";
			var folder = "c:\\" + RandomString.Make();
			var path = folder + "\\someproj.csproj";
			var fileContent = new FileReaderStub(doc.Split(Environment.NewLine.ToCharArray()));
			var target = new FilesInProject(fileContent);
			target.FilePaths(path)
				.Should().Have.SameValuesAs(Path.Combine(folder, @"THIS.txt"));
		}

		[Test]
		public void ShouldNotFindExcludedFiles()
		{
			const string doc = @"
<Project xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
  <ItemGroup>	
		<Compile Exclude=""THIS.cs"" />
	</ItemGroup>
</Project>
";
			var folder = "c:\\" + RandomString.Make();
			var path = folder + "\\someproj.csproj";
			var fileContent = new FileReaderStub(doc.Split(Environment.NewLine.ToCharArray()));
			var target = new FilesInProject(fileContent);
			target.FilePaths(path)
				.Should().Be.Empty();
		}

		[Test]
		public void ShouldNotFindBothIncludedAndExcludedFiles()
		{
			const string doc = @"
<Project xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
  <ItemGroup>	
		<Compile Include=""THIS.cs"" />
		<Compile Exclude=""THIS.cs"" />
	</ItemGroup>
</Project>
";
			var folder = "c:\\" + RandomString.Make();
			var path = folder + "\\someproj.csproj";
			var fileContent = new FileReaderStub(doc.Split(Environment.NewLine.ToCharArray()));
			var target = new FilesInProject(fileContent);
			target.FilePaths(path)
				.Should().Be.Empty();
		}

		[Test]
		public void ShouldFindMultipleProjectPaths()
		{
			const string doc = @"
<Project xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
  <ItemGroup>	
		<Compile Include=""THIS.cs"" />
		<Compile Include=""that/THIS2.cs"" />
	</ItemGroup>
</Project>
";
			var folder = "c:\\" + RandomString.Make();
			var path = folder + "\\someproj.csproj";
			var fileContent = new FileReaderStub(doc.Split(Environment.NewLine.ToCharArray()));
			var target = new FilesInProject(fileContent);
			target.FilePaths(path)
				.Should().Have.SameValuesAs(Path.Combine(folder, "THIS.cs"), Path.Combine(folder, "that/THIS2.cs"));
		}

		[Test]
		public void ShouldFindMixOfMultipleProjectPaths()
		{
			const string doc = @"
<Project xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
  <ItemGroup>	
		<Compile Include=""THIS.cs"" />
	</ItemGroup>
 <ItemGroup>	
		<Content Include=""that/THIS2.cs"" />
	</ItemGroup>
</Project>
";
			var folder = "c:\\" + RandomString.Make();
			var path = folder + "\\someproj.csproj";
			var fileContent = new FileReaderStub(doc.Split(Environment.NewLine.ToCharArray()));
			var target = new FilesInProject(fileContent);
			target.FilePaths(path)
				.Should().Have.SameValuesAs(Path.Combine(folder, "THIS.cs"), Path.Combine(folder, "that/THIS2.cs"));
		}
	}
}