namespace Deroes.Core
{
	public abstract class Hero : Unit
	{
		public Stat Mana { get; protected set; }
		public Stat Stamina { get; protected set; }
		public long Experience { get; private set; }	
		public int Strength { get; private set; }
		public int Dexterity { get; private set; }
		public int Vitality { get; private set; }
		public int Energy { get; private set; }

		protected Hero()
		{
			Experience = 0;
		}

		public void DrinkPotion(Potion p)
		{ 
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

		private void LevelUp()
		{
			Level++;

			Life.OnLevelUp();
			Mana.OnLevelUp();
			Stamina.OnLevelUp();
		}
	}
}
