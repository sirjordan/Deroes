using Deroes.Core.Stats;

namespace Deroes.Core.Units
{
	public class Paladin : Hero
	{
		public Paladin(string name = "Paladin")
		{
			Name = name;
			Mana = new Stat<Vital>(new(@base: 15, levelCoef: 1.5, attrCoef: 2));
			Stamina = new Stat<Vital>(new(@base: 89, levelCoef: 1, attrCoef: 1));
			Life = new Stat<Vital>(new(@base: 55, levelCoef: 2, attrCoef: 3));
			Attributes = new Attributes(this, str: 25, dex: 20, vitality: 25, energy: 15);
		}
	}
}
