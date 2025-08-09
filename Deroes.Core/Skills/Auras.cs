using Deroes.Core.Stats.Modifiers;
using Deroes.Core.Units;

namespace Deroes.Core.Skills
{
	/// <summary>
	/// Aura, Increase damage dealt
	/// </summary>
	public class Might : Skill
	{
		private readonly IStatModifier dmgBonus;

		public Might(Unit u, int level = 1) : base(u, level)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(level);

			int percentage = CalculateBonusDmg(level);

			dmgBonus = new PhysicalDamageModifier(
				new PercentageDamageModifier(percentage),
				new PercentageDamageModifier(percentage));
		}

		/// <summary>
		/// Percentage of Dmg
		/// </summary>
		public static int CalculateBonusDmg(int level)
		{
			return level * 10 + 30;
		}

		public override void Set() => dmgBonus.ApplyModification(Unit);
		public override void Use(object target) { return; }
		public override void Apply(Unit target) { return; }
		public override bool CanUse() => true;
		public override void Unset() => dmgBonus.RemoveModification(Unit);

	}

	/// <summary>
	/// Aura, Adds bonus to Fire resist and Max fire resist
	/// </summary>
	public class ResistFire : Skill
	{
		private readonly IStatModifier fireResist;

		public ResistFire(Unit u, int level = 1) : base(u, level)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(level);

			fireResist = new FireResistModifier(CalculateBonusResist(level));
		}

		public static int CalculateBonusResist(int level)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(level);

			// Exponential saturation
			double _L = 150;   // Asymptotic max resistance (%)
			double _A = 97;    // Initial gap to fill (L - starting value)
			double _k = 0.08;  // Growth rate

			double resistance = _L - _A * Math.Exp(-_k * level);

			return (int)Math.Round(resistance);
		}

		public static int CalculateBonusMaxResist(int level)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(level);

			return Math.Clamp(level, 0, 20);
		}

		public override void Set() => fireResist.ApplyModification(Unit);
		public override void Use(object target) { return; }
		public override void Apply(Unit target) { return; }
		public override bool CanUse() => true;
		public override void Unset() => fireResist.RemoveModification(Unit);
	}
}
