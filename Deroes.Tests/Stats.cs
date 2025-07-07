using Deroes.Core.Stats;
using Deroes.Core.Stats.Modifiers;

namespace Deroes.Tests;

[TestClass]
public class Stats
{
	[TestMethod]
	public void OnAction_AddsPoints_WhenWithinBounds()
	{
		var life = new Vital(100, 0, 0);
		life.OnAction(-20);  // simulate damage
		life.OnAction(10);   // heal a bit

		Assert.AreEqual(90, life.Remaining);
	}

	[TestMethod]
	public void OnAction_DoesNotExceedMax()
	{
		var life = new Vital(100, 0, 0);
		life.OnAction(50);  // full life + 50 = should clamp to 100

		Assert.AreEqual(100, life.Remaining);
	}

	[TestMethod]
	public void OnAction_DoesNotGoBelowZero()
	{
		var life = new Vital(100, 0, 0);
		life.OnAction(-150);  // damage beyond zero

		Assert.AreEqual(0, life.Remaining);
	}

	[TestMethod]
	public void OnAction_ExactZero()
	{
		var life = new Vital(100, 0, 0);
		life.OnAction(-100);

		Assert.AreEqual(0, life.Remaining);
	}

	[TestMethod]
	public void OnAction_ExactMax()
	{
		var life = new Vital(100, 0, 0);
		life.OnAction(-50);
		life.OnAction(50); // should go back to max

		Assert.AreEqual(100, life.Remaining);
	}

	[TestMethod]
	public void OnAction_SmallFractionalPoints()
	{
		var life = new Vital(100, 0, 0);
		life.OnAction(-0.5);
		life.OnAction(0.25);

		Assert.AreEqual(99.75, life.Remaining, 0.0001);
	}

	[TestMethod]
	public void Stat_Should_Return_BaseValue_If_No_Modifiers()
	{
		var baseVital = new Vital(100, 10, 5);
		var stat = new Stat<Vital>(baseVital);

		Assert.AreEqual(100, stat.Value.Max);
	}

	[TestMethod]
	public void Stat_Should_Apply_Additive_Modifier()
	{
		var baseVital = new Vital(100, 10, 5);
		var stat = new Stat<Vital>(baseVital);

		stat.AddModifier(new ManaFlatModifier(25));
		Assert.AreEqual(125, stat.Value.Max);
	}

	[TestMethod]
	public void Stat_Should_Apply_Multiple_Additive_Modifier()
	{
		var baseVital = new Vital(100, 10, 5);
		var stat = new Stat<Vital>(baseVital);

		stat.AddModifier(new ManaFlatModifier(20));
		stat.AddModifier(new ManaFlatModifier(25));
		Assert.AreEqual(145, stat.Value.Max);
	}

	[TestMethod]
	public void Stat_Should_Apply_Percentage_Modifier()
	{
		var baseVital = new Vital(100, 10, 5);
		var stat = new Stat<Vital>(baseVital);

		stat.AddModifier(new ManaPercentageModifier(10));
		Assert.AreEqual(110, stat.Value.Max); // 100 + 10%
	}

	[TestMethod]
	public void Stat_Should_Apply_Combined_Modifiers()
	{
		var baseVital = new Vital(100, 10, 5);
		var stat = new Stat<Vital>(baseVital);

		stat.AddModifier(new ManaFlatModifier(20));
		stat.AddModifier(new ManaPercentageModifier(10)); // +10%

		Assert.AreEqual(132, stat.Value.Max); // 100 + 20 + 10%(120)
	}

	[TestMethod]
	public void Stat_Should_Apply_Combined_Modifiers_InCorrect_Order()
	{
		var baseVital = new Vital(100, 10, 5);
		var stat = new Stat<Vital>(baseVital);

		stat.AddModifier(new ManaPercentageModifier(10)); // +10%
		stat.AddModifier(new ManaFlatModifier(20));
		
		// Percentage should be last
		Assert.AreEqual(132, stat.Value.Max); // 100 + 20 + 10%(120)
	}

	[TestMethod]
	public void Stat_Should_Remove_Modifier_Correctly()
	{
		var baseVital = new Vital(100, 10, 5);
		var modifier = new ManaFlatModifier(50);
		var stat = new Stat<Vital>(baseVital);

		stat.AddModifier(modifier);
		Assert.AreEqual(150, stat.Value.Max);

		stat.RemoveModifier(modifier);
		Assert.AreEqual(100, stat.Value.Max);
	}
}

