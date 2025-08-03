using Deroes.Core.Stats.Modifiers;
using Deroes.Core.Units;

namespace Deroes.Core
{
	public interface IAura
	{
		void Activate(Hero h);
		void Deactivate(Hero h);
	}

	/// <summary>
	/// Increase damage dealt
	/// </summary>
	public class Might : IAura
	{
		private readonly IStatModifier dmgBonus;

		public Might(int level)
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

		public void Activate(Hero h) => dmgBonus.ApplyModification(h);
		public void Deactivate(Hero h) => dmgBonus.RemoveModification(h);
	}

	/// <summary>
	/// Adds bonus to Fire resist and Max fire resist
	/// </summary>
	public class ResistFire : IAura
	{
		private readonly IStatModifier fireResist;

		public ResistFire(int level)
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

		public void Activate(Hero h)
		{
			fireResist.ApplyModification(h);
			// TODO: Add MaxFireResHere
		}

		public void Deactivate(Hero h)
		{
			fireResist.RemoveModification(h);
		}
	}
}
