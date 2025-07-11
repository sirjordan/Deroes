using Deroes.Core.Items;
using Deroes.Core.Items.Wearables;

namespace Deroes.Tests;

[TestClass]
public class Gear_Belt
{
	[TestMethod]
	public void CanAddPotion_WhenSpaceAvailable_ReturnsTrue()
	{
		var belt = new Belt(new BeltItemSpec(0, 2));

		var potion = HealthPotion.Minor;
		bool added = belt.Add(potion);

		Assert.IsTrue(added);
	}

	[TestMethod]
	public void AddPotionToSpecificSlot_WorksCorrectly()
	{
		var belt = new Belt(new BeltItemSpec(1, 2));

		var potion = HealthPotion.Minor;
		bool added = belt.Add(potion, 0, 0);

		Assert.IsTrue(added);
	}

	[TestMethod]
	public void DropPotion_RemovesPotionAndReturnsIt()
	{
		var belt = new Belt(new BeltItemSpec(1, 2));

		var potion = HealthPotion.Minor;
		belt.Add(potion, 0, 0);

		var dropped = belt.Drop(0, 0);

		Assert.IsNotNull(dropped);
		Assert.AreEqual(potion, dropped);
	}

	[TestMethod]
	public void DropPotion_EmptySlot_ReturnsNull()
	{
		var belt = new Belt(new BeltItemSpec(1, 2));

		var dropped = belt.Drop(0, 0);

		Assert.IsNull(dropped);
	}

	[TestMethod]
	public void CannotAddPotion_WhenNoSpaceAvailable()
	{
		var rows = 2;
		var belt = new Belt(new BeltItemSpec(1, rows));

		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < Belt.COLS; j++)
			{
				var potion = HealthPotion.Minor;
				Assert.IsTrue(belt.Add(potion));
			}
		}
		
		var extraPotion = HealthPotion.Minor;
		Assert.IsFalse(belt.Add(extraPotion));
	}
}
