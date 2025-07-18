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
			Physical = new Stat<Resistanse>(new PhysicalResistanse());
			Cold = new Stat<Resistanse>(new ElementalResistance());
			Fire = new Stat<Resistanse>(new ElementalResistance());
			Poison = new Stat<Resistanse>(new ElementalResistance());
			Lightining = new Stat<Resistanse>(new ElementalResistance());
		}
	}
}
