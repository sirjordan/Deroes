using Deroes.Core.Items;

namespace Deroes.Core
{
	public class Chest
	{
		private readonly Hero _hero;
		public int MaxGold => (int)(_hero.MaxGold * 2.5);
		public Stash<Item> Stash { get; private set; }
		public int Gold { get; private set; }

		public Chest(Hero h)
		{
			_hero = h;
			Stash = new Stash<Item>(10, 10);
			Gold = 0;
		}

		// TODO: Unit test this
		// TODO: Similar to Hero's Gold system -> Unite them under 1 object: Gold.cs
		public int AddGold(int amount)
		{
			Gold += amount;

			if (Gold > MaxGold)
			{
				var drop = Gold - MaxGold;

				return TakeGold(drop);
			}
			else
			{
				return 0;
			}
		}

		public int TakeGold(int amount)
		{
			if (amount <= Gold)
			{
				Gold -= amount;

				return amount;
			}
			else
			{
				return TakeGold(Gold);
			}
		}
	}
}
