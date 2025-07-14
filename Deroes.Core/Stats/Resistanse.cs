using Deroes.Core.Stats.Modifiers;

namespace Deroes.Core.Stats
{
	/// <summary>
	/// Phisycal, Cold, Fire, Lightining, Poison?, Magic
	/// </summary>
	public class Resistanse : IStatModifiable<Resistanse>
	{
		public bool Immune { get; private set; }
		public int Amount { get; private set; }

		public Resistanse() : this(0) { }

		public Resistanse(int  amount)
		{
			Amount = amount;
			Immune = false;
		}

		public Resistanse Modify(int modificator)
		{
			return new Resistanse(Amount + modificator);
		}
	}
}
