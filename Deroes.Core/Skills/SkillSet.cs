using Deroes.Core.Units;

namespace Deroes.Core.Skills
{
	public class SkillSet
	{
		private readonly Unit _unit;
		private Skill _primary; 
		private Skill _secondary;

		/// <summary>
		/// Normal, throw, shoot attacks
		/// </summary>
		public DefaultSkillSet Defaults { get; private set; }

		/// <summary>
		/// Unique per hero class
		/// </summary>
		public SkillTree? Specials { get; private set; }

		/// <summary>
		/// Skill selected as Primary from available
		/// </summary>
		public Skill Primary => _primary;

		/// <summary>
		/// Skill selected as Secondary from available
		/// </summary>
		public Skill Secondary => _secondary;

		/// <summary>
		/// Added additionaly by Items
		/// </summary>
		public ICollection<Skill> Additional { get; private set; }

		public SkillSet(Unit u, SkillTree? specials)
		{
			_unit = u;
			Defaults = new DefaultSkillSet(u);
			Specials = specials;
			Additional = [];
			_primary = Defaults.NormalAttack;
			_secondary = Defaults.NormalAttack;
		}

		public void SetPrimary(Func<Unit, Skill>? skillToSet)
		{
			Set(ref _primary, skillToSet);
		}

		public void SetSecondary(Func<Unit, Skill>? skillToSet)
		{
			Set(ref _secondary, skillToSet);
		}

		private void Set(ref Skill target, Func<Unit, Skill>? skillToSet)
		{
			target.Unset();
			target = Defaults.NormalAttack;

			if (skillToSet != null)
			{
				target = skillToSet.Invoke(_unit);
			}

			target.Set();
		}
	}
}
