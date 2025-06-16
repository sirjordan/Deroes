namespace Deroes.Core
{
	public abstract class Unit
	{
		public const int MAX_LEVEL = 99;

		public string Name { get; protected set; }
		public Stat Life { get; protected set; }
		public int Level { get; protected set; }
		public int Defense { get; protected set; }
		public int Damage { get; protected set; }

		public bool IsAlive => (Life.Remaining > 0);
		
		protected Unit()
		{
			Level = 1;
		}

		/// <summary>
		/// Do damage and returns the hitpoint
		/// </summary>
		/// <returns>Damage dealt calculated</returns>
		public double Attack(Unit other)
		{
			var hitPoints = (Damage - other.Defense);
			if (hitPoints < 1)
			{
				hitPoints = 1;
			}

			other.Life.OnAction(-hitPoints);
			
			return hitPoints;
		}

		public void Die() 
		{
			Console.WriteLine($"{Name} was killed");
		}
	}
}
