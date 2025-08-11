using Deroes.Core.Units;

namespace Deroes.Core.Skills
{
	public class SkillSet
	{
		private readonly Unit _unit;

		/// <summary>
		/// Normal, throw, shoot attacks
		/// </summary>
		public DefaultSkillSet Defaults { get; private set; }
		/// <summary>
		/// Unique per hero class
		/// </summary>
		public SkillTree Specials { get; private set; }
		/// <summary>
		/// Skill selected as Primary from available
		/// </summary>
		public Skill Primary { get; private set; }
		/// <summary>
		/// Skill selected as Secondary from available
		/// </summary>
		public Skill Secondary { get; private set; }
		/// <summary>
		/// Added additionaly by Items
		/// </summary>
		public ICollection<Skill> Additional { get; private set; }

		public SkillSet(Unit u)
		{
			_unit = u;
			Defaults = new DefaultSkillSet(u);
			Specials = new SkillTree();
			Additional = [];
			Primary = Defaults.NormalAttack;
			Secondary = Defaults.NormalAttack;
		}

		public void SetPrimary(Func<Unit, Skill>? skillToSet)
		{
			Primary.Unset();
			Primary = Defaults.NormalAttack;

			if (skillToSet != null)
			{
				Primary = skillToSet.Invoke(_unit);
			}

			Primary.Set();
		}

		public void SetSecondary(Func<Unit, Skill>? skillToSet)
		{
			Secondary.Unset();
			Secondary = Defaults.NormalAttack;

			if (skillToSet != null)
			{
				Secondary = skillToSet.Invoke(_unit);
			}

			Secondary.Set();
		}
	}
}
