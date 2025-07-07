using Deroes.Core.Items;
using Deroes.Core.Stats;

namespace Deroes.Core
{
	public class Hero : Unit
	{
		public Stat<Vital> Mana { get; private init; }
		public Vital Stamina { get; private init; }
		public long Experience { get; private set; }

		// Attributes
		public int Strength { get; private set; }
		public int Dexterity { get; private set; }
		public int Vitality { get; private set; }
		public int Energy { get; private set; }

		// Inventory
		public Stash Stash { get; private set; }
		public int Gold { get; private set; }
		public Gear Gear { get; private set; }

		protected Hero()
		{
			Experience = 0;
			Stash = new Stash(10, 4);
			Gold = 0;
			Gear = new Gear(this);
		}

		public static Hero CreatePaladin()
		{
			return new Hero
			{
				Name = "Paladin",

				Mana = new Stat<Vital>(new(@base: 15, levelCoef: 1.5, attrCoef: 2)),
				Stamina = new(@base: 89, levelCoef: 1, attrCoef: 1),
				Life = new(@base: 55, levelCoef: 2, attrCoef: 3),

				Strength = 25,
				Dexterity = 20,
				Vitality = 25,
				Energy = 15
			};
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

		public void AddStrenght()
		{
			Strength++;
		}

		public void AddDexterity()
		{
			Dexterity++;
		}

		public void AddVitality()
		{
			Vitality++;
			Life.OnAddAttribute();
			Stamina.OnAddAttribute();
		}

		public void AddEnergy()
		{
			Energy++;
			Mana.BaseValue.OnAddAttribute();
		}

		public void AddExperience(long xp)
		{
			Experience += xp;

			while (Experience >= XpToLevelUp(Level) && Level < MAX_LEVEL)
			{
				LevelUp();
			}
		}

		private void LevelUp()
		{
			Level++;

			Life.OnLevelUp();
			Mana.BaseValue.OnLevelUp();
			Stamina.OnLevelUp();
		}
	}
}
