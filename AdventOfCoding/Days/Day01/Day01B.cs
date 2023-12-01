using Lib;
using System.Linq;

namespace AdventOfCoding.Days {
	public class Day01B : DayAbstract {

		protected override void Runner(Reader reader)
		{
			(string Str, string Number)[] nts = {
				("one", "o1e"), ("two", "t2o"), ("three", "t3e"),
				("four", "f4r"), ("five", "f5e"), ("six", "s6x"),
				("seven", "s7n"), ("eight", "e8t"), ("nine", "n9n")
			};
			this.Result = reader.ReadAndGetLines()
				.Select(line => nts.Aggregate(line, (current, nt) => current.Replace(nt.Str, nt.Number)))
				.Select(line => int.Parse($"{line.First(char.IsDigit)}{line.Last(char.IsDigit)}"))
				.Sum();
		}

	}
}