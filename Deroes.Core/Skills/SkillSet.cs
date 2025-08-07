using Deroes.Core.Units;

namespace Deroes.Core.Skills
{
	public class SkillSet
	{
		// TODO: ILevelUpScubriber +1 skills

		/// <summary>
		/// Normal, throw, shoot attacks
		/// </summary>
		public DefaultSkillSet Attack { get; private set; } // TODO: This myght be private?
		/// <summary>
		/// Per hero class
		/// </summary>
		public List<Skill> SkillTree { get; private set; }
		public Skill Primary { get; private set; }
		public Skill Secondary { get; private set; }

		public SkillSet(Unit u)
		{
			Attack = new DefaultSkillSet(u);
			Primary = Attack.Normal;
			Secondary = Attack.Normal;
		}

		public void SetPrimary(Skill? skill)
		{
			Primary.Unset();
			Primary = skill ?? Attack.Normal;
			Primary.Set();
		}

		public void SetSecondary(Skill? skill)
		{
			Secondary.Unset();
			Secondary = skill ?? Attack.Normal;
			Secondary.Set();
		}
	}
}
