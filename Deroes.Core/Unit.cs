using Deroes.Core.Stats;

namespace Deroes.Core
{
	public abstract class Unit
	{
		public string Name { get; protected set; }
		public Stat<Vital> Life { get; protected set; }
		public int Level { get; protected set; }
		public Attack Damage { get; private set; }
		public Defense Resistanse { get; private set; }

		public bool IsAlive => (Life.Value.Remaining > 0);
		
		protected Unit()
		{
			Level = 1;
			Damage = new Attack();
			Resistanse = new Defense();
		}

		/// <summary>
		/// Do damage and returns the hitpoint
		/// </summary>
		/// <returns>Damage dealt calculated</returns>
		public double Attack(Unit other)
		{
			var hitpoins = Damage.Apply(other);
			Console.WriteLine($"{Name} did {hitpoins} of damage to {other.Name}");

			return hitpoins;
		}

		public void Die() 
		{
			Console.WriteLine($"{Name} was killed");
		}
	}
}
