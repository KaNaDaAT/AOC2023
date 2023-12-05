using Lib;
using Lib.Extensions;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCoding.Days {
	public class Day05A : DayAbstract {

		protected override void Runner(Reader reader)
		{
			var mappings = Regex.Split(reader.ReadAllWithoutR().Replace("seeds: ", ""), @"\n.*:")
				.Where(_ => !string.IsNullOrEmpty(_))
				.Select(_ => _.Split('\n', StringSplitOptions.RemoveEmptyEntries)
					.Select(_ =>
						_.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList()
					).ToList()
				).ToList();
			this.Result = Enumerable.Range(0, mappings[0][0].Count).Select(i =>
				Enumerable.Range(1, mappings.Count - 1)
					.Aggregate(
						mappings[0][0][i],
						(map, j) => (mappings[j].FirstOrDefault(_ =>
							map.Between(_[1], _[1] + _[2]), null)?.Take(2).Aggregate((a, b) => a - b) ?? 0) + map
					)
				).Min();
		}
	}
}