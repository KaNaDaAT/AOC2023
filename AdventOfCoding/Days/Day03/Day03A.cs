using Lib;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCoding.Days {
	public class Day03A : DayAbstract {

		protected override void Runner(Reader reader)
		{
			var grid = new Grid<char>(reader.ReadAndGetLines().Select(_ => _.ToCharArray()).ToArray());

			this.Result = grid.Select(_ => {
				if (!char.IsDigit(grid.Get(_.x, _.y)) || char.IsDigit(grid.Get(_.x - 1, _.y, '1')))
					return 0;
				List<(int x, int y, int value)> numbers = new() { (_.x, _.y, grid.Get(_.x, _.y) - '0') };
				for (int i = 1; i < grid.ColumnSize - _.x; i++)
				{
					var right = grid.Get(_.x + i, _.y, ' ');
					if (!char.IsDigit(right)) break;
					numbers.Add((_.x + i, _.y, right - '0'));
				}
				var count = numbers.SelectMany(_ => grid.GetAdjcentWithIndex(_.x, _.y, false))
					.Distinct()
					.Count(_ => _.Value != '.' && !char.IsDigit(_.Value));
				this.ToPrint.AppendLine(count + ": " + numbers.Select(_ => _.value).Aggregate((acc, num) => acc * 10 + num));
				return numbers.Select(_ => _.value).Aggregate((acc, num) => acc * 10 + num) * count;
			}).Sum();
		}

	}
}