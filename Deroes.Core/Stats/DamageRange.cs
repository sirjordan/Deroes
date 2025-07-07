namespace Deroes.Core.Stats
{
	public class AttackDamage
	{
		public DamageRange Physical { get; private set; }
		public DamageRange Cold { get; private set; }
		public DamageRange Fire { get; private set; }
		public DamageRange Poison { get; private set; }
		public DamageRange Lightining { get; private set; }

		public DamageRange[] All => [Physical, Cold, Fire, Poison, Lightining];
		public int Min => All.Sum(d => d.Min.Value.Amount);
		public int Max => All.Sum(d => d.Max.Value.Amount);

		public AttackDamage()
			: this(new Physical(), new Cold(), new Fire(), new Poison(), new Lightining()) { }

		public AttackDamage(Physical ph, Cold c, Fire f, Poison p, Lightining l)
		{
			Physical = ph;
			Cold = c;
			Fire = f;
			Poison = p;
			Lightining = l;
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

	public class Damage : IStatModifiable<Damage>
	{
		public int Amount { get; private set; }

		public Damage(int value)
		{
			ArgumentOutOfRangeException.ThrowIfNegative(value);

			Amount = value;
		}

		public Damage Modify(int modificator)
		{
			return new Damage(Amount + modificator);
		}
	}

	/// <summary>
	/// Phisycal, Cold, Fire, Lightining, Poison?, Magic
	/// </summary>
	public abstract class DamageRange
	{
		// TODO: Unit test this!

		public Stat<Damage> Min { get; protected set; }
		public Stat<Damage> Max { get; protected set; }

		public DamageRange(int min, int max)
		{
			ArgumentOutOfRangeException.ThrowIfNegative(min);
			ArgumentOutOfRangeException.ThrowIfNegative(max);

			Min = new Stat<Damage>(new Damage(min));
			Max = new Stat<Damage>(new Damage(max));
		}

		/// <summary>
		/// Do damage, apply effect and returns the hitpoint
		/// </summary>
		/// <returns>Damage dealt calculated</returns>
		public abstract int Apply(Unit defender);

		protected int GetYieldedDamage()
		{
			var rnd = new Random();
			var dmg = rnd.Next(Min.Value.Amount, Max.Value.Amount + 1);

			return dmg;
		}
	}

	/// <summary>
	/// Freze/chill and do damage
	/// </summary>
	public class Cold : DamageRange
	{
		public Cold(int min, int max) : base(min, max) { }

		public Cold() : this(0, 0) { }

		public override int Apply(Unit defender)
		{
			if (defender.Resistanses.Cold.Immune)
			{
				return 0;
			}

			var dmg = GetYieldedDamage();
			var hitPoints = (int)Math.Round(defender.Resistanses.Cold.Amount / 100.0 * dmg);

			// TODO: Freeze/chill

			defender.Life.OnAction(-hitPoints);

			return hitPoints;
		}
	}

	/// <summary>
	/// Do damage and if running - stop
	/// </summary>
	public class Physical : DamageRange
	{
		public Physical(int min, int max) : base(min, max) { }

		public Physical() : this(1, 2) { }

		public override int Apply(Unit defender)
		{
			if (defender.Resistanses.Physical.Immune)
			{
				return 0;
			}

			// TODO: If immune to physical - skip
			// TODO: Take AttackRating in mind

			var dmg = GetYieldedDamage();
			var hitPoints = dmg - defender.Resistanses.Physical.Amount;

			if (hitPoints < 1)
			{
				hitPoints = 1;
			}

			// TODO: if running - stop

			defender.Life.OnAction(-hitPoints);

			return hitPoints;
		}
	}

	public class Fire : DamageRange
	{
		public Fire(int min, int max) : base(min, max) { }

		public Fire() : this(0, 0) { }

		public override int Apply(Unit defender)
		{
			return 0;
		}
	}

	public class Lightining : DamageRange
	{
		public Lightining(int min, int max) : base(min, max) { }

		public Lightining() : this(0, 0) { }

		public override int Apply(Unit defender)
		{
			return 0;
		}
	}

	public class Poison : DamageRange
	{
		public Poison(int min, int max) : base(min, max) { }

		public Poison() : this(0, 0) { }

		public override int Apply(Unit defender)
		{
			return 0;
		}
	}
}
