using Deroes.Core.Units;

namespace Deroes.Core.Stats
{
	public class Attr(int value)
	{
		public int Points { get; private set; } = value;
	}

	public class Attributes : ILevelUpSubscriber
	{
		private Hero _hero;

		public int StatPointsAvailable { get; private set; }
		public Attr Strength { get; private set; }
		public Attr Dexterity { get; private set; }
		public Attr Vitality { get; private set; }
		public Attr Energy { get; private set; }

		public Attributes(Hero h, int str, int dex, int vitality, int energy)
		{
			_hero = h;
			StatPointsAvailable = 0;
			Strength = new Attr(str);
			Dexterity = new Attr(dex);
			Vitality = new Attr(vitality);
			Energy = new Attr(energy);
		}

		public void OnLevelUp()
		{
			StatPointsAvailable += 5;
		}

		public void AddStrenght()
		{
			SubstractPointsAvailableOrThrow();
			Strength = new Attr(Strength.Points + 1);
		}

		public void AddDexterity()
		{
			SubstractPointsAvailableOrThrow();
			Dexterity = new Attr(Dexterity.Points + 1);
		}

		public void AddVitality()
		{
			SubstractPointsAvailableOrThrow();
			Vitality = new Attr(Vitality.Points + 1);
			_hero.Life.BaseValue.OnAddAttribute();
			_hero.Stamina.BaseValue.OnAddAttribute();
		}

		public void AddEnergy()
		{
			SubstractPointsAvailableOrThrow();
			Energy = new Attr(Energy.Points + 1);
			_hero.Mana.BaseValue.OnAddAttribute();
		}

		private void SubstractPointsAvailableOrThrow()
		{
			if (StatPointsAvailable > 0)
			{
				StatPointsAvailable--;
			}
			else
			{
				throw new InvalidOperationException("You cannot add more points.");
			}
		}
	}
}
