namespace Deroes.Core.Stats
{
	public class Defense
	{
		public Stat<Resistanse> Physical { get; private set; }
		public Stat<Resistanse> Cold { get; private set; }
		public Stat<Resistanse> Fire { get; private set; }
		public Stat<Resistanse> Poison { get; private set; }
		public Stat<Resistanse> Lightining { get; private set; }

		public Defense()
		{
			Physical = new Stat<Resistanse>(new Resistanse());
			Cold = new Stat<Resistanse>(new Resistanse());
			Fire = new Stat<Resistanse>(new Resistanse());
			Poison = new Stat<Resistanse>(new Resistanse());
			Lightining = new Stat<Resistanse>(new Resistanse());
		}
	}
}
