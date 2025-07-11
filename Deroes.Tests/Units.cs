using Deroes.Core;
using Deroes.Core.Items;
using Deroes.Core.Items.Wearables;
using Deroes.Core.Stats;
using Deroes.Core.Stats.Modifiers;
using System.Reflection.Emit;

namespace Deroes.Tests
{
	[TestClass]
	public sealed class Units
	{
		[TestMethod]
		public void Unit_Paladin_Attack_Monster()
		{
			var player = Hero.CreatePaladin();
			var enemy = new Monster(1);

			var sword = new Weapon(new WeaponItemSpec(5, 10));

			player.Gear.Equip(sword, _ => _.LeftHand);

			player.Attack(enemy);
			player.Attack(enemy);

			Assert.IsTrue(enemy.Life.Value.Remaining <= 0);
			Assert.IsFalse(enemy.IsAlive);
		}

		[TestMethod]
		public void Unit_Paladin_Attack_Monster_Multiple_Damage()
		{
			var player = Hero.CreatePaladin();
			var enemy = new Monster(84);

			var spec = new WeaponItemSpec(5, 10);
			spec.Modifiers.Add(new PhysicalDamageModifier(new FlatDamageModifier(5), new FlatDamageModifier(10)));
			spec.Modifiers.Add(new ColdDamageModifier(new FlatDamageModifier(5), new FlatDamageModifier(10)));

			var sword = new Weapon(spec);

			player.Gear.Equip(sword, _ => _.LeftHand);

			player.Attack(enemy);

			Assert.IsTrue(enemy.IsAlive);
			Assert.IsTrue(enemy.Life.Value.Remaining <= (4000 - 15));
		}

		[TestMethod]
		public void Unit_Min_Max_Damage()
		{
			var defaultDmg_min = 1;
			var defaultDmg_max = 2;

			var player = Hero.CreatePaladin();

			var spec = new WeaponItemSpec(5, 10);
			spec.Modifiers.Add(new PhysicalDamageModifier(new FlatDamageModifier(5), new FlatDamageModifier(10)));
			spec.Modifiers.Add(new ColdDamageModifier(new FlatDamageModifier(5), new FlatDamageModifier(10)));

			var sword = new Weapon(spec);

			player.Gear.Equip(sword, _ => _.LeftHand);

			Assert.AreEqual(15 + defaultDmg_min, player.Damage.Min);
			Assert.AreEqual(30 + defaultDmg_max, player.Damage.Max);
		}

		[TestMethod]
		public void Unit_Min_Max_Damage_On_Equip_Unequip()
		{
			var defaultDmg_min = 1;
			var defaultDmg_max = 2;

			var player = Hero.CreatePaladin();

			var spec = new WeaponItemSpec(5, 10)
			{
				Modifiers = [
					new PhysicalDamageModifier(new FlatDamageModifier(5), new FlatDamageModifier(10)),
					new ColdDamageModifier(new FlatDamageModifier(5), new FlatDamageModifier(10))
				]
			};

			var sword = new Weapon(spec);

			player.Gear.Equip(sword, _ => _.LeftHand);

			Assert.AreEqual(15 + defaultDmg_min, player.Damage.Min);
			Assert.AreEqual(30 + defaultDmg_max, player.Damage.Max);

			var droped = player.Gear.Unequip(_ => _.LeftHand);

			Assert.AreEqual(defaultDmg_min, player.Damage.Min);
			Assert.AreEqual(defaultDmg_max, player.Damage.Max);
		}

		[TestMethod]
		public void Unit_Monster_Attack_Paladin()
		{
			var player = Hero.CreatePaladin();
			var enemy = new Monster(10);

			enemy.Attack(player);

			Assert.IsTrue(player.IsAlive);
			Assert.IsTrue(53 <= player.Life.Value.Remaining || player.Life.Value.Remaining >= 54, $"Player life is : {player.Life.Value.Remaining}");
		}

		[TestMethod]
		public void Attack_By_Hero_Kill()
		{
			var player = Hero.CreatePaladin();
			var enemy = new Monster(1);

			var sword = new Weapon(new WeaponItemSpec(5, 10));
			player.Gear.Equip(sword, _ => _.LeftHand);


			new Combat(player, enemy)
				.HeroAttacks()
				.HeroAttacks();

			Assert.IsFalse(enemy.IsAlive);
			Assert.IsTrue(player.Experience > 0);
		}

		[TestMethod]
		public void Attack_By_Monster()
		{
			var player = Hero.CreatePaladin();
			var enemy = new Monster(10);

			new Combat(player, enemy).MonsterAttacks();

			Assert.IsTrue(player.IsAlive);
			Assert.IsTrue(53 <= player.Life.Value.Remaining || player.Life.Value.Remaining >= 54, $"Player life is : {player.Life.Value.Remaining}");
		}

