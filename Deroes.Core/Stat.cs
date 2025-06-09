namespace Deroes.Core
{
	public class Stat
	{
		public double Remaining { get; private set; }
		public double Base { get; init; }
		public double PerLevel { get; init; }
		public double PerAttribute { get; init; }
		public double Max { get; private set; }

		public Stat(double @base, double perLevel, double perAttribute)
		{
			Base = @base;
			PerLevel = perLevel;
			PerAttribute = perAttribute;

			Max = Base;
			Remaining = Base;
		}

		public void OnAddAttribute()
		{
			Max += PerAttribute;
		}

		public void OnLevelUp()
		{
			Max += PerLevel;
			Remaining = Max;
		}

		/// <summary>
		/// Add or remove action points.
		/// E.g On dmg: -50 pts, on potion drink: +50 pts, on spell cast: -50 pts
		/// </summary>
		/// <param name="actionPoints">Points to add/remove</param>
		public void OnAction(double actionPoints)
		{
			Remaining += actionPoints;
		}
	}
}
