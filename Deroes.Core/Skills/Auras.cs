using Deroes.Core.Stats.Modifiers;
using Deroes.Core.Units;

namespace Deroes.Core.Skills
{
	/// <summary>
	/// Aura, Increase damage dealt
	/// </summary>
	public class Might(Unit u) : Skill(u, 1)
	{
		private IStatModifier dmgBonus;

		/// <summary>
		/// Percentage of Dmg
		/// </summary>
		public static int CalculateBonusDmg(int level)
		{
			return level * 10 + 30;
		}

		public override void SetupLevel(int level)
		{
			int percentage = CalculateBonusDmg(level);

			dmgBonus = new PhysicalDamageModifier(
				new PercentageDamageModifier(percentage),
				new PercentageDamageModifier(percentage));

			base.SetupLevel(level); 
		}

		public override void Set() => dmgBonus.ApplyModification(Unit);
		public override void Use(object target) { return; }
		public override void Apply(Unit target) { return; }
		public override void Unset() => dmgBonus.RemoveModification(Unit);
	}

	/// <summary>
	/// Aura, Adds bonus to Fire resist and Max fire resist
	/// </summary>
	public class ResistFire(Unit u) : Skill(u, 1)
	{
		private IStatModifier fireResist;

		public static int CalculateBonusResist(int level)
		{
			ArgumentOutOfRangeException.ThrowIfNegative(level);

			if (level == 0)
				return 0;

			// Exponential saturation
			double _L = 150;   // Asymptotic max resistance (%)
			double _A = 97;    // Initial gap to fill (L - starting value)
			double _k = 0.08;  // Growth rate

			double resistance = _L - _A * Math.Exp(-_k * level);

			return (int)Math.Round(resistance);
		}

		public static int CalculateBonusMaxResist(int level)
		{
			ArgumentOutOfRangeException.ThrowIfNegative(level);

			if (level == 0)
				return 0;

			return Math.Clamp(level, 0, 20);
		}

		public override void SetupLevel(int level)
		{
			fireResist = new FireResistModifier(CalculateBonusResist(level));
			base.SetupLevel(level);
		}

		public override void Set() => fireResist.ApplyModification(Unit);
		public override void Use(object target) { return; }
		public override void Apply(Unit target) { return; }
		public override void Unset() => fireResist.RemoveModification(Unit);
	}
}
