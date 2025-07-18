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

	//// Curse
	//public class AmplifyDamage : Spell { }
}
