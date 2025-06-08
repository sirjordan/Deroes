namespace Deroes.Core
{
	public abstract class Hero : Unit
	{
		public long Experience { get; private set; }
		public int Mana { get; protected set; }
		public abstract int LifeInitial { get; }
		public abstract double LifePerLevel { get; }
		public abstract int ManaInitial { get; }
		public abstract double ManaPerLevel { get; }

		protected Hero()
		{
			Experience = 0;
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
			Life = LifeInitial + (int)Math.Ceiling(Level * LifePerLevel);
			Mana = ManaInitial + (int)Math.Ceiling(Level * ManaPerLevel);
		}
	}
}
