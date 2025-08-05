using Deroes.Core.Skills;

namespace Deroes.Core.Tests;

[TestClass]
public class Skills
{
	[TestMethod]
	public void Vengeance_CalculateBonusDmg_Level1_Returns70()
	{
		int result = Vengeance.CalculateBonusDmg(1);
		Assert.AreEqual(70, result);
	}

	[TestMethod]
	public void Vengeance_CalculateBonusDmg_Level2_Returns76()
	{
		int result = Vengeance.CalculateBonusDmg(2);
		Assert.AreEqual(76, result);
	}

	[TestMethod]
	public void Vengeance_CalculateBonusDmg_Level10_Returns124()
	{
		int result = Vengeance.CalculateBonusDmg(10);
		Assert.AreEqual(124, result);
	}

	[TestMethod]
	public void Vengeance_CalculateBonusDmg_Level60_Returns424()
	{
		int result = Vengeance.CalculateBonusDmg(60);
		Assert.AreEqual(424, result);
	}

	[TestMethod]
	public void Vengeance_Calculate_ManaCost()
	{
		double[] expected =
		{
			4.0, 4.3, 4.5, 4.8, 5.0, 5.3, 5.5, 5.8, 6.0, 6.3,
			6.5, 6.8, 7.0, 7.3, 7.5, 7.8, 8.0, 8.3, 8.5, 8.8,
			9.0, 9.3, 9.5, 9.8, 10.0, 10.3, 10.5, 10.8, 11.0, 11.3,
			11.5, 11.8, 12.0, 12.3, 12.5, 12.8, 13.0, 13.3, 13.5, 13.8,
			14.0, 14.3, 14.5, 14.8, 15.0, 15.3, 15.5, 15.8, 16.0, 16.3,
			16.5, 16.8, 17.0, 17.3, 17.5, 17.8, 18.0, 18.3, 18.5, 18.8
		};

		for (int i = 0; i < expected.Length; i++)
		{
			double actual = Vengeance.CalculateManaCost(i);
			Assert.AreEqual(expected[i], actual, 0.3, $"Mismatch at index {i}");
		}
	}
}
