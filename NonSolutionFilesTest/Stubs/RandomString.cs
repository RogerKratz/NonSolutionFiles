using System;

namespace NonSolutionFilesTest.Stubs
{
	public static class RandomString
	{
		private static readonly Random random = new Random();
		const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

		public static string Make()
		{
			var strArr = chars.ToCharArray();
			var arrLength = strArr.Length;
			var ret = string.Empty;
			for (var i = 0; i < 10; i++)
			{
				ret += strArr[random.Next(arrLength)];
			}
			return ret;
		}
	}
}