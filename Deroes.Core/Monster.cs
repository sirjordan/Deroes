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

		public object[] DropItems()
		{
			return []; 
		}

		public Monster()
		{
			Name = "Monster";
			Life = new Stat(@base: 10, 0, 0);
			Damage = 6;
			Defense = 0;
		}
	}
}
