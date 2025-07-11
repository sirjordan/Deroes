using Deroes.Core.Stats.Modifiers;

namespace Deroes.Core.Items.Wearables
{
	public abstract class Wearable(WearableItemSpec itemSpec) : Item
	{
		public WearableItemSpec ItemSpec { get; private set; } = itemSpec;
	}

	public abstract class DefenseItem : Wearable
	{
		protected DefenseItem(DefenseItemSpec itemSpec) : base(itemSpec)
		{
			ArgumentOutOfRangeException.ThrowIfNegative(itemSpec.Defense);

			ItemSpec.Modifiers.Add(new PhysicalResistModifier(itemSpec.Defense));
		}
	}

	public class Weapon : Wearable
	{
		public Weapon(WeaponItemSpec itemSpec) : base(itemSpec)
		{
			ItemSpec.Modifiers.Add(
				new PhysicalDamageModifier(
					new FlatDamageModifier(itemSpec.Min),
					new FlatDamageModifier(itemSpec.Max)));
		}
	}

	public class Helm(DefenseItemSpec itemSpec) : DefenseItem(itemSpec)
	{
	}

	public class Armor(DefenseItemSpec itemSpec) : DefenseItem(itemSpec)
	{
	}

	public class Shield(DefenseItemSpec itemSpec) : DefenseItem(itemSpec)
	{
	}

	public class Gloves(DefenseItemSpec itemSpec) : DefenseItem(itemSpec)
	{
	}

	public class Boots(DefenseItemSpec itemSpec) : DefenseItem(itemSpec)
	{
	}

	public class Belt : DefenseItem
	{
		public const int COLS = 4;
		private Stash<Potion> _potions;

		public Belt(BeltItemSpec itemSpec) : base(itemSpec)
		{
			_potions = new Stash<Potion>(COLS, itemSpec.Rows);
		}

		/// <summary>
		/// Add potion at free space in belt
		/// </summary>
		public bool Add(Potion p)
		{
			// TODO: If existing potions, place it under the same type
			return _potions.Add(p);
		}

		public bool Add(Potion p, int row, int col)
		{
			// TODO: Add at bottom of the stash (reverse row)
			return _potions.Add(p, row, col);
		}

		/// <summary>
		/// Gets and removes it from the belt
		/// </summary>
		public Potion? Drop(int row, int col)
		{
			if (_potions.Peek(row, col) != null)
			{
				var p = _potions.Drop(row, col);
				// TODO: Reaggrange potions to be stacked at the bottom
				return p;
			}

			return null;
		}
	}

	public class Ring(WearableItemSpec itemSpec) : Wearable(itemSpec)
	{
	}

	public class Amulet(WearableItemSpec itemSpec) : Wearable(itemSpec)
	{
	}
}
