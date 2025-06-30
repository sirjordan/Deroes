namespace Deroes.Core
{
	public class Hero : Unit
	{
		// Stats
		public Stat Mana { get; private init; }
		public Stat Stamina { get; private init; }
		public long Experience { get; private set; }

		// Attributes
		public int Strength { get; private set; }
		public int Dexterity { get; private set; }
		public int Vitality { get; private set; }
		public int Energy { get; private set; }

		// Gear
		public Helm? Helm { get; private set; }
		public Armor? Armor { get; private set; }
		public Belt? Belt { get; private set; }
		public Weapon? LeftHand { get; private set; }
		public Shield? RightHand { get; private set; }
		public Gloves? Gloves { get; private set; }
		public Boots? Boots { get; private set; }
		public Ring? LeftRing { get; private set; }
		public Ring? RightRing { get; private set; }
		public Amulet? Amulet { get; private set; }

		// Inventory
		public Stash Inventory { get; private set; }
		public int Gold { get; private set; }

		private Hero()
		{
			Experience = 0;
			Inventory = new Stash(10, 4);
			Gold = 0;
		}

		public static Hero CreatePaladin()
		{
			return new Hero
			{
				Name = "Paladin",

				Mana = new(@base: 15, levelCoef: 1.5, attrCoef: 2),
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
			Mana.OnAddAttribute();
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
			Mana.OnLevelUp();
			Stamina.OnLevelUp();
		}
	}
}
