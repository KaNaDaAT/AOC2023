using Lib;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCoding.Days {
	public class Day03B : DayAbstract {

		protected override void Runner(Reader reader)
		{
			var grid = new Grid<char>(reader.ReadAndGetLines().Select(_ => _.ToCharArray()).ToArray());

			var gears = new NullableDictionary<(int X, int Y), List<int>>();
			grid.ForEach(_ => {
				if (!char.IsDigit(grid.Get(_.x, _.y)) || char.IsDigit(grid.Get(_.x - 1, _.y, '1')))
					return;
				List<(int x, int y, int value)> numbers = new() { (_.x, _.y, grid.Get(_.x, _.y) - '0') };
				for (int i = 1; i < grid.ColumnSize - _.x; i++)
				{
					var right = grid.Get(_.x + i, _.y, ' ');
					if (!char.IsDigit(right)) break;
					numbers.Add((_.x + i, _.y, right - '0'));
				}
				var number = numbers.Select(_ => _.value).Aggregate((acc, num) => acc * 10 + num);
				numbers.SelectMany(_ => grid.GetAdjcentWithIndex(_.x, _.y, false))
					.Distinct()
					.Where(_ => _.Value == '*')
					.ToList()
					.ForEach(_ => (gears[(_.X, _.Y)] ??= new List<int>()).Add(number));
			});
			this.Result = gears.Where(_ => _.Value.Count == 2).Select(_ => _.Value[0] * _.Value[1]).Sum();
		}

	}
}