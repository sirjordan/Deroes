namespace Deroes.Core
{
	public class Paladin : Hero
	{
		public override int LifeInitial => 55;
		public override int ManaInitial => 15;
		public override double LifePerLevel => 2;
		public override double ManaPerLevel => 1.5;
		
		public Paladin()
		{
			Name = "Paladin";
			Life = LifeInitial;
			Mana = ManaInitial;
			Damage = 10;
			Defense = 5;
		}
	}
}
