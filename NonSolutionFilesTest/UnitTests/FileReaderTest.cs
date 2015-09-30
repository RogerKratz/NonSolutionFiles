using System;
using System.IO;
using NonSolutionFiles;
using NonSolutionFilesTest.Stubs;
using NUnit.Framework;
using SharpTestsEx;

namespace NonSolutionFilesTest.UnitTests
{
	public class FileReaderTest
	{
		[Test]
		public void ShouldReadContent()
		{
			var tempFilePath = Path.GetTempFileName();
			var row1 = RandomString.Make();
			var row2 = RandomString.Make();
			try
			{
				File.WriteAllText(tempFilePath, row1 + Environment.NewLine + row2);

				var target = new FileReader();
				target.ReadRows(tempFilePath)
					.Should().Have.SameSequenceAs(row1, row2);
			}
			finally
			{
				File.Delete(tempFilePath);
			}
		}
	}
}