using Deroes.Core.Units;

namespace Deroes.Core.Skills
{
	public class DefaultSkillSet
	{
		public Skill Normal { get; private set; }
		public Skill Throw { get; private set; }
		public Skill Shoot { get; private set; }

		public DefaultSkillSet(Unit u)
		{
			Normal = new NormalAttack(u);
		}
	}

	public class NormalAttack(Unit u) : Skill(u, 1)
	{
		public override void Use(object target)
		{
			Console.WriteLine($"{Unit.Name} did normal attack");
		}

		public override void Apply(Unit target)
		{
			var hitpoins = Unit.Melee.Apply(target);
			Console.WriteLine($"{Unit.Name} did {hitpoins} of damage to {target.Name}");
		}

		public override bool CanUse() => true;
		public override void Set() { return; }
		public override void Unset() { return; }
	}
}
