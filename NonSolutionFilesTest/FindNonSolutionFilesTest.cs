using System.Linq;
using NonSolutionFiles;
using NonSolutionFilesTest.Stubs;
using NUnit.Framework;
using SharpTestsEx;

namespace NonSolutionFilesTest
{
	public class FindNonSolutionFilesTest
	{
		[Test]
		public void ShouldFindNonSolutionFile()
		{
			var solutionPath = RandomString.Make();
			var projectPath = RandomString.Make();
			var filesInProjectFile = new[] {RandomString.Make(), RandomString.Make()};
			var nonSolutionFile = RandomString.Make();

			var projectsInSolution = new ProjectsInSolutionStub(new [] {projectPath});
			var filesInProject = new FilesInProjectStub(filesInProjectFile);
			var filesOnDisk = new FilesOnDiskStub(filesInProjectFile.Union(new[] { nonSolutionFile }));

			var target = new FindNonSolutionFiles(filesOnDisk, filesInProject, projectsInSolution);
			var result = target.Find(solutionPath, Enumerable.Empty<string>());

			result.Should().Have.SameValuesAs(nonSolutionFile);
		}

		[Test]
		public void ShouldFindNonSolutionFileIfNoExcludeMatch()
		{
			var solutionPath = RandomString.Make();
			var projectPath = RandomString.Make();
			var filesInProjectFile = new[] { RandomString.Make(), RandomString.Make() };
			var nonSolutionFile = RandomString.Make();

			var projectsInSolution = new ProjectsInSolutionStub(new[] { projectPath });
			var filesInProject = new FilesInProjectStub(filesInProjectFile);
			var filesOnDisk = new FilesOnDiskStub(filesInProjectFile.Union(new[] { nonSolutionFile }));

			var target = new FindNonSolutionFiles(filesOnDisk, filesInProject, projectsInSolution);
			var result = target.Find(solutionPath, new[] {RandomString.Make(), RandomString.Make()});

			result.Should().Have.SameValuesAs(nonSolutionFile);
		}

		[Test]
		public void ShouldSkipNonSolutionFileIfExcludeMatch()
		{
			var solutionPath = RandomString.Make();
			var projectPath = RandomString.Make();
			var filesInProjectFile = new[] { RandomString.Make(), RandomString.Make() };
			var nonSolutionFile = @"c:\something\me.txt";

			var projectsInSolution = new ProjectsInSolutionStub(new[] { projectPath });
			var filesInProject = new FilesInProjectStub(filesInProjectFile);
			var filesOnDisk = new FilesOnDiskStub(filesInProjectFile.Union(new[] { nonSolutionFile }));

			var target = new FindNonSolutionFiles(filesOnDisk, filesInProject, projectsInSolution);
			var result = target.Find(solutionPath, new[] {"something"});

			result.Should().Be.Empty();
		}
	}
}