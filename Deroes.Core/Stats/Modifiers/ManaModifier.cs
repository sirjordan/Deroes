namespace Deroes.Core.Stats.Modifiers
{
	public class ManaFlatModifier(int value) : IStatModifier<Vital>, IStatModifier
	{
		private readonly int _value = value;

		public string Description => $"{_value} to Mana";
		public int Order => 1;

		public int GetModificator(Vital @base) => _value;
		public void ApplyModification(Hero h) => h.Mana.AddModifier(this);
		public void RemoveModification(Hero h) => h.Mana.RemoveModifier(this);

	}

	public class ManaPercentageModifier(int value) : IStatModifier<Vital>, IStatModifier
	{
		private readonly int _value = value;

		public string Description => $"{_value} % to maximum Mana";
		public int Order => 100;

		public int GetModificator(Vital @base) => (int)(@base.Max * (_value / 100.0));
		public void ApplyModification(Hero h) => h.Mana.AddModifier(this);
		public void RemoveModification(Hero h) => h.Mana.RemoveModifier(this);

	}
}
