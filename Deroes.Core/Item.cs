namespace Deroes.Core
{
	public abstract class Item
	{
		public virtual string Name { get; protected set; }
		/// <summary>
		/// Width in the inventory
		/// </summary>
		public virtual byte Width { get; protected set; }
		/// <summary>
		/// Height in the inventory
		/// </summary>
		public virtual byte Height { get; protected set; }

		public virtual int RequiredLevel { get; protected set; }

		protected Item()
		{
			Name = GetType().Name;
			Width = 1;
			Height = 1;
			RequiredLevel = 1;
		}
	}

	public interface IWearableItem
	{
		int RequiredStrength { get; }
		int RequiredDexterity { get;  }
	}

	public interface IDefenseItem
	{
		int Defense { get; }
	}

	public class Helm : IWearableItem, IDefenseItem
	{
		public int Defense { get; private set; }
		public int RequiredStrength => throw new NotImplementedException();
		public int RequiredDexterity => throw new NotImplementedException();
	}

	public class Armor : IWearableItem, IDefenseItem
	{
		public int Defense { get; private set; }
		public int RequiredStrength => throw new NotImplementedException();
		public int RequiredDexterity => throw new NotImplementedException();
	}

	public abstract class HandItem : IWearableItem
	{
		public int RequiredStrength => throw new NotImplementedException();
		public int RequiredDexterity => throw new NotImplementedException();
	}

	public class Weapon : HandItem
	{
	}

	public class Shield : HandItem
	{
	}
}
