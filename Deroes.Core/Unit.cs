namespace Deroes.Core
{
	public abstract class Unit
	{
		public const int MAX_LEVEL = 99;

		public string Name { get; protected set; }
		public Stat Life { get; protected set; }
		public int Level { get; protected set; }
		public AttackDamage Damage { get; protected set; }
		public AttackResistanse Resistanses { get; protected set; }

		public bool IsAlive => (Life.Remaining > 0);
		
		protected Unit()
		{
			Level = 1;
			Damage = new AttackDamage();
			Resistanses = new AttackResistanse();
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