		[TestMethod]
		public void Hero_AddExperience()
		{
			// Arrange
			var hero = Hero.CreatePaladin();
			int xpToAdd = 100;

			// Act
			hero.AddExperience(xpToAdd);

			// Assert
			Assert.AreEqual(1, hero.Level); // Should not level up
			Assert.AreEqual(100, hero.Experience);
		}

		[TestMethod]
		public void Hero_AddExperience_Level_Up_Single_From_1()
		{
			// Arrange
			var hero = Hero.CreatePaladin();
			int xpToLevel2 = 500; // XP needed from level 1 to 2

			// Act
			hero.AddExperience(xpToLevel2);

			// Assert
			Assert.AreEqual(2, hero.Level);
			Assert.AreEqual(xpToLevel2, hero.Experience);
		}

		[TestMethod]
		public void Hero_AddExperience_Level_Up_To_13_From_1()
		{
			// Arrange
			var hero = Hero.CreatePaladin();

			var xpToLevel13 = 57810;

			// Act
			hero.AddExperience(xpToLevel13);

			// Assert
			Assert.AreEqual(13, hero.Level);
			Assert.AreEqual(xpToLevel13, hero.Experience);
		}

		[TestMethod]
		public void Hero_AddExperience_Level_Up_To_26_From_1()
		{
			// Arrange
			var hero = Hero.CreatePaladin();

			var xpToLevel26 = 538100;

			// Act
			hero.AddExperience(xpToLevel26);

			// Assert
			Assert.AreEqual(26, hero.Level);
		}

		[TestMethod]
		public void Hero_AddExperience_Level_Up_To_50_From_1()
		{
			// Arrange
			var hero = Hero.CreatePaladin();

			var xpToLevel50 = 47254998;

			// Act
			hero.AddExperience(xpToLevel50);

			// Assert
			Assert.AreEqual(50, hero.Level);
		}

		[TestMethod]
		public void Hero_AddExperience_Reach_Level_95_From_1()
		{
			// Arrange
			var hero = Hero.CreatePaladin();
			var lvl95Xp = 3520485254;

			// Act
			hero.AddExperience(lvl95Xp);

			// Assert
			Assert.AreEqual(95, hero.Level);
			Assert.AreEqual(lvl95Xp, hero.Experience);
		}

		[TestMethod]
		public void Hero_AddExperience_Level_To_Max_Limit_From_1()
		{
			// Arrange
			var hero = Hero.CreatePaladin();
			var lvl99Xp = 3520485254765675;

			// Act
			hero.AddExperience(lvl99Xp);
			hero.AddExperience(lvl99Xp);
			hero.AddExperience(lvl99Xp);
			hero.AddExperience(lvl99Xp);

			// Assert
			Assert.AreEqual(Hero.MAX_LEVEL, hero.Level);
		}

		[TestMethod]
		public void Hero_Paladin_Mana_Life_Leveling_lvl1_lvl_2()
		{
			var lvl2 = Hero.CreatePaladin();
			lvl2.AddExperience(Hero.XpToLevelUp(1));

			Assert.AreEqual(2, lvl2.Level);
			Assert.AreEqual(57, lvl2.Life.Value.Max);
			Assert.AreEqual(16.5, lvl2.Mana.Value.Max);

			lvl2.AddExperience(Hero.XpToLevelUp(2));

			Assert.AreEqual(3, lvl2.Level);
			Assert.AreEqual(59, lvl2.Life.Value.Max);
			Assert.AreEqual(18, lvl2.Mana.Value.Max);
		}

		[TestMethod]
		public void Hero_Paladin_Mana_Life_Leveling_lvl_26()
		{
			// Arrange
			var hero = Hero.CreatePaladin();
			var xpToLevel26 = 538100;
			// Act
			hero.AddExperience(xpToLevel26);

			// Assert
			Assert.AreEqual(26, hero.Level);
			Assert.AreEqual(105, hero.Life.Value.Max);
			Assert.AreEqual(52.5, hero.Mana.Value.Max);
			Assert.AreEqual(114, hero.Stamina.Value.Max);
		}

		[TestMethod]
		public void Hero_Paladin_Mana_Life_Leveling_lvl_50()
		{
			var lvl50 = Hero.CreatePaladin();
			var xpToLevel50 = 47254998;

			lvl50.AddExperience(xpToLevel50);

			Assert.AreEqual(50, lvl50.Level);
			Assert.AreEqual(153, lvl50.Life.Value.Max);
			Assert.AreEqual(88.5, lvl50.Mana.Value.Max);
			Assert.AreEqual(138, lvl50.Stamina.Value.Max);
		}

