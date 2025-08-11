using Deroes.Core.Units;

namespace Deroes.Core.Skills
{
	public class SkillSet
	{
		/// <summary>
		/// Normal, throw, shoot attacks
		/// </summary>
		public DefaultSkillSet Defaults { get; private set; }
		/// <summary>
		/// Unique per hero class
		/// </summary>
		public SkillTree Specials { get; private set; }
		public Skill Primary { get; private set; }
		public Skill Secondary { get; private set; }

		public SkillSet(Unit u)
		{
			Defaults = new DefaultSkillSet(u);
			Specials = new SkillTree();
			Primary = Defaults.NormalAttack;
			Secondary = Defaults.NormalAttack;
		}

		public void SetPrimary(Skill? skill)
		{
			// TODO: Should be some of the available skills
			Primary.Unset();
			Primary = skill ?? Defaults.NormalAttack;
			Primary.Set();
		}

		public void SetSecondary(Skill? skill)
		{
			Secondary.Unset();
			Secondary = skill ?? Defaults.NormalAttack;
			Secondary.Set();
		}
	}
}
