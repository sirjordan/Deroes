using Deroes.Core.Skills;
using Deroes.Core.Stats;

namespace Deroes.Core.Units
{
	public abstract class Unit
	{
		public abstract int Max_Level { get; }
		public int Level { get; protected set; }
		public string Name { get; private set; }
		public Stat<Vital> Life { get; private set; }
		public Stat<Vital> Mana { get; private set; }
		public Attack Melee { get; private set; }
		public Defense Resistanse { get; private set; }
		public SkillSet Skills { get; private set; }
		public bool IsAlive => Life.Value.Remaining > 0;

		protected Unit(string name, IUnitSetupFactory unitSetup)
		{
			Name = name;
			Life = unitSetup.Life();
			Mana = unitSetup.Mana();
			Level = 1;
			Melee = new Attack();
			Resistanse = new Defense();
			Skills = unitSetup.Skills(this);
		}

		public virtual void Die()
		{
			Console.WriteLine($"{Name} was killed");
		}
	}
}
