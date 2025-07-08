namespace Deroes.Core.Stats
{
	/// <summary>
	/// Phisycal, Cold, Fire, Lightining, Poison?, Magic
	/// </summary>
	public class Resistanse
	{
		public bool Immune { get; private set; }
		public int Amount { get; private set; }

		public Resistanse()
		{
			Amount = 0;
			Immune = false;
		}
	}
}
