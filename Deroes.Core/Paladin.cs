namespace Deroes.Core
{
	public class Paladin : Hero
	{
		public Paladin()
		{
			Name = "Paladin";
			Damage = 10;
			Defense = 5;

			Mana = new(@base: 15, perLevel: 1.5, perAttribute: 2);
			Stamina = new(@base: 89, perLevel: 1, perAttribute: 1);
			Life = new(@base: 55, perLevel: 2, perAttribute: 2);
		}
	}

	public abstract class Potion()
	{

	}

	public class HealthPotion : Potion
	{

	}

	public class StaminaPotion : Potion
	{

	}

	public class AntidotePotion : Potion
	{

	}
}
