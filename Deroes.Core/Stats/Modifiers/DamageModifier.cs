namespace Deroes.Core.Stats.Modifiers
{
	public class DamageModifier : IStatModifier<DamageRange>
	{
		private DamageRange _bonus;

		public string Description => $"{_bonus.Min} - {_bonus.Max} {_bonus.GetType().Name} damage";

		public int Order => throw new NotImplementedException();

		public DamageModifier(DamageRange bonus)
		{
			_bonus = bonus;
		}

		public int GetModificator(DamageRange @base)
		{
			throw new NotImplementedException();
		}
	}
}
