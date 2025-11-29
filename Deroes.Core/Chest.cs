using Deroes.Core.Items;
using Deroes.Core.Units;

namespace Deroes.Core
{
	public class Chest
	{
		public Stash<Item> Stash { get; private set; }
		public GoldBag Gold { get; private set; }

		public Chest(Hero h)
		{
			Stash = new Stash<Item>(10, 10);
			Gold = new GoldBag(h, 2.5);
		}
	}
}
