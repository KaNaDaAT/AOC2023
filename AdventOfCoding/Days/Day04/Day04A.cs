using Lib;
using System;
using System.Linq;

namespace AdventOfCoding.Days {
	public class Day04A : DayAbstract {

		protected override void Runner(Reader reader)
		{
			this.Result = reader.ReadAndGetLines()
				.Select(line => line.Split(':')[1].Trim().Split(" | "))
				.Select(pair => pair[0].Split(" ", (StringSplitOptions)1).Intersect(pair[1].Split(" ", (StringSplitOptions)1)).Count())
				.Sum(value => (int)Math.Pow(2, value - 1));
		}

	}
}