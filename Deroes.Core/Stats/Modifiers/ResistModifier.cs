namespace Deroes.Core.Stats.Modifiers
{
	public class ColdResistModifier : IStatModifier<Resistanse>, IStatModifier
	{
		public int Order => throw new NotImplementedException();

		public string Description => throw new NotImplementedException();

		public void ApplyModification(Hero h)
		{
			//h.Resistanses.Cold
		}

		public int GetModificator(Resistanse @base)
		{
			throw new NotImplementedException();
		}

		public void RemoveModification(Hero h)
		{
			throw new NotImplementedException();
		}
	}
}
