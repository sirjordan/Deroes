namespace Deroes.Core.Stats
{
	public class Defense
	{
		public Resistanse Physical { get; private set; }
		public Resistanse Cold { get; private set; }
		public Resistanse Fire { get; private set; }
		public Resistanse Poison { get; private set; }
		public Resistanse Lightining { get; private set; }

		public Defense()
		{
			Physical = new Resistanse();
			Cold = new Resistanse();
			Fire = new Resistanse();
			Poison = new Resistanse();
			Lightining = new Resistanse();
		}
	}
}
