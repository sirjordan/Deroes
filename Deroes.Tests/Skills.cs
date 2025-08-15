using Deroes.Core.Items.Wearables;
using Deroes.Core.Skills;
using Deroes.Core.Units;

namespace Deroes.Core.Tests;

[TestClass]
public class Skills
{
	[TestMethod]
	public void Skill_Tier_No_RequiredLevel()
	{
		var skill = new Vengeance(Hero.CreatePaladin());
		Assert.AreEqual(0, skill.RequiredLevel);
		Assert.AreEqual(6, skill.Tier);
	}

	[TestMethod]
	public void Skill_Tier_RequiredLevel_31()
	{
		var skill = new Vengeance(Hero.CreatePaladin());
		skill.SetupLevel(2);
		Assert.AreEqual(31, skill.RequiredLevel);
		Assert.AreEqual(6, skill.Tier);
	}

	[TestMethod]
	public void Skill_Tier_RequiredLevel_32()
	{
		var skill = new Vengeance(Hero.CreatePaladin());
		skill.SetupLevel(3);
		Assert.AreEqual(32, skill.RequiredLevel);
		Assert.AreEqual(6, skill.Tier);
	}

	[TestMethod]
	public void Skill_Tier_RequiredLevel_60()
	{
		var skill = new Vengeance(Hero.CreatePaladin());
		skill.SetupLevel(31);
		Assert.AreEqual(60, skill.RequiredLevel);
		Assert.AreEqual(6, skill.Tier);
	}

	[TestMethod]
	[ExpectedException(typeof(ArgumentOutOfRangeException))]
	public void Skill_Setup_61_Throw_Exception()
	{
		var skill = new Vengeance(Hero.CreatePaladin());
		skill.SetupLevel(61);
	}

	public void Skill_Tier_RequiredLevel_89()
	{
		var skill = new Vengeance(Hero.CreatePaladin());
		skill.SetupLevel(60);
		Assert.AreEqual(89, skill.RequiredLevel);
		Assert.AreEqual(6, skill.Tier);
	}

	[TestMethod]
	public void Skill_Tier_RequiredLevel_30()
	{
		var skill = new Vengeance(Hero.CreatePaladin());
		skill.SetupLevel(1);
		Assert.AreEqual(30, skill.RequiredLevel);
		Assert.AreEqual(6, skill.Tier);
	}

	[TestMethod]
	public void Vengeance_CalculateBonusDmg_Level1_Returns70()
	{
		int result = Vengeance.CalculateBonusDmg(1);
		Assert.AreEqual(70, result);
	}

	[TestMethod]
	public void Vengeance_CalculateBonusDmg_Level2_Returns76()
	{
		int result = Vengeance.CalculateBonusDmg(2);
		Assert.AreEqual(76, result);
	}

	[TestMethod]
	public void Vengeance_CalculateBonusDmg_Level10_Returns124()
	{
		int result = Vengeance.CalculateBonusDmg(10);
		Assert.AreEqual(124, result);
	}

	[TestMethod]
	public void Vengeance_CalculateBonusDmg_Level60_Returns424()
	{
		int result = Vengeance.CalculateBonusDmg(60);
		Assert.AreEqual(424, result);
	}

	[TestMethod]
	public void Vengeance_Calculate_ManaCost()
	{
		double[] expected =
		{
			4.0, 4.3, 4.5, 4.8, 5.0, 5.3, 5.5, 5.8, 6.0, 6.3,
			6.5, 6.8, 7.0, 7.3, 7.5, 7.8, 8.0, 8.3, 8.5, 8.8,
			9.0, 9.3, 9.5, 9.8, 10.0, 10.3, 10.5, 10.8, 11.0, 11.3,
			11.5, 11.8, 12.0, 12.3, 12.5, 12.8, 13.0, 13.3, 13.5, 13.8,
			14.0, 14.3, 14.5, 14.8, 15.0, 15.3, 15.5, 15.8, 16.0, 16.3,
			16.5, 16.8, 17.0, 17.3, 17.5, 17.8, 18.0, 18.3, 18.5, 18.8
		};

		for (int i = 0; i < expected.Length; i++)
		{
			double actual = Vengeance.CalculateManaCost(i);
			Assert.AreEqual(expected[i], actual, 0.3, $"Mismatch at index {i}");
		}
	}

