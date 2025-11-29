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
		var m = new Monster(0, DropChance.Always());
	}

	[TestMethod]
	[ExpectedException(typeof(ArgumentOutOfRangeException))]
	public void GetGoldDrop_ShouldThrow_WhenLevelIsNegative()
	{
		var m = new Monster(-5, DropChance.Always());
	}

	[TestMethod]
	[ExpectedException(typeof(ArgumentOutOfRangeException))]
	public void GetGoldDrop_ShouldThrow_WhenLevelIs_Over_Max()
	{
		var m = new Monster(86, DropChance.Always());
	}

	[TestMethod]
	public void GetGoldDrop_Level5_ShouldBeInExpectedRange()
	{
		var m = new Monster(5, DropChance.Always());
		int gold = m.DropGold();
		Assert.IsTrue(gold >= 5 && gold <= 50, $"Gold: {gold}");
	}

	[TestMethod]
	public void GetGoldDrop_Level15_ShouldBeInExpectedRange()
	{
		var m = new Monster(15, DropChance.Always());
		int gold = m.DropGold();
		Assert.IsTrue(gold >= 20 && gold <= 100, $"Gold: {gold}");
	}

	[TestMethod]
	public void GetGoldDrop_Level25_ShouldBeInExpectedRange()
	{
		var m = new Monster(25, DropChance.Always());
		int gold = m.DropGold();
		Assert.IsTrue(gold >= 40 && gold <= 200, $"Gold: {gold}");
	}

	[TestMethod]
	public void GetGoldDrop_Level35_ShouldBeInExpectedRange()
	{
		var m = new Monster(35, DropChance.Always());
		int gold = m.DropGold();
		Assert.IsTrue(gold >= 100 && gold <= 400, $"Gold: {gold}");
	}

	[TestMethod]
	public void GetGoldDrop_Level45_ShouldBeInExpectedRange()
	{
		var m = new Monster(45, DropChance.Always());
		int gold = m.DropGold();
		Assert.IsTrue(gold >= 200 && gold <= 850, $"Gold: {gold}");
	}

	[TestMethod]
	public void GetGoldDrop_Level55_ShouldBeInExpectedRange()
	{
		var m = new Monster(55, DropChance.Always());
		int gold = m.DropGold();
		Assert.IsTrue(gold >= 400 && gold <= 1700, $"Gold: {gold}");
	}

	[TestMethod]
	public void GetGoldDrop_Level65_ShouldBeInExpectedRange()
	{
		var m = new Monster(65, DropChance.Always());
		int gold = m.DropGold();
		Assert.IsTrue(gold >= 800 && gold <= 3600, $"Gold: {gold}");
	}

	[TestMethod]
	public void GetGoldDrop_Level78_ShouldBeInExpectedRange()
	{
		var m = new Monster(78, DropChance.Always());
		int gold = m.DropGold();
		Assert.IsTrue(gold >= 1300 && gold <= 6400, $"Gold: {gold}");
	}

	[TestMethod]
	public void DropChance_Roll_ZeroPercent_ShouldAlwaysBeFalse()
	{
		for (int i = 0; i < 100; i++)
		{
			Assert.IsFalse(DropChance.Never().Roll());
		}
	}

	[TestMethod]
	public void DropChance_Roll_HundredPercent_ShouldAlwaysBeTrue()
	{
		for (int i = 0; i < 100; i++)
		{
			Assert.IsTrue(DropChance.Always().Roll());
		}
	}

	[TestMethod]
	public void Roll_FiftyPercent_ShouldBeNearHalf_Statistically()
	{
		int attempts = 5000;
		int hits = 0;

		for (int i = 0; i < attempts; i++)
		{
			if (DropChance.Explicit(50).Roll()) 
				hits++;
		}

		double ratio = hits / (double)attempts;

		// Expect results roughly between 40–60%
		Assert.IsTrue(ratio > 0.40 && ratio < 0.60,
			$"Expected hit ratio around 0.5, but got {ratio}");
	}

	[DataTestMethod]
	[DataRow(0, 0)]
	[DataRow(50, 50)]
	[DataRow(100, 100)]
	public void FromPercentage_ValidValues_ShouldSetPercentage(int input, int expected)
	{
		var dc = DropChance.Explicit(input);

		Assert.AreEqual(expected, dc.Chance);
	}

	[DataTestMethod]
	[DataRow(-1, 0)]
	[DataRow(-50, 0)]
	[DataRow(101, 100)]
	[DataRow(999, 100)]
	public void FromPercentage_OutOfRange_ShouldClamp(int input, int expected)
	{
		var dc = DropChance.Explicit(input);

		Assert.AreEqual(expected, dc.Chance);
	}

	[TestMethod]
	public void Always_ShouldReturnPercentage100()
	{
		var dc = DropChance.Always();

		Assert.AreEqual(100, dc.Chance);
	}

	[TestMethod]
	public void Never_ShouldReturnPercentage0()
	{
		var dc = DropChance.Never();

		Assert.AreEqual(0, dc.Chance);
	}

	[TestMethod]
	public void Percentage_ShouldRemainConstantAfterCreation()
	{
		var dc = DropChance.Always();

		// Ensures the property does not change since the setter is private
		Assert.AreEqual(100, dc.Chance);
	}
}
