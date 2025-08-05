using Deroes.Core.Skills;
using Deroes.Core.Stats;

namespace Deroes.Core.Units
{
	public abstract class Unit
	{
		public abstract int Max_Level { get; }
		public int Level { get; protected set; }
		public string Name { get; protected set; }
		public bool IsAlive => Life.Value.Remaining > 0;
		public Stat<Vital> Life { get; protected set; }
		public Stat<Vital> Mana { get; protected set; }
		public Attack Melee { get; private set; }
		public Defense Resistanse { get; private set; }
		public SkillSet Skills { get; private set; }

		protected Unit(string name, Stat<Vital> life)
		{
			Name = name;
			Life = life;
			Level = 1;
			Melee = new Attack();
			Resistanse = new Defense();
			Skills = new SkillSet(this);
		}

		public virtual void Die()
		{
			Console.WriteLine($"{Name} was killed");
		}
	}
}
