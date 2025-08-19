using Deroes.Core.Items.Wearables;
using Deroes.Core.Stats.Modifiers;
using Deroes.Core.Units;

namespace Deroes.Core.Tests
{
	[TestClass]
	public sealed class Units
	{
		private Hero hero;

		[TestInitialize]
		public void Setup()
		{
			hero = new Hero("", new PaladinSetup());
		}

		[TestMethod]
		public void Unit_Paladin_Attack_Monster()
		{
			var enemy = new Monster(1);

			var sword = new Weapon(new WeaponItemSpec(5, 10));

			hero.Gear.Equip(sword, _ => _.LeftHand);

			hero.Skills.Defaults.NormalAttack.Apply(enemy);
			hero.Skills.Defaults.NormalAttack.Apply(enemy);

			Assert.IsTrue(enemy.Life.Value.Remaining <= 0);
			Assert.IsFalse(enemy.IsAlive);
		}

		[TestMethod]
		public void Unit_Paladin_Attack_Monster_Multiple_Damage()
		{
			var enemy = new Monster(84);

			var spec = new WeaponItemSpec(5, 10);
			spec.Modifiers.Add(new PhysicalDamageModifier(new FlatDamageModifier(5), new FlatDamageModifier(10)));
			spec.Modifiers.Add(new ColdDamageModifier(new FlatDamageModifier(5), new FlatDamageModifier(10)));

			var sword = new Weapon(spec);

			hero.Gear.Equip(sword, _ => _.LeftHand);

			hero.Skills.Defaults.NormalAttack.Apply(enemy);

			Assert.IsTrue(enemy.IsAlive);
			Assert.IsTrue(enemy.Life.Value.Remaining <= (4000 - 15));
		}

		[TestMethod]
		public void Unit_Min_Max_Damage()
		{
			var defaultDmg_min = 1;
			var defaultDmg_max = 2;

			var spec = new WeaponItemSpec(5, 10);
			spec.Modifiers.Add(new PhysicalDamageModifier(new FlatDamageModifier(5), new FlatDamageModifier(10)));
			spec.Modifiers.Add(new ColdDamageModifier(new FlatDamageModifier(5), new FlatDamageModifier(10)));

			var sword = new Weapon(spec);

			hero.Gear.Equip(sword, _ => _.LeftHand);

			Assert.AreEqual(15 + defaultDmg_min, hero.Melee.Min);
			Assert.AreEqual(30 + defaultDmg_max, hero.Melee.Max);
		}

		[TestMethod]
		public void Unit_Percentage_Damage()
		{
			var spec = new WeaponItemSpec(9, 18); // 10-20 with the default
			spec.Modifiers.Add(new PhysicalDamageModifier(new PercentageDamageModifier(50), new PercentageDamageModifier(50)));

			var sword = new Weapon(spec);

			hero.Gear.Equip(sword, _ => _.LeftHand);

			Assert.AreEqual(15, hero.Melee.Min);
			Assert.AreEqual(30, hero.Melee.Max);
		}

		[TestMethod]
		public void Unit_Min_Max_Damage_On_Equip_Unequip()
		{
			var defaultDmg_min = 1;
			var defaultDmg_max = 2;

			var spec = new WeaponItemSpec(5, 10)
			{
				Modifiers = [
					new PhysicalDamageModifier(new FlatDamageModifier(5), new FlatDamageModifier(10)),
					new ColdDamageModifier(new FlatDamageModifier(5), new FlatDamageModifier(10))
				]
			};

			var sword = new Weapon(spec);

			hero.Gear.Equip(sword, _ => _.LeftHand);

			Assert.AreEqual(15 + defaultDmg_min, hero.Melee.Min);
			Assert.AreEqual(30 + defaultDmg_max, hero.Melee.Max);

			var droped = hero.Gear.Unequip(_ => _.LeftHand);

			Assert.AreEqual(defaultDmg_min, hero.Melee.Min);
			Assert.AreEqual(defaultDmg_max, hero.Melee.Max);
		}

		[TestMethod]
		public void Unit_Monster_Attack_Paladin()
		{
			var enemy = new Monster(10);

			enemy.Skills.Defaults.NormalAttack.Apply(hero);

			Assert.IsTrue(hero.IsAlive);
			Assert.IsTrue(53 <= hero.Life.Value.Remaining || hero.Life.Value.Remaining >= 54, $"Player life is : {hero.Life.Value.Remaining}");
		}

		[TestMethod]
		public void Attack_By_Hero_Kill()
		{
			var enemy = new Monster(1);

			var sword = new Weapon(new WeaponItemSpec(5, 10));
			hero.Gear.Equip(sword, _ => _.LeftHand);


			new Combat(hero, enemy)
				.HeroAttacks(_ => _.Skills.Primary)
				.HeroAttacks(_ => _.Skills.Primary);

			Assert.IsFalse(enemy.IsAlive);
			Assert.IsTrue(hero.Experience > 0);
		}

		[TestMethod]
		public void Attack_By_Monster()
		{
			var enemy = new Monster(10);

			new Combat(hero, enemy)
				.MonsterAttacks(_ => _.Skills.Primary);

			Assert.IsTrue(hero.IsAlive);
			Assert.IsTrue(53 <= hero.Life.Value.Remaining || hero.Life.Value.Remaining >= 54, $"Player life is : {hero.Life.Value.Remaining}");
		}

