using Deroes.Core.Units;

namespace Deroes.Core.Stats.Modifiers
{
	public abstract class FlatResistModifier(int value) : IStatModifier<Resistanse>, IStatModifier
	{
		private int _value = value;

		public int Order => 100;
		public string Description => $"{_value} to {GetType().Name} resist";
		protected abstract Func<Hero, Stat<Resistanse>> Selector { get; }

		public void ApplyModification(Hero h) => Selector(h).AddModifier(this);
		public int GetModificator(Resistanse @base) => _value;
		public void RemoveModification(Hero h) => Selector(h).RemoveModifier(this);
	}

	public abstract class MaxResistModifier (int value) : IStatModifier<Resistanse>, IStatModifier
	{
		private int _value = value;

		public string Description => $"{_value} to Maximum {GetType().Name} resist";
		public int Order => 1;
		protected abstract Func<Hero, Stat<Resistanse>> Selector { get; }

		public void ApplyModification(Hero h) => Selector(h).AddModifier(this);
		public int GetModificator(Resistanse @base) => _value;
		public void RemoveModification(Hero h) => Selector(h).RemoveModifier(this);
	}

	public class PhysicalResistModifier(int value) : FlatResistModifier(value)
	{
		protected override Func<Hero, Stat<Resistanse>> Selector => h => h.Resistanse.Physical;
	}

	public class ColdResistModifier(int value) : FlatResistModifier(value)
	{
		protected override Func<Hero, Stat<Resistanse>> Selector => h => h.Resistanse.Cold;
	}

	public class FireResistModifier(int value) : FlatResistModifier(value)
	{
		protected override Func<Hero, Stat<Resistanse>> Selector => h => h.Resistanse.Fire;
	}
}
