using Deroes.Core.Units;

namespace Deroes.Core.Skills
{
	public class SkillTree : ILevelUpSubscriber
	{
		private Unit _hero;
		private IEnumerable<SkillNode> RootSkills { get; set; }
		public int AvaliableSkillPoints { get; private set; }

		public SkillTree(Unit hero)
		{
			RootSkills = new HashSet<SkillNode>();
			AvaliableSkillPoints = 0;
			_hero = hero;
		}

		public IEnumerable<SkillNode> GetAllSkills()
		{
			// BFS
			var visited = new HashSet<SkillNode>();
			var queue = new Queue<SkillNode>(RootSkills);

			while (queue.Count > 0)
			{
				var node = queue.Dequeue();
				if (!visited.Add(node))
					continue;

				yield return node;

				foreach (var child in node.Children)
					queue.Enqueue(child);
			}
		}

		public void AddSkillPoint(SkillNode skill)
		{
			ArgumentNullException.ThrowIfNull(nameof(skill));

			if (AvaliableSkillPoints == 0)
				throw new InvalidOperationException("Not enough skill points");

			var found = GetAllSkills().First(_ => _ == skill); // TODO: This might be weak (ref only?) Consider IComparable/IEquitable
			if (found.CanUpSkill(_hero))
			{
				AvaliableSkillPoints--;
				// TODO: Upskill?
				// TODO: Add bonus to childs "synergies"
			}
			else
			{
				throw new InvalidOperationException("Cannot upskill");
			}
		}

		public void OnLevelUp()
		{
			AvaliableSkillPoints++;
		}

		public class SkillNode
		{
			public Skill Skill { get; private set; }
			public ICollection<SkillNode> Parents { get; private set; }
			public ICollection<SkillNode> Children { get; private set; }

			public SkillNode(Skill skill)
			{
				Parents = new HashSet<SkillNode>();
				Children = new HashSet<SkillNode>();
				Skill = skill;
			}

			public bool CanUpSkill(Unit hero)
			{
				return
					hero.Level >= Skill.RequiredLevel &&    // Hero reached level required
					Parents.All(p => p.Skill.Level > 1);    // All parents must have at least 1 skill point
			}

			public SkillNode AddChild(SkillNode child)
			{
				ArgumentNullException.ThrowIfNull(child, nameof(child));

				if (child.Skill.Tier < Skill.Tier)
				{
					throw new ArgumentOutOfRangeException("Child node must be equal or grater Tier");
				}

				if (!Children.Contains(child))
				{
					Children.Add(child);
					child.Parents.Add(this);
				}

				return this;
			}
		}
	}
}
