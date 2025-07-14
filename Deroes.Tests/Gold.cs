using Deroes.Core;
using Deroes.Core.Units;

namespace Deroes.Core.Tests;

[TestClass]
public class GoldTests
{
	private Hero _hero;
	private Hero _lvl13Hero;

	[TestInitialize]
	public void Setup()
	{
		_hero = Hero.CreatePaladin();

		_lvl13Hero = Hero.CreatePaladin();
		var xpToLevel13 = 57810;
		_lvl13Hero.AddExperience(xpToLevel13);
	}

	[TestMethod]
	public void CollectGold_IncreasesGold_WhenBelowMax()
	{
		_hero.Gold.Add(500);
		Assert.AreEqual(500, _hero.Gold.Amount);
	}

	[TestMethod]
	public void CollectGold_Throws_WhenNegativeGold()
	{
		Assert.ThrowsException<ArgumentOutOfRangeException>(() => _hero.Gold.Add(-10));
	}

	[TestMethod]
	public void CollectGold_DoesNotExceedMaxGold()
	{
		_hero.Gold.Add(_hero.Gold.Max + 5000);

		Assert.AreEqual(_hero.Gold.Max, _hero.Gold.Amount, "Gold should not exceed MaxGold after collecting too much.");
	}

	[TestMethod]
	public void DropGold_DecreasesGold()
	{
		_hero.Gold.Add(1005);
		var taken = _hero.Gold.Take(505);

		Assert.AreEqual(500, _hero.Gold.Amount);
		Assert.AreEqual(505, taken);
	}

	[TestMethod]
	public void DropGold_AllExtra_WhenExceedingMaxGold()
	{
		int tooMuch = _hero.Gold.Max + 1000;

		_hero.Gold.Add(tooMuch);

		Assert.AreEqual(_hero.Gold.Max, _hero.Gold.Amount, "Gold should be clamped to MaxGold after drop.");
	}

	[TestMethod]
	public void DropGold_DropsExactAmount()
	{
		_hero.Gold.Add(1000);
		_hero.Gold.Take(200);

		Assert.AreEqual(800, _hero.Gold.Amount, "Gold should decrease by drop amount.");
	}

	[TestMethod]
	public void Add_GoldAboveMax_ShouldDropExcess_lvl_13()
	{
		int max = _lvl13Hero.Gold.Max; 
		int toAdd = max + 5000;

		int dropped = _lvl13Hero.Gold.Add(toAdd);

		Assert.AreEqual(max, _lvl13Hero.Gold.Amount);
		Assert.AreEqual(5000, dropped);
	}

	[TestMethod]
	public void Take_MoreThanAmount_ShouldTakeAll()
	{
		_hero.Gold.Add(1000);

		int taken = _hero.Gold.Take(5000);

		Assert.AreEqual(1000, taken);
		Assert.AreEqual(0, _lvl13Hero.Gold.Amount);
	}

	[TestMethod]
	public void MaxGold_ShouldRespectHeroLevelAndCoefficient()
	{
		var gold = new Gold(_hero, maxGoldCoef: 2.5);

		int expectedMax = (int)(2.5 * _hero.Level * 10000);

		Assert.AreEqual(expectedMax, gold.Max);
	}
}
