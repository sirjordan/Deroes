using Deroes.Core.Items;
using Deroes.Core.Stats;

namespace Deroes.Core.Units
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
	// 19, Anti-spells (aka Curses)
	// 20. Skills: Attack modifiers (aka Vengeance, adds Fire, Ligt, Cold dmg)
	// 21. Attributes as Stats modifiers 
	// 22. Drink potions not only for Hero. (e.g monsters can heal)

	public abstract class Hero : Unit
	{
		public override int Max_Level => 99;
		public Stat<Vital> Stamina { get; protected set; }
		public Attributes Attributes { get; protected set; }
		public Stash<Item> Inventory { get; protected set; }
		public Chest Chest { get; protected set; }
		public Gold Gold { get; protected set; }
		public Gear Gear { get; protected set; }
		public long Experience { get; private set; }

		protected Hero(string name, Stat<Vital> life, Stat<Vital> mana, Stat<Vital> stamina) 
			: base(name, life)
		{
			Mana = mana;
			Stamina = stamina;

			Experience = 0;
			Inventory = new Stash<Item>(10, 4);
			Chest = new Chest(this);
			Gold = new Gold(this);
			Gear = new Gear(this);
		}

		public static Hero CreatePaladin() => new Paladin();

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

		public void AddExperience(long xp)
		{
			Experience += xp;

			while (Experience >= XpToLevelUp(Level) && Level < Max_Level)
			{
				LevelUp();
			}
		}

		public override void Die()
		{
			// TODO: Drop gold (max 20%)
			base.Die();
		}

		private void LevelUp()
		{
			Level++;

			Attributes.OnLevelUp();
			Life.BaseValue.OnLevelUp();
			Mana.BaseValue.OnLevelUp();
			Stamina.BaseValue.OnLevelUp();
		}
	}
}
