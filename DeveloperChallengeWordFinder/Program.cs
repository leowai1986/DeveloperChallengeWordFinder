using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace DeveloperChallengeWordFinder
{
	public class Program
	{
		public static void Main()
		{
			var dictionary = new string[] { "chill", "wind", "snow", "cold" };
			var src = new string[] { "abcdc", "fgwio", "chill", "pqnsd", "uvdxy" };
			var result = new WordFinder(dictionary).Find(src);
			Console.WriteLine(string.Join(",", result));
		}

		public class WordFinder
		{
			private readonly HashSet<string> dictionary;

			public WordFinder(IEnumerable<string> dictionary)
			{
				this.dictionary = new HashSet<string>(dictionary);
			}

			public IList<string> Find(IEnumerable<string> src)
			{
				var leftRightSearchString = string.Join(string.Empty, src);

				var characterMatrix = src
					.Select(row => row.ToCharArray())
					.ToArray();
				var topDownSearchStringBuilder = new StringBuilder();
				for (var i = 0; i < characterMatrix.Length; i++)
				{
					for (var j = 0; j < characterMatrix[i].Length; j++)
					{
						topDownSearchStringBuilder.Append(characterMatrix[j][i]);
					}
				}
				var topDownSearchString = topDownSearchStringBuilder.ToString();

				var resultSet = new HashSet<string>();
				resultSet.UnionWith(dictionary.Where(searchTerm =>
													 leftRightSearchString.Contains(searchTerm)));
				resultSet.UnionWith(dictionary.Where(searchTerm =>
													 topDownSearchString.Contains(searchTerm)));

				return resultSet.ToList();
			}
		}
	}
}