using System.IO;
using NonSolutionFiles;
using NonSolutionFilesTest.Stubs;
using NUnit.Framework;
using SharpTestsEx;

namespace NonSolutionFilesTest.UnitTests
{
	public class ProjectsInSolutionTest
	{
		[Test]
		public void ShouldFindProjectPath()
		{
			var solutionPath = "c:\\" + RandomString.Make() + "\\";
			var solutionFilePath = solutionPath + "something.sln";
			var fileContent = new FileReaderStub(new[] { @"
Project(""{ FAE04EC0 - 301F - 11D3 - BF4B - 00C04F79EFBC}"") = ""SlnUnusedFiles"", ""path\proj.csproj"", ""{ 44DFDA4C - 83A2 - 4DCC - 8EE3 - D1FB75700891}""
" });
			var target = new ProjectsInSolution(fileContent);
			target.ProjectPaths(solutionFilePath)
				.Should().Have.SameValuesAs(Path.Combine(solutionPath, @"path\proj.csproj"));
		}

		[Test]
		public void ShouldFindMultipleProjectPaths()
		{
			var solutionPath = "c:\\" + RandomString.Make() + "\\";
      var solutionFilePath = solutionPath + "something.sln";
			var fileContent = new FileReaderStub(new[]
			{
				@"
Project(""{ FAE04EC0 - 301F - 11D3 - BF4B - 00C04F79EFBC}"") = ""SlnUnusedFiles"", ""SlnUnusedFiles\SlnUnusedFiles.csproj"", ""{ 44DFDA4C - 83A2 - 4DCC - 8EE3 - D1FB75700891}""",
				@"
Project(""{ FAE04EC0 - 301F - 11D3 - BF4B - 00C04F79EFB2}"") = ""SlnUnusedFiles"", ""SlnUnusedFiles2.csproj"", ""{ 44DFDA4C - 83A2 - 4DCC - 8EE3 - D1FB75700892}""
"
			});
			var target = new ProjectsInSolution(fileContent);
			target.ProjectPaths(solutionFilePath)
				.Should().Have.SameValuesAs(Path.Combine(solutionPath, @"SlnUnusedFiles\SlnUnusedFiles.csproj"), Path.Combine(solutionPath, @"SlnUnusedFiles2.csproj"));
		}

		[Test]
		public void ShouldNotFindProjectPathIfProjectIsMissing()
		{
			var path = RandomString.Make();
			var fileContent = new FileReaderStub(new[] { @"
""SlnUnusedFiles\SlnUnusedFiles.csproj"", ""{ 44DFDA4C - 83A2 - 4DCC - 8EE3 - D1FB75700891}""
" });
			var target = new ProjectsInSolution(fileContent);
			target.ProjectPaths(path)
				.Should().Be.Empty();
		}

		[Test]
		public void ShouldIgnoreNonCsProjFiles()
		{
			var path = RandomString.Make();
			var fileContent = new FileReaderStub(new[] { @"
Project(""{ FAE04EC0 - 301F - 11D3 - BF4B - 00C04F79EFBC}"") = ""SlnUnusedFiles"", ""path\proj"", ""{ 44DFDA4C - 83A2 - 4DCC - 8EE3 - D1FB75700891}""
" });
			var target = new ProjectsInSolution(fileContent);
			target.ProjectPaths(path)
				.Should().Be.Empty();
		}

		[Test]
		public void ShouldIgnoreProjectRowsWithoutComma()
		{
			var path = RandomString.Make();
			var fileContent = new FileReaderStub(new[]
			{
				"Project",
 @"
Project(""{ FAE04EC0 - 301F - 11D3 - BF4B - 00C04F79EFBC}"") = ""SlnUnusedFiles+path\proj.csproj+{ 44DFDA4C - 83A2 - 4DCC - 8EE3 - D1FB75700891}""
"
			});
			var target = new ProjectsInSolution(fileContent);
			target.ProjectPaths(path)
				.Should().Be.Empty();
		}
	}
}