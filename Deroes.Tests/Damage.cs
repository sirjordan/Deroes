using Deroes.Core.Items;
using Deroes.Core.Items.Wearables;
using Deroes.Core.Stats;
using Deroes.Core.Stats.Modifiers;
using Deroes.Core.Units;

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
		var unit = Hero.CreatePaladin();

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
		var unit = Hero.CreatePaladin();
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
	public void ColdDamageRange_Apply_ShouldDealNoDamage_WhenImmune()
	{
		var unit = Hero.CreatePaladin(); // Cold immune
		var originalLife = unit.Life.Value.Remaining;

		var cold = new Cold(10, 20);
		int dmg = cold.Apply(unit);

		Assert.AreEqual(0, dmg);
		Assert.AreEqual(originalLife, unit.Life.Value.Remaining);
	}

	[TestMethod]
	public void CombinedDamage_ShouldDealReducedDamage()
	{
		var unit = Hero.CreatePaladin(); // 50% resistance + 5 defence
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

		Assert.AreEqual(originalLife - 9, unit.Life.Value.Remaining);
	}
}
