using Deroes.Core.Items;
using Deroes.Core.Stats;

namespace Deroes.Core.Units
{
	public class Hero : Unit
	{
		private ILevelUpSubscriber[] LevelUpSubscribers => [Attributes, Life.BaseValue, Mana.BaseValue, Stamina.BaseValue, Skills.Specials];

		public override int Max_Level => 99;
		public Stat<Vital> Stamina { get; private set; }
		public Attributes Attributes { get; private set; }
		public Stash<Item> Inventory { get; private set; }
		public Chest Chest { get; private set; }
		public Gold Gold { get; private set; }
		public Gear Gear { get; private set; }
		public long Experience { get; private set; }

		public Hero(string name, IHeroSetupFactory heroSetup)
			: base(name, heroSetup)
		{
			Stamina = heroSetup.Stamina();
			Attributes = heroSetup.Attributes(this);
			Experience = 0;
			Inventory = new Stash<Item>(10, 4);
			Chest = new Chest(this);
			Gold = new Gold(this);
			Gear = new Gear(this);
		}

		public static long XpToLevelUp(int fromLevel)
		{
			double xp;
			if (fromLevel <= 20)
			{
				// Lower levels: Gentle curve
				xp = 45.0 * Math.Pow(fromLevel, 2.8) + 400.0;
			}
			else
			{
				// Higher levels: Steeper progression
				xp = 0.00021 * Math.Pow(fromLevel, 6.7) + 50000.0;
			}

			return (long)Math.Round(xp);
		}

		public void DrinkPotion(IPotion p)
		{
			p.Drink(this);
		}

		public void DrinkPotion(int beltRow, int beltCol)
		{
			var potion = Gear.Belt.Drop(beltRow, beltCol);
			if (potion != null)
			{
				DrinkPotion(potion);
			}
		}

		public void AddExperience(long xp)
		{
			Experience += xp;

			while (Experience >= XpToLevelUp(Level) && Level < Max_Level)
			{
				LevelUp();
			}
		}

		public override void Die()
		{
			// TODO: Drop gold (max 20%)
			base.Die();
		}

		private void LevelUp()
		{
			Level++;

			foreach (var sub in LevelUpSubscribers)
			{
				sub.OnLevelUp();
			}
		}
	}
}
