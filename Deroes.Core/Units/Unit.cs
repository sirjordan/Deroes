using Deroes.Core.Stats;

namespace Deroes.Core.Units
{
	public abstract class Unit
	{
		public abstract int Max_Level { get; }
		public int Level { get; protected set; }
		public string Name { get; protected set; }
		public bool IsAlive => Life.Value.Remaining > 0;
		public Func<Unit, Attack> SelectedAttack { get; private set; }
		public Stat<Vital> Life { get; protected set; }
		public Attack Melee { get; private set; }
		public Attack Ranged { get; private set; }
		public Defense Resistanse { get; private set; }
		
		protected Unit(string name, Stat<Vital> life)
		{
			Name = name;
			Life = life;
			Level = 1;
			Melee = new Attack();
			Ranged = new Attack();
			Resistanse = new Defense();
			SelectedAttack = _ => _.Melee;
		}

		/// <summary>
		/// Do damage and returns the hitpoint
		/// </summary>
		/// <returns>Damage dealt calculated</returns>
		public int Attack(Unit other)
		{
			var attack = SelectedAttack(this);
			var hitpoins = attack.Apply(other);
			Console.WriteLine($"{Name} did {hitpoins} of damage to {other.Name}");

			return hitpoins;
		}

		public void SelectAttack(Func<Unit, Attack> selector)
		{
			SelectedAttack = selector;
		}

		public virtual void Die()
		{
			Console.WriteLine($"{Name} was killed");
		}
	}
}
