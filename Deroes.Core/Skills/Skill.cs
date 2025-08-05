using Deroes.Core.Units;

namespace Deroes.Core.Skills
{
	public abstract class Skill
	{
		public const int MAX_LEVEL = 60;
		public int Level { get; private set; }
		protected Unit Unit { get; private set; }

		protected Skill(Unit u, int level)
		{
			ArgumentOutOfRangeException.ThrowIfLessThan(level, 1);
			ArgumentOutOfRangeException.ThrowIfGreaterThan(level, MAX_LEVEL);

			Level = level;
			Unit = u;
		}

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

		public abstract void Unset();
	}
}
