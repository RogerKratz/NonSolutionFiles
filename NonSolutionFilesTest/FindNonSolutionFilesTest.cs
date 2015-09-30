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
			var result = target.Find(solutionPath);

			result.Should().Have.SameValuesAs(nonSolutionFile);
		}
	}
}