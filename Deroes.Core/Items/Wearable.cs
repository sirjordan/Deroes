using Deroes.Core.Stats;

namespace Deroes.Core.Items
{
	public abstract class WearableItem : Item
	{
		public int RequiredStrength { get; protected set; }
		public int RequiredDexterity { get; protected set; }
		public int Durability { get; protected set; }
		public IEnumerable<IStatModifier> Modifiers { get; protected set; }

		protected WearableItem(int requiredStrength, int requiredDexterity, int durability, IEnumerable<IStatModifier> modifiers)
		{
			RequiredStrength = requiredStrength;
			RequiredDexterity = requiredDexterity;
			Durability = durability;
			Modifiers = modifiers;
		}
	}

	public abstract class DefenseItem(int requiredStrength, int requiredDexterity, int durability, IEnumerable<IStatModifier> modifiers) 
		: WearableItem(requiredStrength, requiredDexterity, durability, modifiers)
	{
		public int Defense { get; protected set; }
	}

	public class Weapon(int requiredStrength, int requiredDexterity, int durability, IEnumerable<IStatModifier> modifiers) 
		: WearableItem(requiredStrength, requiredDexterity, durability, modifiers)
	{
		public Physical Damage { get; protected set; }
		public int AttackSpeed { get; protected set; }
		public bool IsTwoHanded { get; protected set; }
	}

	public class Helm : DefenseItem
	{
		public Helm(int requiredStrength, int requiredDexterity, int durability, IEnumerable<IStatModifier> modifiers) : base(requiredStrength, requiredDexterity, durability, modifiers)
		{
		}
	}

	public class Armor : DefenseItem
	{
		public Armor(int requiredStrength, int requiredDexterity, int durability, IEnumerable<IStatModifier> modifiers) : base(requiredStrength, requiredDexterity, durability, modifiers)
		{
		}
	}

	public class Shield : DefenseItem
	{
		public Shield(int requiredStrength, int requiredDexterity, int durability, IEnumerable<IStatModifier> modifiers) : base(requiredStrength, requiredDexterity, durability, modifiers)
		{
		}

		public int ChanceToBlock { get; private set; }
	}

	public class Gloves : DefenseItem
	{
		public Gloves(int requiredStrength, int requiredDexterity, int durability, IEnumerable<IStatModifier> modifiers) : base(requiredStrength, requiredDexterity, durability, modifiers)
		{
		}
	}

	public class Boots : DefenseItem
	{
		public Boots(int requiredStrength, int requiredDexterity, int durability, IEnumerable<IStatModifier> modifiers) : base(requiredStrength, requiredDexterity, durability, modifiers)
		{
		}
	}

	public class Belt : DefenseItem
	{
		public Belt(int requiredStrength, int requiredDexterity, int durability, IEnumerable<IStatModifier> modifiers) : base(requiredStrength, requiredDexterity, durability, modifiers)
		{
		}
	}

	public class Ring() : WearableItem(0, 0, 0, [])
	{
	}

	public class Amulet() : WearableItem(0, 0, 0, [])
	{
	}
}
