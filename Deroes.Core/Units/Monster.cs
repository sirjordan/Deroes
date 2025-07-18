﻿using Deroes.Core.Items;
using Deroes.Core.Stats;

namespace Deroes.Core.Units
{
	public class Monster : Unit
	{
		// TODO:
		// 1. Chance to drop (37.5% chance))
		// 2. Fine tune (and test) monster health per level
		// 3. Ensure monsetr is dead before drop items/gold
		// 4. Dmg & Resists per monster level?
		// 12. Monster xp calculation

		public override int Max_Level => 85;

		private Random _random = new Random();

		/// <summary>
		/// hp = base + (level * 3 vitalityPerLevel * 2 coef)
		/// </summary>
		/// <param name="level">1 - 85</param>
		public Monster(int level, string name = "Monster") 
			: base(name, GetLifePerMonsterLevel(level))
		{
			ArgumentOutOfRangeException.ThrowIfGreaterThan(level, Max_Level);
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(level);

			Level = level;
		}

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

		public override void Die()
		{
			var drop = DropItems();
			var gold = DropGold();

			// TODO: Drop to the ground event

			base.Die();
		}

		/// <summary>
		/// hp = base + (level * 3 vitalityPerLevel * 2 coef)
		/// </summary>
		private static Stat<Vital> GetLifePerMonsterLevel(int forLevel)
		{
			return new Stat<Vital>(new Vital(@base: (5 + forLevel * 3 * 2), 0, 0));
		}
	}
}
