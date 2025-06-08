namespace Deroes.Core
{
	public abstract class Unit
	{
		public const int MAX_LEVEL = 99;
		public int Level { get; protected set; }
		public double Life { get; protected set; }
		public int Defense { get; protected set; }

		public string? Name { get; protected set; }
		public bool IsAlive => (Life > 0);
		public int Damage { get; protected set; }
	
		protected Unit()
		{
			Level = 1;
		}

		/// <returns>Damage dealt calculated</returns>
		public int Attack(Unit other)
		{
			var hitPoints = (Damage - other.Defense);
			other.Life -= hitPoints;

			return hitPoints;
		}

		public void Die() 
		{
			Console.WriteLine($"{Name} was killed");
		}
	}
}
