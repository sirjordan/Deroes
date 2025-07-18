using Deroes.Core.Stats;
using Deroes.Core.Stats.Modifiers;
using Deroes.Core.Units;

namespace Deroes.Core
{
	public abstract class Skill
	{
	}

	/// <summary>
	/// Throws a Javelin, Knives etc
	/// </summary>
	public class Throw : Skill { }

	/// <summary>
	/// Shoots with Bow or Crossbow 
	/// </summary>
	public class Shoot : Skill { }

	//public abstract class Spell
	//{
	//	private readonly Hero _hero;

	//	public int RequiredCharLevel { get; private set; }
	//	public int Level { get; private set; }
	//	public double ManaCost { get; private set; }    // Change per level

	//	public Spell(Hero h)
	//	{
	//		_hero = h;
	//	}

	//	/// <summary>
	//	/// Execute the skill
	//	/// </summary>
	//	/// <param name="target">Target to apply the cast to.
	//	/// Could be location, Monster, Self</param>
	//	/// <returns>If cast is successfull or not</returns>
	//	public bool Cast(object target)
	//	{
	//		// TODO: This could be moved in the caster
	//		if (_hero.Mana.Value.Remaining >= ManaCost)
	//		{
	//			_hero.Mana.Value.OnAction(-ManaCost);
	//			ApplyEffect(target);

	//			return true; 
	//		}
	//		else
	//		{
	//			// I need mana
	//			return false;
	//		}
	//	}

	//	protected abstract void ApplyEffect(object target);
	//}

	//// [-v-Spells-v-]

	//// Individual attack
	//public class FireBolt : Spell
	//{
	//	public Fire Damage { get; private set; } // Change per level
	//}

	//// Modifier/Enchants
	//public class Vengeance : Spell
	//{
	//	public DamageRange[] Damage { get; private set; } // Change per level
	//}


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

	//// Curse
	//public class AmplifyDamage : Spell { }
}
