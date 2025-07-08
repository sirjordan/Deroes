using Deroes.Core;
using Deroes.Core.Stats;

namespace Deroes.Tests;

[TestClass]
public class DamageTests
{
	[TestMethod]
	public void Damage_CannotBeNegative()
	{
		Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Damage(-1));
	}

	[TestMethod]
	public void Modify_ReturnsNewInstance_WithModifiedAmount()
	{
		var dmg = new Damage(10);
		var modified = dmg.Modify(5);

		Assert.AreEqual(10, dmg.Amount); // original is unchanged
		Assert.AreEqual(15, modified.Amount);
	}

	[TestMethod]
	public void PhysicalDamageRange_Apply_ShouldDealMin1Damage_WhenResistanceTooHigh()
	{
		// Unit_with_a_lot_physical_resistance - 9999
		var unit = Hero.CreatePaladin();
		var originalLife = unit.Life.Value.Remaining;

		var physical = new Physical(1, 2);
		int dmg = physical.Apply(unit);

		Assert.AreEqual(1, dmg);
		Assert.AreEqual(originalLife - 1, unit.Life.Value.Remaining);
	}

	[TestMethod]
	public void PhysicalDamageRange_Apply_ShouldDealReducedDamage_WhenResistsPresent()
	{
		// Unit with 1 defense
		var unit = Hero.CreatePaladin();
		var originalLife = unit.Life.Value.Remaining;

		var physical = new Physical(10, 10);
		int dmg = physical.Apply(unit);

		Assert.AreEqual(9, dmg);
		Assert.AreEqual(originalLife - 9, unit.Life.Value.Remaining);
	}

	[TestMethod]
	public void PhysicalDamageRange_Apply_ShouldDealNoDamage_WhenImmune()
	{
		// Unit with phisical immune
		var unit = Hero.CreatePaladin();
		var originalLife = unit.Life.Value.Remaining;

		var physical = new Physical(10, 15);
		int dmg = physical.Apply(unit);

		Assert.AreEqual(0, dmg);
		Assert.AreEqual(originalLife, unit.Life.Value.Remaining);
	}

	[TestMethod]
	public void ColdDamageRange_Apply_ShouldDealPercentageReducedDamage()
	{
		var unit = Hero.CreatePaladin(); // 50% resistance
		var originalLife = unit.Life.Value.Remaining;

		var cold = new Cold(10, 10);
		int dmg = cold.Apply(unit);

		Assert.AreEqual(5, dmg);
		Assert.AreEqual(originalLife - 5, unit.Life.Value.Remaining);
	}

	[TestMethod]
	public void ColdDamageRange_Apply_ShouldDealNoDamage_WhenImmune()
	{
		var unit = Hero.CreatePaladin(); // Cold immune
		var originalLife = unit.Life.Value.Remaining;

		var cold = new Cold(10, 20);
		int dmg = cold.Apply(unit);

		Assert.AreEqual(0, dmg);
		Assert.AreEqual(originalLife, unit.Life.Value.Remaining);
	}
}
