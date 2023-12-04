using Lib;
using System;
using System.Linq;

namespace AdventOfCoding.Days {
	public class Day04B : DayAbstract {

		class Card {
			public long Count;
			public int Worth;
			public Card(int worth) { Count = 1; Worth = worth; }
		}

		protected override void Runner(Reader reader)
		{
			this.Result = reader.ReadAndGetLines()
				.Select(line => line.Split(':')[1].Trim().Split(" | "))
				.Select(pair => new Card(pair[0].Split(" ", (StringSplitOptions)1).Intersect(pair[1].Split(" ", (StringSplitOptions)1)).Count()))
				.ToList()
				.SelectSubset(
					(card, index, list) => list.Skip(index + 1).Take(card.Worth).ToList(),
					(card, index, next) => {
						next.Select(_ => _.Count += card.Count).ToList();
						return card.Count;
					}
				)
				.Sum();
		}

		public void MoreReadableVersion(Reader reader)
		{

			var list = reader.ReadAndGetLines()
				.Select(line => line.Split(':')[1].Trim().Split(" | "))
				.Select(pair => new Card(pair[0].Split(" ", (StringSplitOptions)1).Intersect(pair[1].Split(" ", (StringSplitOptions)1)).Count()))
				.ToList();
			for (int i = 0; i < list.Count(); i++)
			{
				list.Skip(i + 1).Take(list[i].Worth).Select((card, index) => card.Count += list[i].Count).ToList();
			}
			this.Result = list.Sum(_ => _.Count);
		}

	}
}