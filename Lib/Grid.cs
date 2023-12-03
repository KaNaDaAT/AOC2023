using System;
using System.Collections.Generic;
using System.Text;

namespace Lib {
	public enum Direction {
		UP = 0,
		RIGHT = 1,
		DOWN = 2,
		LEFT = 3,
		UPRIGHT = 4,
		DOWNRIGHT = 5,
		DOWNLEFT = 6,
		UPLEFT = 7
	}
	public enum StringFormat {
		XY,
		YX,
		List
	}

	public class Grid<T> {

		private T[] _data;
		public int RowSize { get; private set; }
		public int ColumnSize { get; private set; }
		public int Size
		{
			get {
				return RowSize * ColumnSize;
			}
		}

		public Grid(T[][] data, bool xySwitched = false)
		{
			if (data.Length == 0)
			{
				RowSize = 0;
				ColumnSize = 0;
				_data = new T[0];
				return;
			}
			RowSize = data[0].Length;
			ColumnSize = data.Length;
			T[] tempdata = new T[RowSize * ColumnSize];
			int sizeInner = data[0].Length;
			for (int i = 0; i < data.Length; i++)
			{
				if (sizeInner != data[i].Length)
				{
					throw new ArgumentException("All rows and columns of the data have to be the same size!", "data");
				}
			}
			if (xySwitched)
			{
				(RowSize, ColumnSize) = (ColumnSize, RowSize);
				for (int x = 0; x < data.Length; x++)
				{
					for (int y = 0; y < data[0].Length; y++)
					{
						tempdata[y * data.Length + x] = data[x][y];
					}
				}
			}
			else
			{
				for (int x = 0; x < data.Length; x++)
				{
					for (int y = 0; y < data[0].Length; y++)
					{
						tempdata[x * data[0].Length + y] = data[x][y];
					}
				}
			}
			_data = tempdata;
		}

		public Grid(T[,] data, bool xySwitched = false)
		{

		}

		public Grid(T[] data, int rowSize)
		{
			int tempcolumnsize = data.Length / rowSize;
			if (tempcolumnsize * rowSize != data.Length)
			{
				throw new ArgumentException("The given data and row size do not match!", "data");
			}
			this.RowSize = rowSize;
			this.ColumnSize = tempcolumnsize;
			_data = data;
		}

		public Grid(T[] data) : this(data, (int)Math.Sqrt(data.Length)) { }

		public Grid(Grid<T> grid)
		{
			this._data = (T[])grid._data.Clone();
			this.RowSize = grid.RowSize;
			this.ColumnSize = grid.ColumnSize;
		}

		public void Set(T value, int column, int row)
		{
			if (ValidateIndex(column, row))
			{
				_data[column + this.RowSize * row] = value;
			}
		}

		public void Set(T value, int index)
		{
			if (ValidateIndex(index))
			{
				_data[index] = value;
			}
		}

		public void SetCreate(T value, int column, int row)
		{
			// if index doesnt exist create it
		}

		public void SetCreate(T value, int index)
		{
			// if index doesnt exist create it
		}

		public T Get(int column, int row)
		{
			if (ValidateIndex(column, row))
			{
				return _data[column + this.RowSize * row];
			}
			return default(T);
		}

		public T Get(int column, int row, T back)
		{
			if (IsValidIndex(column, row))
			{
				return _data[column + this.RowSize * row];
			}
			return back;
		}

		public T Get(int index)
		{
			if (ValidateIndex(index))
			{
				return _data[index];
			}
			return default(T);
		}

		public int GetIndex(int index, Direction direction)
		{
			int column = index % RowSize;
			int row = index / RowSize;
			return GetIndex(column, row, direction);
		}

		public int GetIndex(int column, int row, Direction direction)
		{
			int index = 0;
			switch (direction)
			{
				case Direction.UP:
					index = (row - 1) * RowSize + column;
					break;
				case Direction.RIGHT:
					index = (row) * RowSize + column + 1;
					break;
				case Direction.DOWN:
					index = (row + 1) * RowSize + column;
					break;
				case Direction.LEFT:
					index = (row) * RowSize + column - 1;
					break;
			}
			if (IsValidIndex(index))
			{
				return index;
			}
			else
			{
				return -1;
			}
		}

		public int IndexToColumn(int index)
		{
			return index % RowSize;
		}

		public int IndexToRow(int index)
		{
			return index / RowSize;
		}

		public T GetNeighbour(int index, Direction direction)
		{
			return default(T);
		}

		public T TryGetNeighbour(int index, Direction direction, T defaultValue = default(T))
		{
			return default(T);
		}

