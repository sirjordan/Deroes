using Deroes.Core.Items;
using Deroes.Core.Stats;

namespace Deroes.Core
{
	public class Monster : Unit
	{
		// TODO:
		// 1. Chance to drop (62.5% chance to NO Drop)
		// 2. Fine tune monster health per level

		public const int MAX_LEVEL = 85;

		private Random _random = new Random();

		public int CalcExperience()
		{
			// Sum of all the stats maybe?
			// Monster Level (mLVL) * MonStats (from MonStats.txt game file) / 100
			return 100;
		}

		public Item[] DropItems()
		{
			return []; 
		}

		public int DropGold()
		{
			// Parameters you can tweak:
			double L = 6000;   // Max gold it flattens towards
			double k = 0.1;    // Growth rate
			double x0 = 60;    // Inflection point (where curve starts flattening)

			double averageGold = L / (1 + Math.Exp(-k * (Level - x0)));

			// Random variation: ±50%
			double variation = _random.NextDouble() * 1.0 + 0.5; // 0.5–1.5

			int gold = (int)(Math.Round(averageGold * variation) / 2);

			return gold;
		}

		/// <param name="level">1 - 85</param>
		public Monster(int level)
		{
			ArgumentOutOfRangeException.ThrowIfGreaterThan(level, MAX_LEVEL);
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(level);

			Level = level;
			Name = "Monster";

			var hp = 50 + (level * 3 * 2); // base + (level * 3 vitalityPerLevel * 2 coef)
			Life = new Stat<Vital>(new Vital(@base: hp, 0, 0));
		}
	}
}
