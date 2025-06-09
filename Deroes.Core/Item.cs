namespace Deroes.Core
{
	public abstract class Item
	{
		public string Name { get; protected set; }
		/// <summary>
		/// Width in the inventory
		/// </summary>
		public byte Width { get; protected set; }
		/// <summary>
		/// Height in the inventory
		/// </summary>
		public byte Height { get; protected set; }
	}

	public class Helm : Item
	{
		public int Defense { get; set; }
	}

	public abstract class HandItem : Item
	{ }

	public class Weapon : HandItem
	{ }
}
