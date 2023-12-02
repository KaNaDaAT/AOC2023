using Lib;
using System.Linq;

namespace AdventOfCoding.Days {
	public class Day02B : DayAbstract {

		protected override void Runner(Reader reader)
		{
			this.Result = reader.ReadAndGetLines()
				.Select(_ => _.Split(':'))
				.Select(_ => _[1].Split(';')
					.Select(_ => _.Split(',')
						.Select(_ => _.Trim().Split(' '))
						.ToNullableDictionary(_ => _[1], _ => (int?)int.Parse(_[0]))
					)
				)
				.Select(_ =>
						_.Max(_ => _["red"] ?? 0) *
						_.Max(_ => _["green"] ?? 0) *
						_.Max(_ => _["blue"] ?? 0)
				)
				.Sum();
		}

	}
}