using Deroes.Core.Units;

namespace Deroes.Core.Skills
{
	public abstract class Skill
	{
		public const int MAX_LEVEL = 60;
		public const int MAX_TIER = 6;

		/// <summary>
		/// Level of the Skill
		/// </summary>
		public int Level { get; private set; }
		/// <summary>
		/// Tier of the skill
		/// 1 - 6 Tiers
		/// </summary>
		public int Tier { get; private set; }
		/// <summary>
		/// Required level of the hero to set and use the skill
		/// 1 - 30 level is required
		/// </summary>
		public int RequiredLevel => Tier * 6 - 6;
		protected Unit Unit { get; private set; }

		/// <summary>
		/// Create an instance of a skill
		/// </summary>
		/// <param name="u">Unit reference</param>
		/// <param name="level">Level of the skill (1-60)</param>
		/// <param name="tier">Tier of the skill (1-6)</param>
		/// <exception cref="ArgumentOutOfRangeException">If arguments doenst match min/max alowed</exception>
		protected Skill(Unit u, int level = 1, int tier = 1)
		{
			if (level < 1 || level > MAX_LEVEL) throw new ArgumentOutOfRangeException($"Level must be 1-{MAX_LEVEL}");
			if (tier < 1 || tier > MAX_TIER) throw new ArgumentOutOfRangeException($"Tier must be 1-{MAX_TIER}");

			Level = level;
			Unit = u;
			Tier = tier;
		}

		/// <summary>
		/// Sets the modifications of the skill to the unit
		/// </summary>
		public abstract void Set();

		/// <summary>
		/// You have enough mana, arrows, or durability 
		/// </summary>
		/// <returns></returns>
		public abstract bool CanUse();

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

		/// <summary>
		/// Unsets the modifications of the skill from the unit
		/// </summary>
		public abstract void Unset();
	}
}
