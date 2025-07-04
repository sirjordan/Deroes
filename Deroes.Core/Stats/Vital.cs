namespace Deroes.Core.Stats
{
	public class Vital : IStatModifiable<Vital>
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

		public Vital(double @base, double levelCoef, double attrCoef)
		{
			Base = @base;
			LevelCoef = levelCoef;
			AttributeCoef = attrCoef;

			Max = Base;
			Remaining = Base;
		}

		public Vital(Vital other)
		{
			Base = other.Base;
			LevelCoef = other.LevelCoef;
			AttributeCoef = other.AttributeCoef;
			Max = other.Max;
			Remaining = other.Remaining;
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

		public Vital Modify(int modificator)
		{
			var mod = new Vital(this);
			mod.Max += modificator;
			mod.Remaining += modificator;

			return mod;
		}
	}
}
