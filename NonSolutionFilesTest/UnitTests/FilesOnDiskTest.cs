using System.IO;
using NonSolutionFiles;
using NonSolutionFilesTest.Stubs;
using NUnit.Framework;
using SharpTestsEx;

namespace NonSolutionFilesTest.UnitTests
{
	public class FilesOnDiskTest
	{
		[Test]
		public void ShouldFindFile()
		{
			var tempFilePath = Path.Combine(Path.GetTempPath(), RandomString.Make() + ".cs");
			var text = RandomString.Make();
			try
			{
				File.WriteAllText(tempFilePath, text);
				
				var target = new FilesOnDisk();
				target.ProjectFilesInSamePathAsProjectFileRecursive(tempFilePath)
					.Should().Contain(tempFilePath);
			}
			finally
			{
				File.Delete(tempFilePath);
			}
		}

		[Test]
		public void ShouldFindFileInSubfolder()
		{
			var folder = Path.Combine(Path.GetTempPath(), RandomString.Make());
			var tempFilePath = Path.Combine(folder, RandomString.Make() + ".cs");
			var text = RandomString.Make();
			try
			{
				Directory.CreateDirectory(folder);
				File.WriteAllText(tempFilePath, text);

				var target = new FilesOnDisk();
				target.ProjectFilesInSamePathAsProjectFileRecursive(tempFilePath)
					.Should().Contain(tempFilePath);
			}
			finally
			{
				File.Delete(tempFilePath);
				Directory.Delete(folder);
			}
		}

		[Test]
		public void ShouldFindNonDotCsFile()
		{
			var tempFilePath = Path.Combine(Path.GetTempPath(), RandomString.Make() + ".somethingelse");
			var text = RandomString.Make();
			try
			{
				File.WriteAllText(tempFilePath, text);

				var target = new FilesOnDisk();
				target.ProjectFilesInSamePathAsProjectFileRecursive(tempFilePath)
					.Should().Contain(tempFilePath);
			}
			finally
			{
				File.Delete(tempFilePath);
			}
		}

		[Test]
		public void ShouldNotFindFileInObjFolder()
		{
			var folder = Path.Combine(Path.GetTempPath(), "obj");
			var tempFilePath = Path.Combine(folder, RandomString.Make() + ".cs");
			var text = RandomString.Make();
			try
			{
				Directory.CreateDirectory(folder);
				File.WriteAllText(tempFilePath, text);

				var target = new FilesOnDisk();
				target.ProjectFilesInSamePathAsProjectFileRecursive(tempFilePath)
					.Should().Not.Contain(tempFilePath);
			}
			finally
			{
				File.Delete(tempFilePath);
				Directory.Delete(folder);
			}
		}

		[Test]
		public void ShouldNotFindFileInObjSubFolder()
		{
			var folder = Path.Combine(Path.GetTempPath(), "obj", RandomString.Make());
			var tempFilePath = Path.Combine(folder, RandomString.Make() + ".cs");
			var text = RandomString.Make();
			try
			{
				Directory.CreateDirectory(folder);
				File.WriteAllText(tempFilePath, text);

				var target = new FilesOnDisk();
				target.ProjectFilesInSamePathAsProjectFileRecursive(tempFilePath)
					.Should().Not.Contain(tempFilePath);
			}
			finally
			{
				File.Delete(tempFilePath);
				Directory.Delete(folder);
			}
		}

		[Test]
		public void ShouldNotFindFileInBinFolder()
		{
			var folder = Path.Combine(Path.GetTempPath(), "bin");
			var tempFilePath = Path.Combine(folder, RandomString.Make() + ".cs");
			var text = RandomString.Make();
			try
			{
				Directory.CreateDirectory(folder);
				File.WriteAllText(tempFilePath, text);

				var target = new FilesOnDisk();
				target.ProjectFilesInSamePathAsProjectFileRecursive(tempFilePath)
					.Should().Not.Contain(tempFilePath);
			}
			finally
			{
				File.Delete(tempFilePath);
				Directory.Delete(folder);
			}
		}

		[Test]
		public void ShouldNotFindFileInBinSubFolder()
		{
			var folder = Path.Combine(Path.GetTempPath(), "bin", RandomString.Make());
			var tempFilePath = Path.Combine(folder, RandomString.Make() + ".cs");
			var text = RandomString.Make();
			try
			{
				Directory.CreateDirectory(folder);
				File.WriteAllText(tempFilePath, text);

				var target = new FilesOnDisk();
				target.ProjectFilesInSamePathAsProjectFileRecursive(tempFilePath)
					.Should().Not.Contain(tempFilePath);
			}
			finally
			{
				File.Delete(tempFilePath);
				Directory.Delete(folder);
			}
		}

		[Test]
		public void ShouldNotFindCsProjFiles()
		{
			var tempFilePath = Path.Combine(Path.GetTempPath(), RandomString.Make() + ".csproj");
			var text = RandomString.Make();
			try
			{
				File.WriteAllText(tempFilePath, text);

				var target = new FilesOnDisk();
				target.ProjectFilesInSamePathAsProjectFileRecursive(tempFilePath)
					.Should().Not.Contain(tempFilePath);
			}
			finally
			{
				File.Delete(tempFilePath);
			}
		}

		[Test]
		public void ShouldNotFindSolutionFiles()
		{
			var tempFilePath = Path.Combine(Path.GetTempPath(), RandomString.Make() + ".sln");
			var text = RandomString.Make();
			try
			{
				File.WriteAllText(tempFilePath, text);

				var target = new FilesOnDisk();
				target.ProjectFilesInSamePathAsProjectFileRecursive(tempFilePath)
					.Should().Not.Contain(tempFilePath);
			}
			finally
			{
				File.Delete(tempFilePath);
			}
		}
	}
}