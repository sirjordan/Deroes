using Deroes.Core.Items;

namespace Deroes.Core
{
	/// <summary>
	/// Inventory, Private Stash, Belt, Cube
	/// </summary>
	public class Stash<T> where T : Item
	{
		private T?[,] Space { get; set; }

		public Stash(int width, int height)
		{
			ArgumentOutOfRangeException.ThrowIfLessThan(width, 1);
			ArgumentOutOfRangeException.ThrowIfLessThan(height, 1);

			Space = new T[height, width];
		}

		/// <summary>
		/// Adds an item at the first free space
		/// </summary>
		/// <returns>If adding is done/possible</returns>
		public bool Add(T item) 
		{
			var added = false;

			for (int row = 0; row <= Space.GetLength(0) - item.Width ; row++)
			{
				for (int col = 0; col <= Space.GetLength(1) - item.Height; col++)
				{
					if (Peek(row, col) == null)
					{
						Add(item, row, col);
						added = true;
						break;
					}
				}

				if (added)
					break;
			}

			return added;
		}

		/// <summary>
		/// Adds an item at the specific location
		/// </summary>
		/// <returns>If adding is done/possible</returns>
		public bool Add(T item, int top, int left) 
		{
			if (Peek(top, left) != null) 
				return false;

			for (int row = top; row < top + item.Width; row++)
			{
				for (int col = left; col < left + item.Height; col++)
				{
					Space[row, col] = item;
				}
			}

			return true;
		}

		/// <summary>
		/// Get/peek an item from specific location
		/// </summary>
		public T? Peek(int top, int left) 
		{
			return Space[top, left];
		}

		public T Drop(int top, int left)
		{
			var item = Space[top, left];

			ArgumentNullException.ThrowIfNull(item);

			for (int row = top; row < top + item.Width; row++)
			{
				for (int col = left; col < left + item.Height; col++)
				{
					Space[row, col] = null;
				}
			}

			return item;
		}
	}
}
