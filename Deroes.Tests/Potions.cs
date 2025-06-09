using Deroes.Core;

namespace Deroes.Tests;

[TestClass]
public class Potions
{
	private Paladin _paladin;

	[TestInitialize]
	public void Setup()
	{
		_paladin = new Paladin();
		_paladin.AddVitality();
		_paladin.AddVitality();
		_paladin.AddVitality();
		_paladin.AddVitality();
		_paladin.AddVitality();
		_paladin.AddEnergy();
		_paladin.AddEnergy();
		_paladin.AddEnergy();
		_paladin.AddEnergy();
		_paladin.AddEnergy();
		_paladin.AddEnergy();
		_paladin.AddEnergy();
		_paladin.AddEnergy();
		_paladin.AddEnergy();
		_paladin.AddEnergy();
		_paladin.Life.OnAction(-_paladin.Life.Max);     // drain to 0
		_paladin.Mana.OnAction(-_paladin.Mana.Max);
		_paladin.Stamina.OnAction(-_paladin.Stamina.Max);
	}

	[TestMethod]
	public void HealthPotion_Minor_AddsExpectedLife()
	{
		var potion = HealthPotion.Minor;
		double expected = potion.Value * _paladin.Life.LevelCoef;

		potion.Drink(_paladin);

		Assert.AreEqual(expected, _paladin.Life.Remaining, 0.001);
	}

	[TestMethod]
	public void ManaPotion_Minor_AddsExpectedMana()
	{
		var potion = ManaPotion.Minor;
		double expected = potion.Value * _paladin.Mana.LevelCoef;

		potion.Drink(_paladin);

		Assert.AreEqual(expected, _paladin.Mana.Remaining, 0.001);
	}

	[TestMethod]
	public void StaminaPotion_FillsToMax()
	{
		var potion = new StaminaPotion();

		potion.Drink(_paladin);

		Assert.AreEqual(_paladin.Stamina.Max, _paladin.Stamina.Remaining, 0.001);
	}

	[TestMethod]
	public void RejuvenationPotion_Normal_PartialRecovery()
	{
		var potion = RejuvenationPotion.Normal;
		double expectedLife = (potion.Value / 100.0) * _paladin.Life.Max;
		double expectedMana = (potion.Value / 100.0) * _paladin.Mana.Max;

		potion.Drink(_paladin);

		Assert.AreEqual(expectedLife, _paladin.Life.Remaining, 0.001);
		Assert.AreEqual(expectedMana, _paladin.Mana.Remaining, 0.001);
	}

	[TestMethod]
	public void RejuvenationPotion_Full_FullRecovery()
	{
		var potion = RejuvenationPotion.Full;

		potion.Drink(_paladin);

		Assert.AreEqual(_paladin.Life.Max, _paladin.Life.Remaining, 0.001);
		Assert.AreEqual(_paladin.Mana.Max, _paladin.Mana.Remaining, 0.001);
	}

	[TestMethod]
	public void HealthPotion_DoesNotExceedMax()
	{
		_paladin.Life.OnAction(_paladin.Life.Max - 10); // near max

		var potion = HealthPotion.Super;
		potion.Drink(_paladin);

		Assert.AreEqual(_paladin.Life.Max, _paladin.Life.Remaining, 0.001);
	}

	[TestMethod]
	public void ManaPotion_DoesNotExceedMax()
	{
		_paladin.Mana.OnAction(_paladin.Mana.Max - 5); // near max

		var potion = ManaPotion.Super;
		potion.Drink(_paladin);

		Assert.AreEqual(_paladin.Mana.Max, _paladin.Mana.Remaining, 0.001);
	}
}
