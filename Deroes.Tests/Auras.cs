using Deroes.Core.Units;

namespace Deroes.Core.Tests;

[TestClass]
public class Auras
{
	[TestMethod]
	public void Resist_Fire_Level1_ShouldBeAround60()
	{
		double resist = ResistFire.CalculateBonusResist(1);

		Assert.IsTrue(resist >= 59 && resist <= 61, $"Expected ~60%, got {resist}");
	}

	[TestMethod]
	public void Resist_Fire_Activate_Level1_ShouldBeAround60_On_Hero()
	{
		var hero = Hero.CreatePaladin();
		Assert.IsTrue(hero.Resistanse.Fire.Value.Amount == 0);

		var resistFire = new ResistFire(1);
		resistFire.Activate(hero);

		Assert.IsTrue(hero.Resistanse.Fire.Value.Amount >= 59 && hero.Resistanse.Fire.Value.Amount <= 61, $"Expected ~60%, got {hero.Resistanse.Fire.Value.Amount}");

		resistFire.Deactivate(hero);
		Assert.IsTrue(hero.Resistanse.Fire.Value.Amount == 0);
	}

	[TestMethod]
	public void Resist_Fire_Activate_Level5_ShouldBeAround85_On_Hero()
	{
		var hero = Hero.CreatePaladin();
		Assert.IsTrue(hero.Resistanse.Fire.Value.Amount == 0);

		var resistFire = new ResistFire(5); // ~85 but max is 75
		resistFire.Activate(hero);

		Assert.IsTrue(hero.Resistanse.Fire.Value.Amount == 75, $"Expected 75%, got {hero.Resistanse.Fire.Value.Amount}");

		resistFire.Deactivate(hero);
		Assert.IsTrue(hero.Resistanse.Fire.Value.Amount == 0);
	}

	[TestMethod]
	public void Resist_Fire_Level5_ShouldBeAround85()
	{
		double resist = ResistFire.CalculateBonusResist(5);

		Assert.IsTrue(resist >= 84 && resist <= 86, $"Expected ~85%, got {resist}");
	}

	[TestMethod]
	public void Resist_Fire_Level10_ShouldBeAround106()
	{
		double resist = ResistFire.CalculateBonusResist(10);

		Assert.IsTrue(resist >= 105 && resist <= 107, $"Expected ~106%, got {resist}");
	}

	[TestMethod]
	public void Resist_Fire_Level30_ShouldBeAround141()
	{
		double resist = ResistFire.CalculateBonusResist(30);

		Assert.IsTrue(resist >= 140 && resist <= 142, $"Expected ~141%, got {resist}");
	}

	[TestMethod]
	public void Resist_Fire_Level60_ShouldBeCloseTo150()
	{
		double resist = ResistFire.CalculateBonusResist(60);

		Assert.IsTrue(resist >= 149 && resist <= 150, $"Expected ~150%, got {resist}");
	}

	[TestMethod]
	[ExpectedException(typeof(ArgumentOutOfRangeException))]
	public void Resist_Fire_LevelZero_ShouldThrow()
	{
		ResistFire.CalculateBonusResist(0);
	}

	[TestMethod]
	public void Resist_Fire_IncreasesMonotonically()
	{
		double previous = ResistFire.CalculateBonusResist(1);

		for (int level = 2; level <= 60; level++)
		{
			double current = ResistFire.CalculateBonusResist(level);
			Assert.IsTrue(current >= previous, $"Resistance should not decrease at level {level}.");
			previous = current;
		}
	}

	[TestMethod]
	public void Resist_Fire_Max_Resist_LVL_1()
	{
		Assert.AreEqual(1, ResistFire.CalculateBonusMaxResist(1));
	}

	[TestMethod]
	public void Resist_Fire_Max_Resist_LVL_5()
	{
		Assert.AreEqual(5, ResistFire.CalculateBonusMaxResist(5));
	}

	[TestMethod]
	public void Resist_Fire_Max_Resist_LVL_10()
	{
		Assert.AreEqual(10, ResistFire.CalculateBonusMaxResist(10));
	}

	[TestMethod]
	public void Resist_Fire_Max_Resist_LVL_20()
	{
		Assert.AreEqual(20, ResistFire.CalculateBonusMaxResist(20));
	}

	[TestMethod]
	public void Resist_Fire_Max_Resist_LVL_25()
	{
		Assert.AreEqual(20, ResistFire.CalculateBonusMaxResist(25));
	}
}
