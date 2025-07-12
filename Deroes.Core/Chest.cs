using Deroes.Core.Items;

namespace Deroes.Core
{
	public class Chest
	{
		public Stash<Item> Stash { get; private set; }
		public Gold Gold { get; private set; }

		public Chest(Hero h)
		{
			Stash = new Stash<Item>(10, 10);
			Gold = new Gold(h, 2.5);
		}
	}
}
