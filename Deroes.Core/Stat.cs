namespace Deroes.Core
{
	public class Stat
	{
		public double Remaining { get; private set; }
		public double Base { get; private init; }
		/// <summary>
		/// Value received per level
		/// </summary>
		public double LevelCoef { get; private init; }
		/// <summary>
		/// Value received per arrtibute
		/// </summary>
		public double AttributeCoef { get; private init; }
		public double Max { get; private set; }

		public Stat(double @base, double levelCoef, double attrCoef)
		{
			Base = @base;
			LevelCoef = levelCoef;
			AttributeCoef = attrCoef;

			Max = Base;
			Remaining = Base;
		}

		public void OnAddAttribute()
		{
			Max += AttributeCoef;
		}

		public void OnLevelUp()
		{
			Max += LevelCoef;
			Remaining = Max;
		}

		/// <summary>
		/// Add or remove action points.
		/// E.g On dmg: -50 pts, on potion drink: +50 pts, on spell cast: -50 pts
		/// </summary>
		/// <param name="actionPoints">Points to add/remove</param>
		public void OnAction(double actionPoints)
		{
			Remaining = Math.Clamp(Remaining + actionPoints, 0, Max);
		}
	}
}
