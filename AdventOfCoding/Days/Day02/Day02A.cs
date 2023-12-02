using Lib;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCoding.Days {
	public class Day02A : DayAbstract {

		private int _redCubes = 12;
		private int _greenCubes = 13;
		private int _blueCubes = 14;

		protected override void Runner(Reader reader)
		{
			this.Result = reader.ReadAndGetLines()
				.Select(_ => _.Split(':'))
				.Select(_ => new Game(
						int.Parse(_[0][4..]),
						_[1].Split(';').Select(_ =>
							_.Split(',')
								.Select(_ => _.Trim().Split(' '))
								.ToNullableDictionary(_ => _[1], _ => (int?)int.Parse(_[0]))
						).ToList()
				))
				.Sum(g => g.IsPossible(_redCubes, _greenCubes, _blueCubes) ? g.Id : 0);
		}
		record Game(int Id, List<NullableDictionary<string, int?>> GameSubSets) {
			public bool IsPossible(int red, int green, int blue)
				=> GameSubSets.All(gss => red >= (gss["red"] ?? 0) && green >= (gss["green"] ?? 0) && blue >= (gss["blue"] ?? 0));
		}

	}
}