	[TestMethod]
	public void Vengeance_Set_Lvl_1()
	{
		var hero = Hero.CreatePaladin();
		var min = 5;
		var max = 10;
		var sword = new Weapon(new WeaponItemSpec(min, max)); // 6-12 with default
		var level = 1;
		hero.Gear.Equip(sword, _ => _.LeftHand);

		var vngns = new Vengeance(hero);
		vngns.SetupLevel(level);
		vngns.Set();

		Assert.IsTrue(vngns.CanUse());

		var expectedBonusPerc = Vengeance.CalculateBonusDmg(level) * 3; // cold + fire + light = 210%
		var expMin = (min + 1) * (expectedBonusPerc / 100.0) + (min + 1); // 18.6
		var expMax = (max + 2) * (expectedBonusPerc / 100.0) + (max + 2); // 37.2

		Assert.AreEqual((int)expMin, hero.Melee.Min, 2);
		Assert.AreEqual((int)expMax, hero.Melee.Max, 2);
	}

	[TestMethod]
	public void Vengeance_Set_Lvl_6()
	{
		var hero = Hero.CreatePaladin();
		var min = 5;
		var max = 10;
		var level = 6;
		var sword = new Weapon(new WeaponItemSpec(min, max)); // 6-12 with default
		hero.Gear.Equip(sword, _ => _.LeftHand);

		var vngns = new Vengeance(hero);
		vngns.SetupLevel(level);
		vngns.Set();

		Assert.IsTrue(vngns.CanUse());

		var expectedBonusPerc = Vengeance.CalculateBonusDmg(level) * 3; // cold + fire + light = 300%
		var expMin = (min + 1) * (expectedBonusPerc / 100.0) + (min + 1); // 24
		var expMax = (max + 2) * (expectedBonusPerc / 100.0) + (max + 2); // 48

		Assert.AreEqual((int)expMin, hero.Melee.Min, 2);
		Assert.AreEqual((int)expMax, hero.Melee.Max, 2);
	}

	[TestMethod]
	public void Vengeance_Set_Lvl_16()
	{
		var hero = Hero.CreatePaladin();
		var min = 5;
		var max = 10;
		var level = 16;
		var sword = new Weapon(new WeaponItemSpec(min, max)); // 6-12 with default
		hero.Gear.Equip(sword, _ => _.LeftHand);

		var vngns = new Vengeance(hero);
		vngns.SetupLevel(level);
		vngns.Set();

		Assert.IsTrue(vngns.CanUse());

		var expectedBonusPerc = Vengeance.CalculateBonusDmg(level) * 3; // cold + fire + light = 480%
		var expMin = (min + 1) * (expectedBonusPerc / 100.0) + (min + 1); 
		var expMax = (max + 2) * (expectedBonusPerc / 100.0) + (max + 2); 

		Assert.AreEqual((int)expMin, hero.Melee.Min, 2);
		Assert.AreEqual((int)expMax, hero.Melee.Max, 2);
	}

	[TestMethod]
	public void Vengeance_Set_Lvl_30()
	{
		var hero = Hero.CreatePaladin();
		var min = 5;
		var max = 10;
		var level = 30;
		var sword = new Weapon(new WeaponItemSpec(min, max)); // 6-12 with default
		hero.Gear.Equip(sword, _ => _.LeftHand);

		var vngns = new Vengeance(hero);
		vngns.SetupLevel(level);
		vngns.Set();

		Assert.IsTrue(vngns.CanUse());

		var expectedBonusPerc = Vengeance.CalculateBonusDmg(level) * 3; // cold + fire + light = 732%
		var expMin = (min + 1) * (expectedBonusPerc / 100.0) + (min + 1); 
		var expMax = (max + 2) * (expectedBonusPerc / 100.0) + (max + 2); 

		Assert.AreEqual((int)expMin, hero.Melee.Min, 2);
		Assert.AreEqual((int)expMax, hero.Melee.Max, 2);
	}

