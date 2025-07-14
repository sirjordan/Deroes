using Deroes.Core.Units;

namespace Deroes.Core
{
	public class Gold
	{
		private double _maxGoldCoef;
		private Hero _hero;

		public int Max => (int)(_maxGoldCoef * _hero.Level * 10000);
		public int Amount { get; private set; }

		public Gold(Hero h, double maxGoldCoef = 1.0)
		{
			_maxGoldCoef = maxGoldCoef;
			_hero = h;
			Amount = 0;
		}

		public int Add(int amount)
		{
			ArgumentOutOfRangeException.ThrowIfLessThan(amount, 0);

			Amount += amount;

			if (Amount > Max)
			{
				var drop = Amount - Max;

				return Take(drop);
			}
			else
			{
				return 0;
			}
		}

		public int Take(int amount)
		{
			if (amount <= Amount)
			{
				Amount -= amount;

				return amount;
			}
			else
			{
				return Take(Amount);
			}
		}
	}
}
