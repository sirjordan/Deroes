namespace Deroes.Core
{
	public class Paladin : Hero
	{
		public Paladin()
		{
			Name = "Paladin";
			Damage = 10;
			Defense = 5;

			Mana = new(@base: 15, levelCoef: 1.5, attrCoef: 2);
			Stamina = new(@base: 89, levelCoef: 1, attrCoef: 1);
			Life = new(@base: 55, levelCoef: 2, attrCoef: 3);
		}
	}
}
