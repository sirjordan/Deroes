using Deroes.Core.Stats;
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
	/// Adds Elemental Damage and Attack Rating bonus to your attack
	/// </summary>
	public class Fanaticism : IAura
	{
		public void Activate(Hero h)
		{
			throw new NotImplementedException();
		}

		public void Deactivate(Hero h)
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>
	/// Adds bonus to Fire resist and Max fire resist
	/// </summary>
	public class ResistFire : IAura
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

		public void Activate(Hero h)
		{
			h.Resistanse.Fire.AddModifier(fireResist); 
			// TODO: Add MaxFireResHere
		}

		public void Deactivate(Hero h) => h.Resistanse.Fire.RemoveModifier(fireResist);
	}
}
