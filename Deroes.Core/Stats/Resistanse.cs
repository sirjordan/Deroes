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
		public Stat<MaxResist> Max { get; private set; }
		public int Min { get; private set; }

		public ElementalResistance() : this(0) { }

		public ElementalResistance(int amount) : base(amount)
		{
			Min = -75;
			Max = new Stat<MaxResist>(new());
			Amount = Math.Clamp(amount, Min, Max.Value.Value);
			Immune = false;
		}

		public override Resistanse Modify(int modificator) => new ElementalResistance(Amount + modificator);
	}

	public class MaxResist : IStatModifiable<MaxResist>
	{
		public const int MAX = 95;
		public const int DEFAULT = 75;

		public int Value { get; private set; }

		public MaxResist() : this(DEFAULT) { }

		public MaxResist(int value)
		{
			ArgumentOutOfRangeException.ThrowIfLessThan(value, DEFAULT);
			ArgumentOutOfRangeException.ThrowIfGreaterThan(value, MAX);

			Value = value;
		}

		public MaxResist Modify(int modificator)
		{
			var amount = Math.Clamp(Value + modificator, DEFAULT, MAX);

			return new MaxResist(amount);
		}
	}
}
