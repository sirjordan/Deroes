namespace Deroes.Core
{
	public class AttackResistanse
	{
		public Resistanse Physical { get; private set; }
		public Resistanse Cold { get; private set; }
		public Resistanse Fire { get; private set; }
		public Resistanse Poison { get; private set; }
		public Resistanse Lightining { get; private set; }

		public AttackResistanse()
		{
			Physical = new Resistanse();
			Cold = new Resistanse();
			Fire = new Resistanse();
			Poison = new Resistanse();
			Lightining = new Resistanse();
		}
	}

	/// <summary>
	/// Phisycal, Cold, Fire, Lightining, Poison?, Magic
	/// </summary>
	public class Resistanse
	{
		public bool Immune { get; private set; }
		public int Amount { get; private set; }

		public Resistanse()
		{
			Amount = 0;
			Immune = false;
		}
	}
}
