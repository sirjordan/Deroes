namespace Deroes.Core
{
	public class AttackDamage
	{
		public Damage Physical { get; private set; }
		public Damage Cold { get; private set; }
		public Damage Fire { get; private set; }
		public Damage Poison { get; private set; }
		public Damage Lightining { get; private set; }

		public Damage[] All => [Physical, Cold, Fire, Poison, Lightining];
		public int Min => All.Sum(d => d.Min);
		public int Max => All.Sum(d => d.Max);

		public AttackDamage()
		{
			Physical = new Physical(1, 2);
			Cold = new Cold(0, 0);
			Fire = new Fire(0, 0);
			Poison = new Poison(0, 0);
			Lightining = new Lightining(0, 0);
		}

		public int Apply(Unit defender)
		{
			var hitPoints = 0;
			foreach (var dmg in All)
			{
				hitPoints += dmg.Apply(defender);
			}

			return hitPoints;
		}
	}

	public class AttackResistanse
	{
		public Resistanse Physical { get; private set; }
		public Resistanse Cold { get; private set; }
		public Resistanse Fire { get; private set; }
		public Resistanse Poison { get; private set; }
		public Resistanse Lightining { get; private set; }

		public AttackResistanse()
		{
			Physical = new Resistanse();
			Cold = new Resistanse();
			Fire = new Resistanse();
			Poison = new Resistanse();
			Lightining = new Resistanse();
		}
	}

	/// <summary>
	/// Phisycal, Cold, Fire, Lightining, Poison?, Magic
	/// </summary>
	public abstract class Damage
	{
		public int Min { get; protected set; }
		public int Max { get; protected set; }

		public Damage(int min, int max)
		{
			ArgumentOutOfRangeException.ThrowIfNegative(min);

			Min = min;
			Max = max;
		}

		/// <summary>
		/// Do damage, apply effect and returns the hitpoint
		/// </summary>
		/// <returns>Damage dealt calculated</returns>
		public abstract int Apply(Unit defender);

		protected int GetYieldedDamage()
		{
			var rnd = new Random();
			var dmg = rnd.Next(Min, Max + 1);

			return dmg;
		}
	}

	/// <summary>
	/// Phisycal, Cold, Fire, Lightining, Poison?, Magic
	/// </summary>
	public class Resistanse
	{
		public bool Immune { get; private set; }
		public int Amount { get; private set; }

		public Resistanse()
		{
			Amount = 0;
			Immune = false;
		}
	}

	/// <summary>
	/// Freze/chill and do damage
	/// </summary>
	public class Cold(int min, int max) : Damage(min, max)
	{
		public override int Apply(Unit defender)
		{
			if (defender.Resistanses.Cold.Immune)
			{
				return 0;
			}

			var dmg = GetYieldedDamage();
			var hitPoints = (int)Math.Round((defender.Resistanses.Cold.Amount / 100.0) * dmg);

			// TODO: Freeze/chill

			defender.Life.OnAction(-hitPoints);

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
			if (defender.Resistanses.Physical.Immune)
			{
				return 0;
			}

			// TODO: If immune to physical - skip
			// TODO: Take AttackRating in mind

			var dmg = GetYieldedDamage();
			var hitPoints = (dmg - defender.Resistanses.Physical.Amount);

			if (hitPoints < 1)
			{
				hitPoints = 1;
			}

			// TODO: if running - stop

			defender.Life.OnAction(-hitPoints);

			return hitPoints;
		}
	}

	public class Fire(int min, int max) : Damage(min, max)
	{
		public override int Apply(Unit defender)
		{
			throw new NotImplementedException();
		}
	}

	public class Lightining(int min, int max) : Damage(min, max)
	{
		public override int Apply(Unit defender)
		{
			throw new NotImplementedException();
		}
	}

	public class Poison(int min, int max) : Damage(min, max)
	{
		public override int Apply(Unit defender)
		{
			throw new NotImplementedException();
		}
	}
}
