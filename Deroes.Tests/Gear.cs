using Deroes.Core.Items;
using Deroes.Core.Items.Wearables;
using Deroes.Core.Stats.Modifiers;
using Deroes.Core.Units;
using Moq;

namespace Deroes.Core.Tests;

[TestClass]
public class Gear
{
	private Hero _hero;

	[TestInitialize]
	public void Setup()
	{
		_hero = Hero.CreatePaladin();
	}

	[TestMethod]
	public void Equip_ValidHelm_ShouldEquip()
	{
		var helm = new Helm(new DefenseItemSpec(1));

		var success = _hero.Gear.Equip(helm, g => g.Helm);

		Assert.IsTrue(success);
		Assert.AreEqual(helm, _hero.Gear.Helm);
	}

	[TestMethod]
	public void Equip_ValidHelm_Should_NOT_Equip_Strength()
	{
		var itemSpec = new DefenseItemSpec(1)
		{
			RequiredStrength = _hero.Attributes.Strength.Points + 1
		};
		var helm = new Helm(itemSpec);

		var success = _hero.Gear.Equip(helm, g => g.Helm);

		Assert.IsFalse(success);
		Assert.AreNotEqual(helm, _hero.Gear.Helm);
		Assert.IsNull(_hero.Gear.Helm);
	}

	[TestMethod]
	[ExpectedException(typeof(InvalidOperationException))]
	public void Equip_AlreadyEquippedSlot_ShouldThrow()
	{
		var helm1 = new Helm(new DefenseItemSpec(1));
		var helm2 = new Helm(new DefenseItemSpec(1));

		_hero.Gear.Equip(helm1, g => g.Helm);
		_hero.Gear.Equip(helm2, g => g.Helm); // Should throw
	}

	[TestMethod]
	public void Unequip_ShouldReturnCorrectItemAndRemoveModifiers()
	{
		var modifier = new Mock<IStatModifier>();
		modifier.Setup(m => m.ApplyModification(_hero)).Verifiable();
		modifier.Setup(m => m.RemoveModification(_hero)).Verifiable();

		var helm = new Helm(new DefenseItemSpec(1) { Modifiers = [modifier.Object] });
		
		_hero.Gear.Equip(helm, g => g.Helm);

		var unequipped = _hero.Gear.Unequip(g => g.Helm);

		Assert.AreEqual(helm, unequipped);
		Assert.IsNull(_hero.Gear.Helm);

		modifier.Verify(m => m.ApplyModification(_hero), Times.Once);
		modifier.Verify(m => m.RemoveModification(_hero), Times.Once);
	}


}
