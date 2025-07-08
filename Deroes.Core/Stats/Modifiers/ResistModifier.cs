namespace Deroes.Core.Stats.Modifiers
{
	public abstract class FlatResistModifier : IStatModifier<Resistanse>, IStatModifier
	{
		private int _value;

		public int Order => 1;
		public string Description => $"{_value} to {GetType().Name} resist";
		protected abstract Func<Hero, Stat<Resistanse>> Selector { get; }

		protected FlatResistModifier(int value)
		{
			_value = value;
		}

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
}
