using System;
using System.IO;
using System.Text;

namespace ElasticSearchTests
{
	public class RandomData
	{
		public static string GetRandomContent(int paras, int words)
		{
			var builder = new StringBuilder();
			for(var p = 0; p <= paras; p++)
			{
				if (paras > 0) builder.Append("<p>");
				for (var w = 0; w <= words; w++)
				{
					builder.Append(RandomWord());
					builder.Append((w < words) ? " " : ".");
				}
				if (paras > 0) builder.Append("</p>");
			}

			return builder.ToString();
		}

		public static string RandomWord()
		{
			return dictionary[random.Next(dictionary.Length)];
		}

		private static readonly Random random = new Random((int)DateTime.Now.Ticks);
		private static readonly string[] dictionary = File.ReadAllLines("words.lst");
		
		public static string RandomString(int size)
		{
			var builder = new StringBuilder();
			for (var i = 0; i < size; i++)
			{
				var ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
				builder.Append(ch);
			}

			return builder.ToString();
		}
	}
}