namespace Deroes.Core.Stats.Modifiers
{
	public abstract class VitalFlatModifier(int value) : IStatModifier<Vital>, IStatModifier
	{
		private readonly int _value = value;

		public string Description => $"{_value} to {GetType().Name}";
		public int Order => 1;
		protected abstract Func<Hero, Stat<Vital>> Selector { get; }

		public int GetModificator(Vital @base) => _value;
		public void ApplyModification(Hero h) => Selector(h).AddModifier(this);
		public void RemoveModification(Hero h) => Selector(h).RemoveModifier(this);
	}

	public abstract class VitalPercentageModifier(int value) : IStatModifier<Vital>, IStatModifier
	{
		private readonly int _value = value;

		public string Description => $"{_value} % to maximum {GetType().Name}";
		public int Order => 100;
		protected abstract Func<Hero, Stat<Vital>> Selector { get; }

		public int GetModificator(Vital @base) => (int)(@base.Max * (_value / 100.0));
		public void ApplyModification(Hero h) => h.Mana.AddModifier(this);
		public void RemoveModification(Hero h) => h.Mana.RemoveModifier(this);
	}

	public class ManaFlatModifier(int value) : VitalFlatModifier(value)
	{
		protected override Func<Hero, Stat<Vital>> Selector => h => h.Mana;
	}

	public class ManaPercentageModifier(int value) : VitalPercentageModifier(value)
	{
		protected override Func<Hero, Stat<Vital>> Selector => h => h.Mana;
	}

	public class LifeFlatModifier(int value) : VitalFlatModifier(value)
	{
		protected override Func<Hero, Stat<Vital>> Selector => h => h.Life;
	}

	public class LifePercentageModifier(int value) : VitalPercentageModifier(value)
	{
		protected override Func<Hero, Stat<Vital>> Selector => h => h.Life;
	}

	public class StaminaModifier(int value) : VitalPercentageModifier(value)
	{
		protected override Func<Hero, Stat<Vital>> Selector => h => h.Stamina;
	}
}
