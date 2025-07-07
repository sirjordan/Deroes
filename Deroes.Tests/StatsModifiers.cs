using Deroes.Core;
using Deroes.Core.Items;
using Deroes.Core.Stats.Modifiers;

namespace Deroes.Tests;

[TestClass]
public class StatsModifiers
{
	[TestMethod]
	public void ManaFlatModifier_IncreasesManaValueCorrectly()
	{
		// Arrange
		var hero = Hero.CreatePaladin();
		var startMana = hero.Mana.Value.Max;

		var modifier = new ManaFlatModifier(10);

		// Act
		modifier.ApplyModification(hero);
		var modifiedMana = hero.Mana.Value.Max;

		// Assert
		Assert.AreEqual(startMana + 10, modifiedMana);
	}

	[TestMethod]
	public void ManaFlatModifier_Removed_CorrectlyRestoresBaseValue()
	{
		// Arrange
		var hero = Hero.CreatePaladin();
		var startMana = hero.Mana.Value.Max;

		var modifier = new ManaFlatModifier(15);
		modifier.ApplyModification(hero);

		// Act
		modifier.RemoveModification(hero);
		var restoredMana = hero.Mana.Value.Max;

		// Assert
		Assert.AreEqual(startMana, restoredMana);
	}

	[TestMethod]
	public void MultipleModifiers_AreAppliedInCorrectOrder()
	{
		// Arrange
		var hero = Hero.CreatePaladin();
		var startMana = hero.Mana.Value.Max;

		var mod1 = new ManaFlatModifier(5);
		var mod2 = new ManaFlatModifier(10);

		// Act
		mod2.ApplyModification(hero);
		mod1.ApplyModification(hero);

		var modifiedMana = hero.Mana.Value.Max;

		// Assert
		Assert.AreEqual(startMana + 15, modifiedMana);
	}

	[TestMethod]
	public void Equip_HelmWithManaModifier_IncreasesHeroMana()
	{
		// Arrange
		var hero = Hero.CreatePaladin();
		var startMana = hero.Mana.Value.Max;

		var modifier = new ManaFlatModifier(10);
		var helm = new Helm(10, 10, 1,[modifier]);

		// Act
		bool equipped = hero.Gear.Equip(helm, g => g.Helm);
		var modifiedMana = hero.Mana.Value.Max;

		// Assert
		Assert.IsTrue(equipped);
		Assert.AreEqual(startMana + 10, modifiedMana);
	}

	[TestMethod]
	public void Equip_IncompatibleHelm_DoesNotApplyModifier()
	{
		// Arrange
		var hero = Hero.CreatePaladin();
		var startMana = hero.Mana.Value.Max;

		var modifier = new ManaFlatModifier(20);
		// Requirements too high
		var helm = new Helm(999, 999, 999, [modifier]);

		// Act
		bool equipped = hero.Gear.Equip(helm, g => g.Helm);

		// Assert
		Assert.IsFalse(equipped);
		Assert.AreEqual(startMana, hero.Mana.Value.Max);
	}

	[TestMethod]
	public void Unequip_Helm_RemovesModifierCorrectly()
	{
		// Arrange
		var hero = Hero.CreatePaladin();
		var modifier = new ManaFlatModifier(15);
		var helm = new Helm(10, 10, 1, [modifier]);

		hero.Gear.Equip(helm, g => g.Helm);
		var manaWithHelm = hero.Mana.Value.Max;

		// Act
		var unequippedItem = hero.Gear.Unequip(g => g.Helm);
		var manaAfterUnequip = hero.Mana.Value.Max;

		// Assert
		Assert.AreEqual(helm, unequippedItem);
		Assert.AreEqual(manaWithHelm - 15, manaAfterUnequip);
	}

	[TestMethod]
	[ExpectedException(typeof(InvalidOperationException))]
	public void Equip_SameSlotTwice_ThrowsException()
	{
		// Arrange
		var hero = Hero.CreatePaladin();
		var helm1 = new Helm(10, 10, 1,[]);
		var helm2 = new Helm(10, 10, 1, []);

		// Act
		hero.Gear.Equip(helm1, g => g.Helm);
		hero.Gear.Equip(helm2, g => g.Helm); // Should throw
	}
}
