namespace Deroes.Core.Skills
{
	public class SkillTree : ILevelUpSubscriber
	{
		public IEnumerable<SkillNode> Skills { get; private set; }
		public int AvaliableSkillPoints { get; private set; }

		public SkillTree()
		{
			Skills = new HashSet<SkillNode>();
			AvaliableSkillPoints = 0;
		}

		public Skill[] GetAll()
		{
			throw new NotImplementedException();
		}

		public void AddSkillPoint(Skill skill)
		{
			if (AvaliableSkillPoints == 0)
			{
				throw new InvalidOperationException("Not enough skill points");
			}

			// TODO: Traverse and Find the skill
			// TODO: Check if parent has at least 1 level
			// TODO: Level up the skill

			AvaliableSkillPoints--;
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

			public SkillNode AddChild(SkillNode child)
			{
				ArgumentNullException.ThrowIfNull(child, nameof(child));

				if (child.Skill.Tier < Skill.Tier)
				{
					throw new ArgumentOutOfRangeException("Child must be equal or grater Tier");
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
