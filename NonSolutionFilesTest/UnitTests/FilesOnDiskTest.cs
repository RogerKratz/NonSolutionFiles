using System;
using System.IO;
using NonSolutionFiles;
using NonSolutionFilesTest.Stubs;
using NUnit.Framework;
using SharpTestsEx;

namespace NonSolutionFilesTest.UnitTests
{
	public class FilesOnDiskTest
	{
		private static string rootFolder;
		private static string projectFile;

		[SetUp]
		public void CreateWorkingDirectory()
		{
			rootFolder = Path.Combine(Path.GetTempPath(), "NonSolutionFiles");
			projectFile = Path.Combine(rootFolder, RandomString.Make() + "theone.csproj");
			Directory.CreateDirectory(rootFolder);
			File.WriteAllText(projectFile, RandomString.Make());
		}

		[TearDown]
		public void DropWorkingDirectory()
		{
			Directory.Delete(rootFolder, true);
		}

		[Test]
		public void ShouldFindFile()
		{
			var tempFilePath = Path.Combine(rootFolder, RandomString.Make() + ".cs");
			File.WriteAllText(tempFilePath, RandomString.Make());
				
			var target = new FilesOnDisk();
			target.ProjectFilesInSamePathAsProjectFileRecursive(projectFile)
				.Should().Contain(tempFilePath);
		}

		[Test]
		public void ShouldFindFileInSubfolder()
		{
			var folder = Path.Combine(rootFolder, RandomString.Make());
			var tempFilePath = Path.Combine(folder, RandomString.Make() + ".cs");
			Directory.CreateDirectory(folder);
			File.WriteAllText(tempFilePath, RandomString.Make());

			var target = new FilesOnDisk();
			target.ProjectFilesInSamePathAsProjectFileRecursive(projectFile)
				.Should().Contain(tempFilePath);
		}

		[Test]
		public void ShouldFindNonDotCsFile()
		{
			var tempFilePath = Path.Combine(rootFolder, RandomString.Make() + ".somethingelse");
			File.WriteAllText(tempFilePath, RandomString.Make());

			var target = new FilesOnDisk();
			target.ProjectFilesInSamePathAsProjectFileRecursive(projectFile)
				.Should().Contain(tempFilePath);
		}

		[Test]
		public void ShouldNotFindFileInObjFolder()
		{
			var folder = Path.Combine(rootFolder, "obj");
			var tempFilePath = Path.Combine(folder, RandomString.Make() + ".cs");
			Directory.CreateDirectory(folder);
			File.WriteAllText(tempFilePath, RandomString.Make());

			var target = new FilesOnDisk();
			target.ProjectFilesInSamePathAsProjectFileRecursive(projectFile)
				.Should().Not.Contain(tempFilePath);
		}

		[Test]
		public void ShouldNotFindFileInObjSubFolder()
		{
			var folder = Path.Combine(rootFolder, "obj", RandomString.Make());
			var tempFilePath = Path.Combine(folder, RandomString.Make() + ".cs");
			Directory.CreateDirectory(folder);
			File.WriteAllText(tempFilePath, RandomString.Make());

			var target = new FilesOnDisk();
			target.ProjectFilesInSamePathAsProjectFileRecursive(projectFile)
				.Should().Not.Contain(tempFilePath);
		}

		[Test]
		public void ShouldNotFindFileInBinFolder()
		{
			var folder = Path.Combine(rootFolder, "bin");
			var tempFilePath = Path.Combine(folder, RandomString.Make() + ".cs");
			Directory.CreateDirectory(folder);
			File.WriteAllText(tempFilePath, RandomString.Make());

			var target = new FilesOnDisk();
			target.ProjectFilesInSamePathAsProjectFileRecursive(projectFile)
				.Should().Not.Contain(tempFilePath);
		}

		[Test]
		public void ShouldNotFindFileInBinSubFolder()
		{
			var folder = Path.Combine(rootFolder, "bin", RandomString.Make());
			var tempFilePath = Path.Combine(folder, RandomString.Make() + ".cs");
			Directory.CreateDirectory(folder);
			File.WriteAllText(tempFilePath, RandomString.Make());

			var target = new FilesOnDisk();
			target.ProjectFilesInSamePathAsProjectFileRecursive(projectFile)
				.Should().Not.Contain(tempFilePath);
		}

		[Test]
		public void ShouldNotFindFileInBinCaseingSubFolder()
		{
			var folder = Path.Combine(rootFolder, "BiN", RandomString.Make());
			var tempFilePath = Path.Combine(folder, RandomString.Make() + ".cs");
			Directory.CreateDirectory(folder);
			File.WriteAllText(tempFilePath, RandomString.Make());

			var target = new FilesOnDisk();
			target.ProjectFilesInSamePathAsProjectFileRecursive(projectFile)
				.Should().Not.Contain(tempFilePath);
		}

		[Test]
		public void ShouldNotFindUserFiles()
		{
			var tempFilePath = Path.Combine(rootFolder, RandomString.Make() + ".user");
			File.WriteAllText(tempFilePath, RandomString.Make());

			var target = new FilesOnDisk();
			target.ProjectFilesInSamePathAsProjectFileRecursive(projectFile)
				.Should().Not.Contain(tempFilePath);
		}

		[Test]
		public void ShouldNotFindCsProjFiles()
		{
			var tempFilePath = Path.Combine(rootFolder, RandomString.Make() + ".csproj");
			File.WriteAllText(tempFilePath, RandomString.Make());

			var target = new FilesOnDisk();
			target.ProjectFilesInSamePathAsProjectFileRecursive(projectFile)
				.Should().Not.Contain(tempFilePath);
		}

		[Test]
		public void ShouldNotFindSolutionFiles()
		{
			var tempFilePath = Path.Combine(rootFolder, RandomString.Make() + ".sln");
			File.WriteAllText(tempFilePath, RandomString.Make());

			var target = new FilesOnDisk();
			target.ProjectFilesInSamePathAsProjectFileRecursive(projectFile)
				.Should().Not.Contain(tempFilePath);
		}

		[Test]
		public void ShouldOnlyAcceptFilePaths()
		{
			var target = new FilesOnDisk();
			Assert.Throws<ArgumentException>(() =>
			{
				target.ProjectFilesInSamePathAsProjectFileRecursive(rootFolder);
			});
		}
	}
}