		[TestMethod]
		public void Hero_AddExperience()
		{
			// Arrange
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
			int xpToLevel2 = 500; // XP needed from level 1 to 2

			// Act
			hero.AddExperience(xpToLevel2);

			// Assert
			Assert.AreEqual(2, hero.Level);
			Assert.AreEqual(xpToLevel2, hero.Experience);
			Assert.AreEqual(5, hero.Attributes.StatPointsAvailable);
		}

		[TestMethod]
		public void Hero_AddExperience_Level_Up_To_13_From_1()
		{
			// Arrange
			var xpToLevel13 = 57810;

			// Act
			hero.AddExperience(xpToLevel13);

			// Assert
			Assert.AreEqual(13, hero.Level);
			Assert.AreEqual(xpToLevel13, hero.Experience);
			Assert.AreEqual(5 * 13 - 5, hero.Attributes.StatPointsAvailable);
		}

		[TestMethod]
		public void Hero_AddExperience_Level_Up_To_26_From_1()
		{
			// Arrange
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
			var lvl99Xp = 3520485254765675;

			// Act
			hero.AddExperience(lvl99Xp);
			hero.AddExperience(lvl99Xp);
			hero.AddExperience(lvl99Xp);
			hero.AddExperience(lvl99Xp);

			// Assert
			Assert.AreEqual(hero.Max_Level, hero.Level);
		}

		[TestMethod]
		public void Hero_Paladin_Mana_Life_Leveling_lvl1_lvl_2()
		{
			hero.AddExperience(Hero.XpToLevelUp(1));

			Assert.AreEqual(2, hero.Level);
			Assert.AreEqual(57, hero.Life.Value.Max);
			Assert.AreEqual(16.5, hero.Mana.Value.Max);

			hero.AddExperience(Hero.XpToLevelUp(2));

			Assert.AreEqual(3, hero.Level);
			Assert.AreEqual(59, hero.Life.Value.Max);
			Assert.AreEqual(18, hero.Mana.Value.Max);
		}

		[TestMethod]
		public void Hero_Paladin_Mana_Life_Leveling_lvl_26()
		{
			// Arrange
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
			var xpToLevel50 = 47254998;

			hero.AddExperience(xpToLevel50);

			Assert.AreEqual(50, hero.Level);
			Assert.AreEqual(153, hero.Life.Value.Max);
			Assert.AreEqual(88.5, hero.Mana.Value.Max);
			Assert.AreEqual(138, hero.Stamina.Value.Max);
		}

		[TestMethod]
		public void Hero_Paladin_Mana_Life_Leveling_lvl_95()
		{
			// Arrange
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
			var xpToLevel26 = 538100;
			var points = 5;
			
			hero.AddExperience(xpToLevel26);

			hero.Attributes.AddVitality();
			hero.Attributes.AddVitality();
			hero.Attributes.AddVitality();
			hero.Attributes.AddVitality();
			hero.Attributes.AddVitality();

			var expectedHp = (hero.Level * hero.Life.Value.LevelCoef - hero.Life.Value.LevelCoef) + (hero.Life.Value.Base + hero.Life.Value.AttributeCoef * points);
			var expStamina = (hero.Level * hero.Stamina.Value.LevelCoef - hero.Stamina.Value.LevelCoef) + (hero.Stamina.Value.Base + hero.Stamina.Value.AttributeCoef * points);

			Assert.AreEqual(expectedHp, hero.Life.Value.Max, 0.5);
			Assert.AreEqual(expStamina, hero.Stamina.Value.Max, 0.5);

		}

		[TestMethod]
		public void Hero_Paladin_AddVitality_50Points()
		{
			var xpToLevel26 = 538100;
			var points = 50;
			
			hero.AddExperience(xpToLevel26);

			for (int i = 0; i < points; i++)
			{
				hero.Attributes.AddVitality();
			}

			var expectedHp = (hero.Level * hero.Life.Value.LevelCoef - hero.Life.Value.LevelCoef) + (hero.Life.Value.Base + hero.Life.Value.AttributeCoef * points);
			var expStamina = (hero.Level * hero.Stamina.Value.LevelCoef - hero.Stamina.Value.LevelCoef) + (hero.Stamina.Value.Base + hero.Stamina.Value.AttributeCoef * points);


			Assert.AreEqual(expectedHp, hero.Life.Value.Max);
			Assert.AreEqual(expStamina, hero.Stamina.Value.Max);
		}

		[TestMethod]
		public void Hero_Paladin_AddEnergy_5Points()
		{
			var points = 5;
			hero.AddExperience(Hero.XpToLevelUp(1));

			hero.Attributes.AddEnergy();
			hero.Attributes.AddEnergy();
			hero.Attributes.AddEnergy();
			hero.Attributes.AddEnergy();
			hero.Attributes.AddEnergy();

			var expected = (hero.Level * hero.Mana.Value.LevelCoef - hero.Mana.Value.LevelCoef) + (hero.Mana.Value.Base + hero.Mana.Value.AttributeCoef * points);
			Assert.AreEqual(expected, hero.Mana.Value.Max, 0.5);
		}

		[TestMethod]
		public void Hero_Paladin_AddEnergy_50Points()
		{
			var points = 50;
			hero.AddExperience(Hero.XpToLevelUp(10));

			for (int i = 0; i < points; i++)
			{
				hero.Attributes.AddEnergy();
			}

			var expected = (hero.Level * hero.Mana.Value.LevelCoef - hero.Mana.Value.LevelCoef) + (hero.Mana.Value.Base + hero.Mana.Value.AttributeCoef * points);
			Assert.AreEqual(expected, hero.Mana.Value.Max, 0.5);
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
