using Deroes.Core.Stats;
using Deroes.Core.Units;

namespace Deroes.Core.Tests;

[TestClass]
public class AttributesTests
{
	[TestMethod]
	public void OnLevelUp_IncreasesStatPointsAvailableBy5()
	{
		var hero = new Paladin();
		var attributes = new Attributes(hero, 10, 10, 10, 10);

		attributes.OnLevelUp();

		Assert.AreEqual(5, attributes.StatPointsAvailable);
	}

	[TestMethod]
	public void AddStrength_IncreasesStrengthPoints_WhenStatPointsAvailable()
	{
		var hero = new Paladin();
		var attributes = new Attributes(hero, 5, 5, 5, 5);
		attributes.OnLevelUp();

		attributes.AddStrenght();

		Assert.AreEqual(5 + 1, attributes.Strength.Points);
		Assert.AreEqual(4, attributes.StatPointsAvailable);
	}

	[TestMethod]
	public void AddDexterity_IncreasesDexterityPoints_WhenStatPointsAvailable()
	{
		var hero = new Paladin();
		var attributes = new Attributes(hero, 5, 5, 5, 5);
		attributes.OnLevelUp();

		attributes.AddDexterity();

		Assert.AreEqual(6, attributes.Dexterity.Points);
		Assert.AreEqual(4, attributes.StatPointsAvailable);
	}

	[TestMethod]
	public void AddVitality_IncreasesVitalityPoints_AndCallsHeroMethods()
	{
		var hero_lvl2 = new Paladin();
		hero_lvl2.AddExperience(Hero.XpToLevelUp(1));

		var initial = hero_lvl2.Attributes.Vitality.Points;

		hero_lvl2.Attributes.AddVitality();

		Assert.AreEqual(initial + 1, hero_lvl2.Attributes.Vitality.Points);
		Assert.AreEqual(4, hero_lvl2.Attributes.StatPointsAvailable);
	}

	[TestMethod]
	public void AddEnergy_IncreasesEnergyPoints_AndCallsHeroMana()
	{
		var hero_lvl2 = new Paladin();
		hero_lvl2.AddExperience(Hero.XpToLevelUp(1));

		var initial = hero_lvl2.Attributes.Energy.Points;

		hero_lvl2.Attributes.AddEnergy();

		Assert.AreEqual(initial + 1, hero_lvl2.Attributes.Energy.Points);
		Assert.AreEqual(4, hero_lvl2.Attributes.StatPointsAvailable);
	}

	[TestMethod]
	[ExpectedException(typeof(InvalidOperationException))]
	public void AddStrength_Throws_WhenNoStatPointsAvailable()
	{
		var hero = new Paladin();
		var attributes = new Attributes(hero, 5, 5, 5, 5);

		attributes.AddStrenght();
	}
}
