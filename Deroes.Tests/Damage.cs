using Deroes.Core.Items.Wearables;
using Deroes.Core.Stats;
using Deroes.Core.Stats.Modifiers;
using Deroes.Core.Units;

namespace Deroes.Core.Tests;

[TestClass]
public class DamageTests
{
	private Hero unit;

	[TestInitialize]
	public void Setup()
	{
		unit = new Hero("", new PaladinSetup());
	}

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
		var armor = new Armor(new DefenseItemSpec(999));

		unit.Gear.Equip(armor, _ => _.Armor);

		var originalLife = unit.Life.Value.Remaining;

		var physical = new Physical(9, 10);
		int dmg = physical.Apply(unit);

		Assert.AreEqual(1, dmg);
		Assert.AreEqual(originalLife - 1, unit.Life.Value.Remaining);
	}

	[TestMethod]
	public void PhysicalDamageRange_Apply_ShouldDealReducedDamage_WhenResistsPresent()
	{
		var armor = new Armor(new DefenseItemSpec(5));
		unit.Gear.Equip(armor, _ => _.Armor);

		var originalLife = unit.Life.Value.Remaining;

		var physical = new Physical(10, 10);
		int dmg = physical.Apply(unit);

		Assert.AreEqual(5, dmg);
		Assert.AreEqual(originalLife - 5, unit.Life.Value.Remaining);
	}

	[TestMethod]
	public void PhysicalDamageRange_Apply_ShouldDealNoDamage_WhenImmune()
	{
		// Unit with phisical immune
		var unit = new Monster(1);
		var originalLife = unit.Life.Value.Remaining;

		var physical = new Physical(10, 15);
		int dmg = physical.Apply(unit);

		Assert.AreEqual(0, dmg);
		Assert.AreEqual(originalLife, unit.Life.Value.Remaining);
	}

	[TestMethod]
	public void ColdDamageRange_Apply_ShouldDealPercentageReducedDamage()
	{
		// 50% resistance
		var armor = new Armor(new DefenseItemSpec(1)
		{
			Modifiers = [new ColdResistModifier(50)]
		});
		unit.Gear.Equip(armor, _ => _.Armor);

		var originalLife = unit.Life.Value.Remaining;

		var cold = new Cold(10, 10);
		int dmg = cold.Apply(unit);

		Assert.AreEqual(5, dmg);
		Assert.AreEqual(originalLife - 5, unit.Life.Value.Remaining);
	}

	[TestMethod]
	public void ColdDamageRange_MAX_Resist_ShouldDealPercentageReducedDamage()
	{
		var armor = new Armor(new DefenseItemSpec(1)
		{
			Modifiers = [new ColdResistModifier(100)]   // Should be max 75%
		});
		unit.Gear.Equip(armor, _ => _.Armor);

		var originalLife = unit.Life.Value.Remaining;

		var cold = new Cold(10, 10);
		int dmg = cold.Apply(unit);

		Assert.AreEqual(2, dmg);
		Assert.AreEqual(originalLife - 2, unit.Life.Value.Remaining);
	}

	[TestMethod]
	public void ColdDamageRange_Negative_Resist_ShouldDeal_Increased_Damage()
	{
		var armor = new Armor(new DefenseItemSpec(1)
		{
			Modifiers = [new ColdResistModifier(-50)]  
		});
		unit.Gear.Equip(armor, _ => _.Armor);

		var originalLife = unit.Life.Value.Remaining;

		var cold = new Cold(10, 10);
		int dmg = cold.Apply(unit);

		Assert.AreEqual(15, dmg);
		Assert.AreEqual(originalLife - 15, unit.Life.Value.Remaining);
	}

	[TestMethod]
	public void ColdDamageRange_Apply_ShouldDealNoDamage_WhenImmune()
	{
		var unit = new Monster(1); // Cold immune
		var originalLife = unit.Life.Value.Remaining;

		var cold = new Cold(10, 20);
		int dmg = cold.Apply(unit);

		Assert.AreEqual(0, dmg);
		Assert.AreEqual(originalLife, unit.Life.Value.Remaining);
	}

	[TestMethod]
	public void CombinedDamage_ShouldDealReducedDamage()
	{
		// 50% resistance + 7 defence
		var armor = new Armor(new DefenseItemSpec(1)
		{
			Modifiers = [
				new ColdResistModifier(50),
				new PhysicalResistModifier(6)
				]
		});
		unit.Gear.Equip(armor, _ => _.Armor);

		var originalLife = unit.Life.Value.Remaining;

		var cold = new Cold(10, 10);
		int dmg = cold.Apply(unit);

		var ph = new Physical(10, 10);
		ph.Apply(unit);

		Assert.AreEqual(originalLife - 8, unit.Life.Value.Remaining);
	}
}
