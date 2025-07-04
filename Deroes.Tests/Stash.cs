using Deroes.Core;
using Deroes.Core.Items;
using Moq;

namespace Deroes.Tests;

[TestClass]
public class StashTests
{
	private Stash stash = null!;

	[TestInitialize]
	public void Setup()
	{
		stash = new Stash(10, 4);
	}

	private Item CreateMockItem(string name, int width, int height)
	{
		var mock = new Mock<Item>();
		mock.SetupGet(i => i.Name).Returns(name);
		mock.SetupGet(i => i.Width).Returns((byte)width);
		mock.SetupGet(i => i.Height).Returns((byte)height);
		return mock.Object;
	}

	[TestMethod]
	public void Add_1x1_Item_Succeeds()
	{
		var item = CreateMockItem("Gem", 1, 1);

		var result = stash.Add(item, 0, 0);

		Assert.IsTrue(result);
		Assert.AreEqual(item, stash.Peek(0, 0));
	}

	[TestMethod]
	public void Add_2x2_Item_Succeeds()
	{
		var item = CreateMockItem("Armor", 2, 2);

		var result = stash.Add(item, 1, 1);

		Assert.IsTrue(result);
		Assert.AreEqual(item, stash.Peek(1, 1));
		Assert.AreEqual(item, stash.Peek(2, 2));
	}

	[TestMethod]
	public void Add_Item_Overlapping_Fails()
	{
		var item1 = CreateMockItem("Sword", 2, 2);
		var item2 = CreateMockItem("Shield", 2, 2);

		Assert.IsTrue(stash.Add(item1, 0, 0));
		var result = stash.Add(item2, 1, 1); // Overlaps bottom right

		Assert.IsFalse(result);
	}

	[TestMethod]
	[ExpectedException(typeof(IndexOutOfRangeException))]
	public void Add_Item_OutOfBounds_Fails()
	{
		var item = CreateMockItem("Oversize", 3, 3);
		var result = stash.Add(item, 2, 8); // overflows horizontally

		Assert.IsFalse(result);
	}

	[TestMethod]
	public void Drop_Removes_All_Cells()
	{
		var item = CreateMockItem("Armor", 2, 2);
		stash.Add(item, 0, 0);

		var dropped = stash.Drop(0, 0);

		Assert.AreEqual(item, dropped);
		Assert.IsNull(stash.Peek(0, 0));
		Assert.IsNull(stash.Peek(1, 1));
	}

	[TestMethod]
	public void Drop_Throws_When_Null()
	{
		Assert.ThrowsException<ArgumentNullException>(() => stash.Drop(0, 0));
	}

	[TestMethod]
	public void Fill_Stash_With_1x1_Items()
	{
		int count = 0;
		for (int row = 0; row < 4; row++)
		{
			for (int col = 0; col < 10; col++)
			{
				var item = CreateMockItem($"Item{count++}", 1, 1);
				Assert.IsTrue(stash.Add(item, row, col));
			}
		}

		// Full now, next should fail
		var extra = CreateMockItem("Extra", 1, 1);
		Assert.IsFalse(stash.Add(extra, 0, 0));
	}

	[TestMethod]
	public void Add_Item_At_Last_Fitting_Slot()
	{
		var item = CreateMockItem("Corner", 2, 2);
		Assert.IsTrue(stash.Add(item, 2, 8)); // Fits in bottom right
	}

	[TestMethod]
	public void Add_1x1_Item_To_Empty_Stash_Should_Succeed()
	{
		var stash = new Stash(10, 4);
		var item = CreateMockItem("P", 1, 1);

		var result = stash.Add(item);

		Assert.IsTrue(result);
	}

	[TestMethod]
	public void Add_Item_To_Occupied_Space_Should_Find_Next_Free_Space()
	{
		var stash = new Stash(3, 2);
		var item1 = CreateMockItem("a", 1, 1);
		var item2 = CreateMockItem("b", 1, 1);
		var item3 = CreateMockItem("c", 1, 1);
		var item4 = CreateMockItem("d", 1, 1);

		Assert.IsTrue(stash.Add(item1));
		Assert.IsTrue(stash.Add(item2));
		Assert.IsTrue(stash.Add(item3));
		Assert.IsTrue(stash.Add(item4));

		var a = stash.Peek(0, 0);
		Assert.IsNotNull(a);
		Assert.AreEqual("a", a.Name);

		var d = stash.Peek(1, 0);
		Assert.IsNotNull(d);
		Assert.AreEqual("d", d.Name);
	}
}
