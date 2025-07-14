using Deroes.Core;
using Deroes.Core.Units;

namespace Deroes.Core.Tests;

[TestClass]
public class Drops
{
	[TestMethod]
	[ExpectedException(typeof(ArgumentOutOfRangeException))]
	public void GetGoldDrop_ShouldThrow_WhenLevelIsZero()
	{
		var m = new Monster(0);
	}

	[TestMethod]
	[ExpectedException(typeof(ArgumentOutOfRangeException))]
	public void GetGoldDrop_ShouldThrow_WhenLevelIsNegative()
	{
		var m = new Monster(-5);
	}

	[TestMethod]
	[ExpectedException(typeof(ArgumentOutOfRangeException))]
	public void GetGoldDrop_ShouldThrow_WhenLevelIs_Over_Max()
	{
		var m = new Monster(86);
	}

	[TestMethod]
	public void GetGoldDrop_Level5_ShouldBeInExpectedRange()
	{
		var m = new Monster(5);
		int gold = m.DropGold();
		Assert.IsTrue(gold >= 5 && gold <= 50, $"Gold: {gold}");
	}

	[TestMethod]
	public void GetGoldDrop_Level15_ShouldBeInExpectedRange()
	{
		var m = new Monster(15);
		int gold = m.DropGold();
		Assert.IsTrue(gold >= 20 && gold <= 100, $"Gold: {gold}");
	}

	[TestMethod]
	public void GetGoldDrop_Level25_ShouldBeInExpectedRange()
	{
		var m = new Monster(25);
		int gold = m.DropGold();
		Assert.IsTrue(gold >= 40 && gold <= 200, $"Gold: {gold}");
	}

	[TestMethod]
	public void GetGoldDrop_Level35_ShouldBeInExpectedRange()
	{
		var m = new Monster(35);
		int gold = m.DropGold();
		Assert.IsTrue(gold >= 100 && gold <= 400, $"Gold: {gold}");
	}

	[TestMethod]
	public void GetGoldDrop_Level45_ShouldBeInExpectedRange()
	{
		var m = new Monster(45);
		int gold = m.DropGold();
		Assert.IsTrue(gold >= 200 && gold <= 850, $"Gold: {gold}");
	}

	[TestMethod]
	public void GetGoldDrop_Level55_ShouldBeInExpectedRange()
	{
		var m = new Monster(55);
		int gold = m.DropGold();
		Assert.IsTrue(gold >= 400 && gold <= 1700, $"Gold: {gold}");
	}

	[TestMethod]
	public void GetGoldDrop_Level65_ShouldBeInExpectedRange()
	{
		var m = new Monster(65);
		int gold = m.DropGold();
		Assert.IsTrue(gold >= 800 && gold <= 3600, $"Gold: {gold}");
	}

	[TestMethod]
	public void GetGoldDrop_Level78_ShouldBeInExpectedRange()
	{
		var m = new Monster(78);
		int gold = m.DropGold();
		Assert.IsTrue(gold >= 1300 && gold <= 6400, $"Gold: {gold}");
	}
}
