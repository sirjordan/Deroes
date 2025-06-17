namespace Deroes.Core
{
	//public class Damage
	//{
	//	// Combined all damage<t>
	//}

	//public class Resistanse
	//{
	//	// Combined all res<t>
	//}

	/// <summary>
	/// Phisycal, Cold, Fire, Lightining, Poison?, Magic
	/// </summary>
	public abstract class Damage
	{
		public int Min { get; protected set; }
		public int Max { get; protected set; }

		public Damage(int min, int max)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(min);

			Min = min;
			Max = max;
		}

		/// <summary>
		/// Do damage, apply effect and returns the hitpoint
		/// </summary>
		/// <returns>Damage dealt calculated</returns>
		public abstract int Apply(Unit defender);

		protected int GetActualDamage()
		{
			var rnd = new Random();
			var dmg = rnd.Next(Min, Max);

			return dmg;
		}
	}

	/// <summary>
	/// Phisycal, Cold, Fire, Lightining, Poison?, Magic
	/// </summary>
	/// <typeparam name="T">Type of damage</typeparam>
	public class Resistanse<T> where T : Damage
	{
		public bool Immune { get; private set; }
		public int Amount { get; private set; }
	}

	/// <summary>
	/// Freze/chill and do damage
	/// </summary>
	public class Cold(int min, int max) : Damage(min, max)
	{
		public override int Apply(Unit defender)
		{
			if (defender.ColdRes.Immune)
			{
				return 0;
			}

			// 
			var dmg = GetActualDamage();
			var hitPoints = (int)Math.Round((defender.ColdRes.Amount * 0.01) * dmg);

			defender.Life.OnAction(-hitPoints);
			// TODO: Freeze/chill

			return hitPoints;
		}
	}

	/// <summary>
	/// Do damage and if running - stop
	/// </summary>
	public class Physical(int min, int max) : Damage(min, max)
	{
		public override int Apply(Unit defender)
		{
			// TODO: If immune to physical - skip

			var dmg = GetActualDamage();
			var hitPoints = (dmg - defender.Defense);

			if (hitPoints < 1)
			{
				hitPoints = 1;
			}

			defender.Life.OnAction(-hitPoints);
			// TODO: if running - stop

			return hitPoints;
		}
	}
}
