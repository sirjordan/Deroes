using Deroes.Core.Stats;

namespace Deroes.Core.Units
{
	public class Paladin : Hero
	{
		private static Stat<Vital> LifeInitial => new(new(@base: 55, levelCoef: 2, attrCoef: 3));
		private static Stat<Vital> ManaInitial => new(new(@base: 15, levelCoef: 1.5, attrCoef: 2));
		private static Stat<Vital> StaminaInitial => new(new(@base: 89, levelCoef: 1, attrCoef: 1));

		public Paladin(string name = "Paladin")
			: base(name, LifeInitial, ManaInitial, StaminaInitial)
		{
			Attributes = new Attributes(this, str: 25, dex: 20, vitality: 25, energy: 15);
		}

		public void ActivateAura()
		{ } // ??
	}
}
