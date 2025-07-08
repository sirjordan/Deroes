namespace Deroes.Core.Stats
{
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
			if (defender.Resistanse.Cold.Value.Immune)
			{
				return 0;
			}

			var dmg = GetYieldedDamage();
			var hitPoints = (int)Math.Round(dmg - (defender.Resistanse.Cold.Value.Amount / 100.0 * dmg));

			// TODO: Freeze/chill

			defender.Life.Value.OnAction(-hitPoints);

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
			if (defender.Resistanse.Physical.Value.Immune)
			{
				return 0;
			}

			// TODO: Take AttackRating in mind

			var dmg = GetYieldedDamage();
			var hitPoints = dmg - defender.Resistanse.Physical.Value.Amount;

			if (hitPoints < 1)
			{
				hitPoints = 1;
			}

			// TODO: if running - stop

			defender.Life.Value.OnAction(-hitPoints);

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
