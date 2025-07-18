using Deroes.Core.Stats.Modifiers;

namespace Deroes.Core.Stats
{
	public abstract class Resistanse : IStatModifiable<Resistanse>
	{
		public bool Immune { get; protected set; }
		public int Amount { get; protected set; }

		protected Resistanse(int amount)
		{
			Amount = amount;
			Immune = false;
		}

		public abstract Resistanse Modify(int modificator);
	}

	/// <summary>
	/// E.g Defense
	/// </summary>
	public class PhysicalResistanse : Resistanse
	{
		public PhysicalResistanse() : this(0) { }
		public PhysicalResistanse(int amount) : base(amount) { }

		public override Resistanse Modify(int modificator) => new PhysicalResistanse(Amount + modificator);
	}

	/// <summary>
	/// Cold, Fire, Lightining, Poison, Magic?
	/// </summary>
	public class ElementalResistance : Resistanse
	{
		public int Max { get; private set; }
		public int Min { get; private set; }

		public ElementalResistance() : this(0) { }

		public ElementalResistance(int amount) : base(amount)
		{
			Max = 75;
			Min = -75;
			Amount = Math.Clamp(amount, Min, Max);
			Immune = false;
		}

		public override Resistanse Modify(int modificator) => new ElementalResistance(Amount + modificator);
	}
}
