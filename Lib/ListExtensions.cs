using System;
using System.Collections.Generic;
using System.Linq;

namespace Lib {
	public static class ListExtensions {

		public static IEnumerable<TResult> SelectSubset<TSource, TResult>(
			this IEnumerable<TSource> source,
			Func<TSource, bool> subsetSelector,
			Func<TSource, List<TSource>, TResult> selector
		)
		{
			ArgumentNullException.ThrowIfNull(source);
			ArgumentNullException.ThrowIfNull(subsetSelector);
			ArgumentNullException.ThrowIfNull(selector);

			var list = source.Where(_ => subsetSelector.Invoke(_)).ToList();
			return source.Select(_ => selector.Invoke(_, list));
		}

		public static IEnumerable<TResult> SelectSubset<TSource, TResult>(
			this IEnumerable<TSource> source,
			Func<TSource, int, bool> subsetSelector,
			Func<TSource, List<TSource>, TResult> selector
		)
		{
			ArgumentNullException.ThrowIfNull(source);
			ArgumentNullException.ThrowIfNull(subsetSelector);
			ArgumentNullException.ThrowIfNull(selector);

			var list = source.Where(subsetSelector.Invoke).ToList();
			return source.Select(_ => selector.Invoke(_, list));
		}

		public static IEnumerable<TResult> SelectSubset<TSource, TResult>(
			this IEnumerable<TSource> source,
			Func<IEnumerable<TSource>, IEnumerable<TSource>> subsetSelector,
			Func<TSource, List<TSource>, TResult> selector
		)
		{
			ArgumentNullException.ThrowIfNull(source);
			ArgumentNullException.ThrowIfNull(subsetSelector);
			ArgumentNullException.ThrowIfNull(selector);

			var list = subsetSelector(source).ToList();
			return source.Select(_ => selector.Invoke(_, list));
		}

		public static IEnumerable<TResult> SelectSubset<TSource, TResult>(
			this IEnumerable<TSource> source,
			Func<TSource, int, IEnumerable<TSource>, IEnumerable<TSource>> subsetSelector,
			Func<TSource, int, IEnumerable<TSource>, TResult> selector
		)
		{
			ArgumentNullException.ThrowIfNull(source);
			ArgumentNullException.ThrowIfNull(subsetSelector);
			ArgumentNullException.ThrowIfNull(selector);

			return source.Select((el, index) => selector(el, index, subsetSelector(el, index, source)));
		}

		public static IEnumerable<TResult> SelectSubset<TSource, TResult>(
			this IEnumerable<TSource> source,
			int from,
			int to,
			Func<TSource, List<TSource>, TResult> selector
		)
		{
			ArgumentNullException.ThrowIfNull(source);
			ArgumentNullException.ThrowIfNull(selector);

			var list = source.Skip(from).Take(to - from).ToList();
			return source.Select(_ => selector.Invoke(_, list));
		}
	}
}
