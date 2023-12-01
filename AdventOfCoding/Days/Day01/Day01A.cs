using Lib;
using System.Linq;

namespace AdventOfCoding.Days {
	public class Day01A : DayAbstract {

		protected override void Runner(Reader reader)
		{
			this.Result = reader.ReadAndGetLines()
				.Select(_ => $"{_.First(char.IsDigit)}{_.Last(char.IsDigit)}")
				.Select(int.Parse)
				.Sum();
		}

	}
}