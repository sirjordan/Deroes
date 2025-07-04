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
}
