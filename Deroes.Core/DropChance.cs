namespace Deroes.Core
{
    /// <summary>
    /// Chance of any type (any type of item or gold)
    /// </summary>
    public class DropChance
	{
		/*No-drop chance:
		p1/1, p2/1 = 62.5%
		p2/2, p3/1, p3/2, p4/1 = 38.78 %
		p3/3, p4/2, p4/3, p5/1, p5/2, p6/1 = 24.05 %
		p4/4, p5/3, p5/4, p6/2, p6/3, p7/1, p7/2, p8/1 = 14.29 %
		p5/5, p6/4, p6/5, p7/3, p7/4, p8/2, p8/3 = 9.09 %
		p6/6, p7/5, p7/6, p8/4, p8/5 = 4.76 %
		p7/7, p8/6, p8/7 = 3.23 %
		p8/8 = 1.64%
		*/

		public static double DefaultChance => 37.5;
		public double Chance { get; private set; }

		private DropChance(double perentage)
		{
			Chance = Math.Clamp(perentage, 0, 100);
		}

		/// <summary>
		/// Special monsters or debug/test
		/// </summary>
		public static DropChance Always() => new(100);

		/// <summary>
		/// Special monsters or debug/test
		/// </summary>
		public static DropChance Never() => new(0);

		/// <summary>
		/// Special monsters only
		/// </summary>
		public static DropChance Explicit(int percentage) => new(percentage);

		/// <summary>
		/// Typical usage on most monsters
		/// </summary>
		public static DropChance Default() => new(DefaultChance);

		public bool Roll()
		{
			return Random.Shared.Next(0, 100) < Chance;
		}
	}
}
