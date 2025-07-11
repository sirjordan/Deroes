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
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(itemSpec.Defense);

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

	public class Belt(DefenseItemSpec itemSpec) : DefenseItem(itemSpec)
	{
		// TDOO: Stash, Invisible belt for begining
	}

	public class Ring(WearableItemSpec itemSpec) : Wearable(itemSpec)
	{
	}

	public class Amulet(WearableItemSpec itemSpec) : Wearable(itemSpec)
	{
	}
}