		[TestMethod]
		public void Hero_Paladin_Mana_Life_Leveling_lvl_95()
		{
			// Arrange
			var hero = Hero.CreatePaladin();
			var lvl95Xp = 3520485254;

			// Act
			hero.AddExperience(lvl95Xp);

			// Assert
			Assert.AreEqual(95, hero.Level);
			Assert.AreEqual(243, hero.Life.Value.Max);
			Assert.AreEqual(156, hero.Mana.Value.Max);
		}

		[TestMethod]
		public void Hero_Paladin_AddVitality_5Points()
		{
			var hero = Hero.CreatePaladin();

			hero.AddVitality();
			hero.AddVitality();
			hero.AddVitality();
			hero.AddVitality();
			hero.AddVitality();

			Assert.AreEqual(hero.Life.Value.Base + hero.Life.Value.AttributeCoef * 5, hero.Life.Value.Max);
			Assert.AreEqual(hero.Stamina.Value.Base + hero.Stamina.Value.AttributeCoef * 5, hero.Stamina.Value.Max);

		}

		[TestMethod]
		public void Hero_Paladin_AddVitality_50Points()
		{
			var hero = Hero.CreatePaladin();

			for (int i = 0; i < 50; i++)
			{
				hero.AddVitality();
			}

			Assert.AreEqual(hero.Life.Value.Base + hero.Life.Value.AttributeCoef * 50, hero.Life.Value.Max);
			Assert.AreEqual(hero.Stamina.Value.Base + hero.Stamina.Value.AttributeCoef * 50, hero.Stamina.Value.Max);
		}

		[TestMethod]
		public void Hero_Paladin_AddEnergy_5Points()
		{
			var hero = Hero.CreatePaladin();

			hero.AddEnergy();
			hero.AddEnergy();
			hero.AddEnergy();
			hero.AddEnergy();
			hero.AddEnergy();

			Assert.AreEqual(hero.Mana.Value.Base + hero.Mana.Value.AttributeCoef * 5, hero.Mana.Value.Max);
		}

		[TestMethod]
		public void Hero_Paladin_AddEnergy_50Points()
		{
			var hero = Hero.CreatePaladin();

			for (int i = 0; i < 50; i++)
			{
				hero.AddEnergy();
			}

			Assert.AreEqual(hero.Mana.Value.Base + hero.Mana.Value.AttributeCoef * 50, hero.Mana.Value.Max);
		}

		[TestMethod]
		public void Monster_Health_Per_Level_1()
		{
			var m = new Monster(1);
			var expectedHp = 5 + (1 * 3 * 2);

			Assert.AreEqual(expectedHp, m.Life.Value.Remaining);
			Assert.AreEqual(expectedHp, m.Life.Value.Max);
		}

		[TestMethod]
		public void Monster_Health_Per_Level_10()
		{
			var m = new Monster(10);
			var expectedHp = 5 + (10 * 3 * 2);

			Assert.AreEqual(expectedHp, m.Life.Value.Remaining);
			Assert.AreEqual(expectedHp, m.Life.Value.Max);
		}

		[TestMethod]
		public void Monster_Health_Per_Level_20()
		{
			var m = new Monster(20);
			var expectedHp = 5 + (20 * 3 * 2);

			Assert.AreEqual(expectedHp, m.Life.Value.Remaining);
			Assert.AreEqual(expectedHp, m.Life.Value.Max);
		}

		[TestMethod]
		public void Monster_Health_Per_Level_35()
		{
			var m = new Monster(35);
			var expectedHp = 5 + (35 * 3 * 2);

			Assert.AreEqual(expectedHp, m.Life.Value.Remaining);
			Assert.AreEqual(expectedHp, m.Life.Value.Max);
		}

		[TestMethod]
		public void Monster_Health_Per_Level_50()
		{
			var m = new Monster(50);
			var expectedHp = 5 + (50 * 3 * 2);

			Assert.AreEqual(expectedHp, m.Life.Value.Remaining);
			Assert.AreEqual(expectedHp, m.Life.Value.Max);
		}

		[TestMethod]
		public void Monster_Health_Per_Level_85()
		{
			var m = new Monster(85);
			var expectedHp = 5 + (85 * 3 * 2);

			Assert.AreEqual(expectedHp, m.Life.Value.Remaining);
			Assert.AreEqual(expectedHp, m.Life.Value.Max);
		}
	}
}
