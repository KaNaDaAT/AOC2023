using System.Numerics;

namespace Lib.Extensions {
	public static class NumericExtension {

		public static bool Between<TValue>(this TValue value, TValue min, TValue max)
			where TValue : INumber<TValue>
		{
			if (min > max) (min, max) = (max, min);
			return min <= value && value <= max;
		}
	}
}
