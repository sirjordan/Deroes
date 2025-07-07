namespace Deroes.Core.Stats.Modifiers
{
	public class StatModificator
	{
		public int Bonus { get; private set; }
	}

	public class DamageModifier : IStatModifier<DamageRange>, IStatModifier
	{
		private DamageRange _bonus;

		public string Description => $"{_bonus.Min} - {_bonus.Max} {_bonus.GetType().Name} damage";

		public int Order => 1;

		public DamageModifier(DamageRange bonus)
		{
			_bonus = bonus;
		}

		public int GetModificator(DamageRange @base)
		{
			throw new NotImplementedException();
		}

		public void ApplyModification(Hero h)
		{
			throw new NotImplementedException();
		}

		public void RemoveModification(Hero h)
		{
			throw new NotImplementedException();
		}
	}
}
