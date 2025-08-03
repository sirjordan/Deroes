using Deroes.Core.Stats.Modifiers;
using Deroes.Core.Units;

namespace Deroes.Core
{
	public abstract class Skill
	{
		public const int MAX_LEVEL = 60;
		public int Level { get; private set; }
		protected Unit Unit { get; private set; }

		protected Skill(Unit u, int level)
		{
			ArgumentOutOfRangeException.ThrowIfLessThan(1, level);
			ArgumentOutOfRangeException.ThrowIfGreaterThan(MAX_LEVEL, level);

			Level = level;
			Unit = u; 
		}

		/// <summary>
		/// Use/start the skill
		/// Animation event, Take mana, take arrows, etc
		/// </summary>
		/// <param name="target">Direction, Vector, Units' direction</param>
		public abstract void Use(object target);

		/// <summary>
		/// On colusion/hit the target
		/// Take dmg, apply curse, activate aura, etc
		/// </summary>
		/// <param name="target">Target context</param>
		public abstract void Apply(Unit target);
	}

	public class NormalAttack(Unit u) : Skill(u, 1)
	{
		public override void Use(object target)
		{
			Console.WriteLine($"{Unit.Name} did normal attack");
		}

		public override void Apply(Unit target)
		{
			var hitpoins = Unit.Melee.Apply(target);
			Console.WriteLine($"{Unit.Name} did {hitpoins} of damage to {target.Name}");
		}
	}

	/// <summary>
	/// Adds elemental damage to your attack
	/// </summary>
	public class Vengeance : Skill
	{
		private readonly IStatModifier coldDmg;
		public double ManaCost { get; private set; }

		public Vengeance(Unit u, int level): base(u, level) 
		{
			int dmgBonusPercentage = 20;
			coldDmg = new PhysicalDamageModifier(
				new PercentageDamageModifier(dmgBonusPercentage),
				new PercentageDamageModifier(dmgBonusPercentage));
			ManaCost = 5.0;
		}

		public override void Use(object target)
		{
			if (target == null) return;
			if (Unit.Mana.Value.Remaining < ManaCost) throw new InvalidOperationException("Not enough Mana");

			Unit.Mana.Value.OnAction(-ManaCost);

			Console.WriteLine($"{Unit.Name} did Vengeance attack");
		}

		public override void Apply(Unit target)
		{
			
		}
	}
}
