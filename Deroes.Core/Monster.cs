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
			Life = 10;
			Damage = 6;
			Defense = 0;
		}
	}
}
