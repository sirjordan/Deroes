using Deroes.Core;

namespace Deroes.Tests;

[TestClass]
public class Gold
{
	private Hero _hero;

	[TestInitialize]
	public void Setup()
	{
		_hero = Hero.CreatePaladin(); // Or however you create your hero
	}

	[TestMethod]
	public void CollectGold_IncreasesGold_WhenBelowMax()
	{
		_hero.CollectGold(500);
		Assert.AreEqual(500, _hero.Gold);
	}

	[TestMethod]
	public void CollectGold_Throws_WhenNegativeGold()
	{
		Assert.ThrowsException<ArgumentOutOfRangeException>(() => _hero.CollectGold(-10));
	}

	[TestMethod]
	public void CollectGold_DoesNotExceedMaxGold()
	{
		_hero.CollectGold(_hero.MaxGold + 5000);

		Assert.AreEqual(_hero.MaxGold, _hero.Gold, "Gold should not exceed MaxGold after collecting too much.");
	}

	[TestMethod]
	public void DropGold_DecreasesGold()
	{
		_hero.CollectGold(1000);
		_hero.DropGold(500);

		Assert.AreEqual(500, _hero.Gold);
	}

	[TestMethod]
	public void DropGold_AllExtra_WhenExceedingMaxGold()
	{
		int tooMuch = _hero.MaxGold + 1000;

		_hero.CollectGold(tooMuch);

		Assert.AreEqual(_hero.MaxGold, _hero.Gold, "Gold should be clamped to MaxGold after drop.");
	}

	[TestMethod]
	public void DropGold_DropsExactAmount()
	{
		_hero.CollectGold(1000);
		_hero.DropGold(200);

		Assert.AreEqual(800, _hero.Gold, "Gold should decrease by drop amount.");
	}
}
