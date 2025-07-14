namespace Deroes.Core.Stats.Modifiers
{
	public interface IStatModifiable<T>
	{
		/// <summary>
		/// Get new modified instance
		/// </summary>
		/// <param name="modificator">Value to modifie with</param>
		public T Modify(int modificator);
	}
}
