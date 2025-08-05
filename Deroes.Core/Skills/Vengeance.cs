using Deroes.Core.Stats.Modifiers;
using Deroes.Core.Units;

namespace Deroes.Core.Skills
{
	/// <summary>
	/// Adds elemental damage to your attack
	/// </summary>
	public class Vengeance : Skill
	{
		private readonly IStatModifier coldDmg;
		private readonly IStatModifier fireDmg;
		private readonly IStatModifier ligheningDmg;

		public double ManaCost { get; private set; }

		public Vengeance(Unit u, int level) : base(u, level)
		{
			int dmgBonusPercentage = CalculateBonusDmg(level);

			coldDmg = new ColdDamageModifier(
				new PercentageDamageModifier(dmgBonusPercentage),
				new PercentageDamageModifier(dmgBonusPercentage));
			fireDmg = new FireDamageModifier(
				new PercentageDamageModifier(dmgBonusPercentage),
				new PercentageDamageModifier(dmgBonusPercentage));
			ligheningDmg = new LightiningDamageModifier(
				new PercentageDamageModifier(dmgBonusPercentage),
				new PercentageDamageModifier(dmgBonusPercentage));

			ManaCost = CalculateManaCost(level);
		}

		public override void Set()
		{
			coldDmg.ApplyModification(Unit);
			fireDmg.ApplyModification(Unit);
			ligheningDmg.ApplyModification(Unit);
		}

		public override void Use(object target)
		{
			if (target == null) throw new ArgumentNullException("target");
			if (!CanUse()) throw new InvalidOperationException("Not enough Mana");

			Unit.Mana.Value.OnAction(-ManaCost);

			Console.WriteLine($"{Unit.Name} did Vengeance attack");
		}

		public override void Apply(Unit target)
		{
			Unit.Skills.Attack.Normal.Apply(target);
		}

		public override void Unset()
		{
			coldDmg.RemoveModification(Unit);
			fireDmg.RemoveModification(Unit);
			ligheningDmg.RemoveModification(Unit);
		}

		public override bool CanUse() => Unit.Mana.Value.Remaining > ManaCost;

		/// <summary>
		/// Dmg Percantage
		/// </summary>
		public static int CalculateBonusDmg(int level)
		{
			return 70 + (level - 1) * 6;
		}

		public static double CalculateManaCost(int level)
		{
			return 4.0 + 0.25 * level + 0.05 * (level % 2);
		}
	}
}
