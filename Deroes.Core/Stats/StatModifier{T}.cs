namespace Deroes.Core.Stats
{
	/// <summary>
	/// Applies a modification on hero
	/// Holds the 'on what' modification is applied
	/// </summary>
	public interface IStatModifier
	{
		public void ApplyModification(Hero h);
		public void RemoveModification(Hero h);
	}

	/// <summary>
	/// Holds 'how' modification is calculated
	/// </summary>
	public interface IStatModifier<T> 
	{
		public string Description { get; }

		/// <summary>
		/// Calculated value to include in modification
		/// Static value or Percentage
		/// </summary>
		public int GetModificator(T @base);

		/// <summary>
		/// Order of execution
		/// </summary>
		public int Order { get; }
	}
}
