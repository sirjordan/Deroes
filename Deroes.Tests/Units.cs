using Deroes.Core;

namespace Deroes.Tests
{
	[TestClass]
	public sealed class Units
	{
		[TestMethod]
		public void Unit_Paladin_Attack_Monster()
		{
			var player = Hero.CreatePaladin();
			var enemy = new Monster();

			player.Attack(enemy);

			Assert.IsTrue(enemy.Life.Remaining <= 0);
			Assert.IsFalse(enemy.IsAlive);
		}

		[TestMethod]
		public void Unit_Monster_Attack_Paladin()
		{
			var player = Hero.CreatePaladin();
			var enemy = new Monster();

			enemy.Attack(player);

			Assert.IsTrue(player.IsAlive);
			Assert.AreEqual(54, player.Life.Remaining);
		}

		[TestMethod]
		public void Attack_By_Hero_Kill()
		{
			var player = Hero.CreatePaladin();
			var enemy = new Monster();

			new Attack(player, enemy).ByHero();

			Assert.IsFalse(enemy.IsAlive);
			Assert.IsTrue(player.Experience > 0);
		}

		[TestMethod]
		public void Attack_By_Monster()
		{
			var player = Hero.CreatePaladin();
			var enemy = new Monster();

			new Attack(player, enemy).ByMonster();

			Assert.IsTrue(player.IsAlive);
			Assert.AreEqual(54, player.Life.Remaining);
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
			Assert.AreEqual(Unit.MAX_LEVEL, hero.Level);
		}

		[TestMethod]
		public void Hero_Paladin_Mana_Life_Leveling_lvl1_lvl_2()
		{
			var lvl2 = Hero.CreatePaladin();
			lvl2.AddExperience(Hero.XpToLevelUp(1));

			Assert.AreEqual(2, lvl2.Level);
			Assert.AreEqual(57, lvl2.Life.Max);
			Assert.AreEqual(16.5, lvl2.Mana.Max);

			lvl2.AddExperience(Hero.XpToLevelUp(2));

			Assert.AreEqual(3, lvl2.Level);
			Assert.AreEqual(59, lvl2.Life.Max);
			Assert.AreEqual(18, lvl2.Mana.Max);
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
			Assert.AreEqual(105, hero.Life.Max);
			Assert.AreEqual(52.5, hero.Mana.Max);
			Assert.AreEqual(114, hero.Stamina.Max);
		}

		[TestMethod]
		public void Hero_Paladin_Mana_Life_Leveling_lvl_50()
		{
			var lvl50 = Hero.CreatePaladin();
			var xpToLevel50 = 47254998;

			lvl50.AddExperience(xpToLevel50);

			Assert.AreEqual(50, lvl50.Level);
			Assert.AreEqual(153, lvl50.Life.Max);
			Assert.AreEqual(88.5, lvl50.Mana.Max);
			Assert.AreEqual(138, lvl50.Stamina.Max);
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
			Assert.AreEqual(243, hero.Life.Max);
			Assert.AreEqual(156, hero.Mana.Max);
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

			Assert.AreEqual(hero.Life.Base + hero.Life.AttributeCoef * 5, hero.Life.Max);
			Assert.AreEqual(hero.Stamina.Base + hero.Stamina.AttributeCoef * 5, hero.Stamina.Max);

		}

		[TestMethod]
		public void Hero_Paladin_AddVitality_50Points()
		{
			var hero = Hero.CreatePaladin();

			for (int i = 0; i < 50; i++)
			{
				hero.AddVitality();
			}

			Assert.AreEqual(hero.Life.Base + hero.Life.AttributeCoef * 50, hero.Life.Max);
			Assert.AreEqual(hero.Stamina.Base + hero.Stamina.AttributeCoef * 50, hero.Stamina.Max);
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

			Assert.AreEqual(hero.Mana.Base + hero.Mana.AttributeCoef * 5, hero.Mana.Max);
		}

		[TestMethod]
		public void Hero_Paladin_AddEnergy_50Points()
		{
			var hero = Hero.CreatePaladin();

			for (int i = 0; i < 50; i++)
			{
				hero.AddEnergy();
			}

			Assert.AreEqual(hero.Mana.Base + hero.Mana.AttributeCoef * 50, hero.Mana.Max);
		}
	}
}
