using Deroes.Core.Skills;
using Deroes.Core.Stats;

namespace Deroes.Core.Units
{
	public interface IUnitSetupFactory
	{
		Stat<Vital> Mana();
		Stat<Vital> Life();
		SkillSet Skills(Unit u);
	}

	public interface IHeroSetupFactory : IUnitSetupFactory
	{
		Stat<Vital> Stamina();
		Attributes Attributes(Hero h);
	}

	public class PaladinSetup : IHeroSetupFactory
	{
		public Attributes Attributes(Hero h) => new(h, str: 25, dex: 20, vitality: 25, energy: 15);
		public Stat<Vital> Life() => new(new(@base: 55, levelCoef: 2, attrCoef: 3));
		public Stat<Vital> Mana() => new(new(@base: 15, levelCoef: 1.5, attrCoef: 2));
		public Stat<Vital> Stamina() => new(new(@base: 89, levelCoef: 1, attrCoef: 1));
		public SkillSet Skills(Unit h)
		{
			var root = new SkillTree.SkillNode(new Might(h));
			var child1 = new SkillTree.SkillNode(new ResistFire(h));
			var child2 = new SkillTree.SkillNode(new Vengeance(h));

			root.AddChild(child1);
			root.AddChild(child2);

			var tree = new SkillTree(h, [root]);
			var skillset = new SkillSet(h, tree);

			return skillset;
		}
	}

	public class MonsterBaseSetup : IUnitSetupFactory
	{
		private int _level;

		public MonsterBaseSetup(int level)
		{
			_level = level;
		}

		public Stat<Vital> Life() => GetLifePerMonsterLevel(_level);
		public Stat<Vital> Mana() => new(new(@base: 15, levelCoef: 1.5, attrCoef: 2));
		public SkillSet Skills(Unit u) => new(u, null);

		/// <summary>
		/// hp = base + (level * 3 vitalityPerLevel * 2 coef)
		/// </summary>
		private static Stat<Vital> GetLifePerMonsterLevel(int forLevel)
		{
			return new Stat<Vital>(new(@base: (5 + forLevel * 3 * 2), 0, 0));
		}
	}
}
