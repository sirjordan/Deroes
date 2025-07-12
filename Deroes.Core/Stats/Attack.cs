namespace Deroes.Core.Stats
{
	public class Attack
	{
		public DamageRange Physical { get; private set; }
		public DamageRange Cold { get; private set; }
		public DamageRange Fire { get; private set; }
		public DamageRange Poison { get; private set; }
		public DamageRange Lightining { get; private set; }

		private DamageRange[] All => [Physical, Cold, Fire, Poison, Lightining];
		public int Min => All.Sum(d => d.Min.Value.Amount);
		public int Max => All.Sum(d => d.Max.Value.Amount);

		public Attack()
			: this(new Physical(), new Cold(), new Fire(), new Poison(), new Lightining()) { }

		public Attack(Physical ph, Cold c, Fire f, Poison p, Lightining l)
		{
			Physical = ph;
			Cold = c;
			Fire = f;
			Poison = p;
			Lightining = l;
		}

		public int Apply(Unit defender)
		{
			var hitPoints = 0;
			foreach (var dmg in All)
			{
				hitPoints += dmg.Apply(defender);
			}

			return hitPoints;
		}
	}
}
