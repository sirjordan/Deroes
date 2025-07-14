using Deroes.Core.Items;
using Deroes.Core.Units;

namespace Deroes.Core.Tests;

[TestClass]
public class Potions
{
	private Hero _paladin;

	[TestInitialize]
	public void Setup()
	{
		_paladin = Hero.CreatePaladin();

		var xpToLevel26 = 538100;
		// Act
		_paladin.AddExperience(xpToLevel26);

		_paladin.Attributes.AddVitality();
		_paladin.Attributes.AddVitality();
		_paladin.Attributes.AddVitality();
		_paladin.Attributes.AddVitality();
		_paladin.Attributes.AddVitality();
		_paladin.Attributes.AddEnergy();
		_paladin.Attributes.AddEnergy();
		_paladin.Attributes.AddEnergy();
		_paladin.Attributes.AddEnergy();
		_paladin.Attributes.AddEnergy();
		_paladin.Attributes.AddEnergy();
		_paladin.Attributes.AddEnergy();
		_paladin.Attributes.AddEnergy();
		_paladin.Attributes.AddEnergy();
		_paladin.Attributes.AddEnergy();
		_paladin.Life.Value.OnAction(-_paladin.Life.Value.Max);     // drain to 0
		_paladin.Mana.Value.OnAction(-_paladin.Mana.Value.Max);
		_paladin.Stamina.Value.OnAction(-_paladin.Stamina.Value.Max);
	}

	[TestMethod]
	public void HealthPotion_Minor_AddsExpectedLife()
	{
		var potion = HealthPotion.Minor;
		double expected = potion.Units * _paladin.Life.Value.LevelCoef;

		potion.Drink(_paladin);

		Assert.IsTrue(potion.Empty);
		Assert.AreEqual(expected, _paladin.Life.Value.Remaining, 0.001);
	}

	[TestMethod]
	public void ManaPotion_Minor_AddsExpectedMana()
	{
		var potion = ManaPotion.Minor;
		double expected = potion.Units * _paladin.Mana.Value.LevelCoef;

		potion.Drink(_paladin);

		Assert.IsTrue(potion.Empty);
		Assert.AreEqual(expected, _paladin.Mana.Value.Remaining, 0.001);
	}

	[TestMethod]
	public void StaminaPotion_FillsToMax()
	{
		var potion = new StaminaPotion();

		potion.Drink(_paladin);

		Assert.AreEqual(_paladin.Stamina.Value.Max, _paladin.Stamina.Value.Remaining, 0.001);
	}	

	[TestMethod]
	public void RejuvenationPotion_Normal_PartialRecovery()
	{
		var potion = RejuvenationPotion.Normal;
		double expectedLife = (potion.Units / 100.0) * _paladin.Life.Value.Max;
		double expectedMana = (potion.Units / 100.0) * _paladin.Mana.Value.Max;

		potion.Drink(_paladin);

		Assert.AreEqual(expectedLife, _paladin.Life.Value.Remaining, 0.001);
		Assert.AreEqual(expectedMana, _paladin.Mana.Value.Remaining, 0.001);
	}

	[TestMethod]
	public void RejuvenationPotion_Full_FullRecovery()
	{
		var potion = RejuvenationPotion.Full;

		potion.Drink(_paladin);

		Assert.AreEqual(_paladin.Life.Value.Max, _paladin.Life.Value.Remaining, 0.001);
		Assert.AreEqual(_paladin.Mana.Value.Max, _paladin.Mana.Value.Remaining, 0.001);
	}

	[TestMethod]
	public void HealthPotion_DoesNotExceedMax()
	{
		_paladin.Life.Value.OnAction(_paladin.Life.Value.Max - 10); // near max

		var potion = HealthPotion.Super;
		potion.Drink(_paladin);

		Assert.AreEqual(_paladin.Life.Value.Max, _paladin.Life.Value.Remaining, 0.001);
	}

	[TestMethod]
	public void ManaPotion_DoesNotExceedMax()
	{
		_paladin.Mana.Value.OnAction(_paladin.Mana.Value.Max - 5); // near max

		var potion = ManaPotion.Super;
		potion.Drink(_paladin);

		Assert.AreEqual(_paladin.Mana.Value.Max, _paladin.Mana.Value.Remaining, 0.001);
	}
}
