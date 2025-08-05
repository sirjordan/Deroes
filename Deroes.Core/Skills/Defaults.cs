using Deroes.Core.Units;

namespace Deroes.Core.Skills
{
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

	// TODO:
	// Range/shoot
	// Throw
}