	[TestMethod]
	public void Combined_Vengeance_Lvl1_And_Aura_Lvl1()
	{
		var hero = Hero.CreatePaladin();
		var min = 5;
		var max = 10;
		var level = 1;
		var sword = new Weapon(new WeaponItemSpec(min, max)); // 6-12 with default
		hero.Gear.Equip(sword, _ => _.LeftHand);

		var vngns = new Vengeance(hero); // + 210% elemntal = +12 to 25 dmg in total
		var might = new Might(hero); // + 40% = +2 to 4 dmg
		
		vngns.SetupLevel(level);
		might.SetupLevel(level);

		hero.Skills.Additional.Add(vngns);
		hero.Skills.Additional.Add(might);

		hero.Skills.SetSecondary(_ => _.Skills.Additional.First(s => s == might));
		hero.Skills.SetPrimary(_ => _.Skills.Additional.First(s => s == vngns));

		Assert.AreEqual(vngns, hero.Skills.Primary);
		Assert.AreEqual(might, hero.Skills.Secondary);

		Assert.AreEqual(20, hero.Melee.Min, 2);
		Assert.AreEqual(40, hero.Melee.Max, 2);
	}

	[TestMethod]
	public void Combined_Vengeance_Lvl6_And_Aura_Lvl1()
	{
		var hero = Hero.CreatePaladin();
		var min = 5;
		var max = 10;
		var sword = new Weapon(new WeaponItemSpec(min, max)); // 6-12 with default
		hero.Gear.Equip(sword, _ => _.LeftHand);

		var vngns = new Vengeance(hero); 
		var might = new Might(hero); 

		vngns.SetupLevel(6);	// 300% = + 18 to 36
		might.SetupLevel(1);	// 40% = + 2 to 4 dmg

		hero.Skills.Additional.Add(vngns);
		hero.Skills.Additional.Add(might);

		hero.Skills.SetPrimary(_ => _.Skills.Additional.First(s => s == vngns));
		hero.Skills.SetSecondary(_ => _.Skills.Additional.First(s => s == might));

		Assert.AreEqual(vngns, hero.Skills.Primary);
		Assert.AreEqual(might, hero.Skills.Secondary);

		Assert.AreEqual(26, hero.Melee.Min, 2);
		Assert.AreEqual(52, hero.Melee.Max, 2);
	}

	[TestMethod]
	public void Combined_Vengeance_Lvl1_And_Aura_Lvl6()
	{
		var hero = Hero.CreatePaladin();
		var min = 5;
		var max = 10;
		var sword = new Weapon(new WeaponItemSpec(min, max)); // 6-12 with default
		hero.Gear.Equip(sword, _ => _.LeftHand);

		var vngns = new Vengeance(hero); 
		var might = new Might(hero); 

		vngns.SetupLevel(1);    // + 210% elemntal = +12 to 25 dmg in total
		might.SetupLevel(6);    // 90% = +5 to 10

		hero.Skills.Additional.Add(vngns);
		hero.Skills.Additional.Add(might);

		hero.Skills.SetPrimary(_ => _.Skills.Additional.First(s => s == vngns));
		hero.Skills.SetSecondary(_ => _.Skills.Additional.First(s => s == might));

		Assert.AreEqual(vngns, hero.Skills.Primary);
		Assert.AreEqual(might, hero.Skills.Secondary);

		Assert.AreEqual(23, hero.Melee.Min, 2);
		Assert.AreEqual(47, hero.Melee.Max, 2);
	}

	[TestMethod]
	public void Combined_Vengeance_Lvl6_And_Aura_Lvl6()
	{
		var hero = Hero.CreatePaladin();
		var min = 5;
		var max = 10;
		var sword = new Weapon(new WeaponItemSpec(min, max)); // 6-12 with default
		hero.Gear.Equip(sword, _ => _.LeftHand);

		var vngns = new Vengeance(hero);  
		var might = new Might(hero); 

		vngns.SetupLevel(6);    // 300% = + 18 to 36
		might.SetupLevel(6);    // 90 % = +5 to 10

		hero.Skills.Additional.Add(vngns);
		hero.Skills.Additional.Add(might);

		hero.Skills.SetPrimary(_ => _.Skills.Additional.First(s => s == vngns));
		hero.Skills.SetSecondary(_ => _.Skills.Additional.First(s => s == might));

		Assert.AreEqual(vngns, hero.Skills.Primary);
		Assert.AreEqual(might, hero.Skills.Secondary);

		Assert.AreEqual(29, hero.Melee.Min, 2);
		Assert.AreEqual(58, hero.Melee.Max, 2);
	}
}
