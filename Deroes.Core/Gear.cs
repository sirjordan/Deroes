namespace Deroes.Core
{
	public abstract class WearableItem : Item
	{
		public int RequiredStrength { get; protected set; }
		public int RequiredDexterity { get; protected set; }
		public int Durability { get; protected set; }
	}

	public abstract class DefenseItem : WearableItem
	{
		public int Defense { get; protected set; }
	}

	public abstract class Weapon : WearableItem
	{
		public int MinDamage { get; protected set; }
		public int MaxDamage { get; protected set; }
		public int AttackSpeed { get; protected set; }
	}

	public class Helm : DefenseItem { }

	public class Armor : DefenseItem { }

	public class Shield : DefenseItem
	{
		public int ChanceToBlock { get; private set; }
	}

	public class Gloves : DefenseItem { }

	public class Boots : DefenseItem { }

	public class Belt : DefenseItem { }

	public class Ring : Item { }

	public class Amulet : Item { }
}
