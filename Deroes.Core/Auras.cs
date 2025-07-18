using Deroes.Core.Stats;
using Deroes.Core.Stats.Modifiers;
using Deroes.Core.Units;

namespace Deroes.Core
{
	public interface IAura // ??
	{
		void Activate();
		void Deactivate();
	}

	/// <summary>
	/// Adds Damage and Attack Rating bonus to your attack
	/// </summary>
	public class Fanaticism : IStatModifier
	{
		public void ApplyModification(Hero h)
		{
			throw new NotImplementedException();
		}

		public void RemoveModification(Hero h)
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>
	/// Adds bonus to Fire resist and Max fire resist
	/// </summary>
	public class ResistFire : IStatModifier
	{
		private readonly IStatModifier<Resistanse> fireResist;

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

		public void ApplyModification(Hero h) => h.Resistanse.Fire.AddModifier(fireResist);
		public void RemoveModification(Hero h) => h.Resistanse.Fire.RemoveModifier(fireResist);
	}
}
