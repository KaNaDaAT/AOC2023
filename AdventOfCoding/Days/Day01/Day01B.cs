using Lib;
using System.Linq;

namespace AdventOfCoding.Days {
	public class Day01B : DayAbstract {

		protected override void Runner(Reader reader)
		{
			(string Str, int Number)[] nts = {
				("one", 1), ("two", 2), ("three", 3),
				("four", 4), ("five", 5), ("six", 6),
				("seven", 7), ("eight", 8), ("nine", 9)
			};
			this.Result = reader.ReadAndGetLines()
				.Select(line => nts.Aggregate(line, (current, nt) => current.Replace(nt.Str, $"{nt.Str[0]}{nt.Number}{nt.Str[^1]}")))
				.Select(line => int.Parse($"{line.First(char.IsDigit)}{line.Last(char.IsDigit)}"))
				.Sum();
		}

	}
}