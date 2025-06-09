using Deroes.Core;

namespace Deroes.Tests;

[TestClass]
public class Stats
{
	[TestMethod]
	public void OnAction_AddsPoints_WhenWithinBounds()
	{
		var life = new Stat(100, 0, 0);
		life.OnAction(-20);  // simulate damage
		life.OnAction(10);   // heal a bit

		Assert.AreEqual(90, life.Remaining);
	}

	[TestMethod]
	public void OnAction_DoesNotExceedMax()
	{
		var life = new Stat(100, 0, 0);
		life.OnAction(50);  // full life + 50 = should clamp to 100

		Assert.AreEqual(100, life.Remaining);
	}

	[TestMethod]
	public void OnAction_DoesNotGoBelowZero()
	{
		var life = new Stat(100, 0, 0);
		life.OnAction(-150);  // damage beyond zero

		Assert.AreEqual(0, life.Remaining);
	}

	[TestMethod]
	public void OnAction_ExactZero()
	{
		var life = new Stat(100, 0, 0);
		life.OnAction(-100);

		Assert.AreEqual(0, life.Remaining);
	}

	[TestMethod]
	public void OnAction_ExactMax()
	{
		var life = new Stat(100, 0, 0);
		life.OnAction(-50);
		life.OnAction(50); // should go back to max

		Assert.AreEqual(100, life.Remaining);
	}

	[TestMethod]
	public void OnAction_SmallFractionalPoints()
	{
		var life = new Stat(100, 0, 0);
		life.OnAction(-0.5);
		life.OnAction(0.25);

		Assert.AreEqual(99.75, life.Remaining, 0.0001);
	}
}
