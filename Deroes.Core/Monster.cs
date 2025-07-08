using Deroes.Core.Items;
using Deroes.Core.Stats;

namespace Deroes.Core
{
	public class Monster : Unit
	{
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
			return 0;
		}

		public Monster(double health)
		{
			Name = "Monster";
			Life = new Stat<Vital>(new Vital(@base: health, 0, 0));
		}
	}
}
