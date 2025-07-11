using Deroes.Core.Items;
using Deroes.Core.Stats;

namespace Deroes.Core
{
	// TODO:
	// 1. Range attack
	// 2. Auras
	// 3. Spells
	// 4. Strenth, Dex, Vitality and Energy as stats or something advanced? (modifing the hero)
	// 5. Attack rating
	// 6. Attack speed and any other speed concept
	// 8. Primary and secondary attack
	// 9. 1 and 2 handed weapons
	// 10. Class only gear (ex. Amazon only)
	// 11. Item drop factory
	// 13. Rare, Set and Unique items
	// 14. Per character level modifiers
	// 15. Sockets
	// 16. Durability
	// 17. Charms
	// 18. Chest/Private stash - Stash (10x10) + Gold (with max per level * 2.5 * HeroMaxGoldPerLevel)
	// 19, Anti-spells (aka Curses)

	public class Hero : Unit
	{
		public const int MAX_LEVEL = 99;
		public int MaxGold => Level * 10000;

		public Stat<Vital> Mana { get; private init; }
		public Stat<Vital> Stamina { get; private init; }
		public long Experience { get; private set; }

		public int Strength { get; private set; }
		public int Dexterity { get; private set; }
		public int Vitality { get; private set; }
		public int Energy { get; private set; }

		public Stash<Item> Inventory { get; private set; }
		public Chest Chest { get; private set; }

		public int Gold { get; private set; }
		public Gear Gear { get; private set; }

		protected Hero()
		{
			Experience = 0;
			Inventory = new Stash<Item>(10, 4);
			Chest = new Chest(this);
			Gold = 0;
			Gear = new Gear(this);
		}

		public static Hero CreatePaladin()
		{
			return new Hero
			{
				Name = "Paladin",

				Mana = new Stat<Vital>(new(@base: 15, levelCoef: 1.5, attrCoef: 2)),
				Stamina = new Stat<Vital>(new(@base: 89, levelCoef: 1, attrCoef: 1)),
				Life = new Stat<Vital>(new(@base: 55, levelCoef: 2, attrCoef: 3)),

				Strength = 25,
				Dexterity = 20,
				Vitality = 25,
				Energy = 15
			};
		}

		public static long XpToLevelUp(int fromLevel)
		{
			double xp;
			if (fromLevel <= 20)
			{
				// Lower levels: Gentle curve
				xp = 45.0 * Math.Pow(fromLevel, 2.8) + 400.0;
			}
			else
			{
				// Higher levels: Steeper progression
				xp = 0.00021 * Math.Pow(fromLevel, 6.7) + 50000.0;
			}

			return (long)Math.Round(xp);
		}

		public void DrinkPotion(IPotion p)
		{
			p.Drink(this);
		}

		public void DrinkPotion(int beltRow, int beltCol)
		{
			var potion = Gear.Belt.Drop(beltRow, beltCol);
			if (potion != null)
			{
				DrinkPotion(potion);
			}
		}

		public void AddStrenght()
		{
			Strength++;
		}

		public void AddDexterity()
		{
			Dexterity++;
		}

		public void AddVitality()
		{
			Vitality++;
			Life.BaseValue.OnAddAttribute();
			Stamina.BaseValue.OnAddAttribute();
		}

		public void AddEnergy()
		{
			Energy++;
			Mana.BaseValue.OnAddAttribute();
		}

		public void AddExperience(long xp)
		{
			Experience += xp;

			while (Experience >= XpToLevelUp(Level) && Level < MAX_LEVEL)
			{
				LevelUp();
			}
		}

		public void CollectGold(int amount)
		{
			ArgumentOutOfRangeException.ThrowIfLessThan(amount, 0);

			Gold += amount;

			if (Gold > MaxGold)
			{
				var drop = Gold - MaxGold;

				DropGold(drop);
			}
		}

		public void DropGold(int amount)
		{
			Gold -= amount;
			// TODO: Drop to the ground event
		}

		public override void Die()
		{
			// TODO: Drop gold (max 20%)
			base.Die();
		}

		private void LevelUp()
		{
			Level++;

			Life.BaseValue.OnLevelUp();
			Mana.BaseValue.OnLevelUp();
			Stamina.BaseValue.OnLevelUp();
		}
	}
}