		public List<T> GetAdjcent(int column, int row, bool ignoreCorners = true)
		{
			List<T> adjcent = new List<T>();
			if (IsValidIndex(column - 1, row))
			{
				adjcent.Add(Get(column - 1, row));
			}
			if (IsValidIndex(column + 1, row))
			{
				adjcent.Add(Get(column + 1, row));
			}
			if (IsValidIndex(column, row - 1))
			{
				adjcent.Add(Get(column, row - 1));
			}
			if (IsValidIndex(column, row + 1))
			{
				adjcent.Add(Get(column, row + 1));
			}
			if (!ignoreCorners)
			{
				if (IsValidIndex(column - 1, row - 1))
				{
					adjcent.Add(Get(column - 1, row - 1));
				}
				if (IsValidIndex(column - 1, row + 1))
				{
					adjcent.Add(Get(column - 1, row + 1));
				}
				if (IsValidIndex(column + 1, row - 1))
				{
					adjcent.Add(Get(column + 1, row - 1));
				}
				if (IsValidIndex(column + 1, row + 1))
				{
					adjcent.Add(Get(column + 1, row + 1));
				}
			}
			return adjcent;
		}

		public List<(int X, int Y, T Value)> GetAdjcentWithIndex(int column, int row, bool ignoreCorners = true)
		{
			List<(int X, int Y, T Value)> adjcent = new List<(int X, int Y, T Value)>();
			if (IsValidIndex(column - 1, row))
				adjcent.Add((column - 1, row, Get(column - 1, row)));
			if (IsValidIndex(column + 1, row))
				adjcent.Add((column + 1, row, Get(column + 1, row)));
			if (IsValidIndex(column, row - 1))
				adjcent.Add((column, row - 1, Get(column, row - 1)));
			if (IsValidIndex(column, row + 1))
				adjcent.Add((column, row + 1, Get(column, row + 1)));
			if (!ignoreCorners)
			{
				if (IsValidIndex(column - 1, row - 1))
					adjcent.Add((column - 1, row - 1, Get(column - 1, row - 1)));
				if (IsValidIndex(column - 1, row + 1))
					adjcent.Add((column - 1, row + 1, Get(column - 1, row + 1)));
				if (IsValidIndex(column + 1, row - 1))
					adjcent.Add((column + 1, row - 1, Get(column + 1, row - 1)));
				if (IsValidIndex(column + 1, row + 1))
					adjcent.Add((column + 1, row + 1, Get(column + 1, row + 1)));
			}
			return adjcent;
		}

		public List<T> GetAdjcent(int index, bool ignoreCorners = false)
		{
			int column = index % RowSize;
			int row = index / RowSize;
			return GetAdjcent(column, row, ignoreCorners);
		}

		public List<T> GetInRadius(int column, int row, int radius = 1, bool ignoreCorners = false)
		{
			List<T> inradius = new List<T>();
			return inradius;
		}

		public List<T> GetInRadius(int index, int radius = 1, bool ignoreCorners = false)
		{
			return new List<T>();
		}

		public void AddColumn(int column = -1)
		{

		}

		public void AddRow(int row = -1)
		{

		}

		public void RemoveColumn(int column = -1)
		{

		}

		public void RemoveRow(int row = -1)
		{

		}

		public T[] ToArray()
		{
			return _data;
		}

		public T[][] To2DArray()
		{
			// TODO XY Switched
			T[][] data = new T[this.ColumnSize][];
			for (int x = 0; x < RowSize; x++)
			{
				T[] subdata = new T[this.RowSize];
				for (int y = 0; y < RowSize; y++)
				{
					subdata[y] = _data[x * RowSize + y];
				}
				data[x] = subdata;
			}
			return data;
		}

		public string ToString(StringFormat format)
		{
			StringBuilder str = new StringBuilder();
			switch (format)
			{
				case StringFormat.List:
					for (int i = 0; i < _data.Length; i++)
					{
						str.Append(_data[i]);
						if (i + 1 != _data.Length)
						{
							str.Append(", ");
						}
						else
						{
							str.AppendLine("");
						}
					}
					break;
				default:
					break;
			}
			return str.ToString();
		}

		public bool IsValidIndex(int column, int row)
		{
			return (column >= 0 && column < this.ColumnSize) && (row >= 0 && row < this.RowSize);
		}

		public bool IsValidIndex(int index)
		{
			return index >= 0 && index < _data.Length;
		}

		private bool ValidateIndex(int column, int row)
		{
			if (column < 0 || column >= this.ColumnSize)
			{
				throw new ArgumentOutOfRangeException("The given column is outside of the allowed range 0 to " + ColumnSize, "column");
			}
			else if (row < 0 || row >= this.RowSize)
			{
				throw new ArgumentOutOfRangeException("The given row is outside of the allowed range 0 to " + RowSize, "row");
			}
			return true;
		}

		private bool ValidateIndex(int index)
		{
			if (index < 0 || index >= _data.Length)
			{
				throw new IndexOutOfRangeException("Given index is outside of the data range");
			}
			return true;
		}

	}

	public static class GridExtensions {
		public static void ForEach<T>(this Grid<T> grid, Action<(int x, int y)> action)
		{
			for (int x = 0; x < grid.ColumnSize; x++)
			{
				for (int y = 0; y < grid.RowSize; y++)
				{
					action.Invoke((x, y));
				}
			}
		}
		public static IEnumerable<TOut> Select<TValue, TOut>(this Grid<TValue> grid, Func<(int x, int y), TOut> action)
		{
			for (int x = 0; x < grid.ColumnSize; x++)
			{
				for (int y = 0; y < grid.RowSize; y++)
				{
					yield return action.Invoke((x, y));
				}
			}
		}
	}
}
