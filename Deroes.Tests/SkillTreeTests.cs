using Deroes.Core.Skills;
using Deroes.Core.Units;

namespace Deroes.Core.Tests;

#nullable disable

[TestClass]
public class SkillTreeTests
{
	[TestMethod]
	public void GetAllSkills_ReturnsAllNodes_BFS()
	{
		// Arrange
		var hero = new Paladin();
		var tree = new SkillTree(hero);

		var root = new SkillTree.SkillNode(new Might(hero));
		var child1 = new SkillTree.SkillNode(new ResistFire(hero));
		var child2 = new SkillTree.SkillNode(new Vengeance(hero));

		root.AddChild(child1);
		root.AddChild(child2);

		// Hacky way to set private RootSkills for test
		typeof(SkillTree)
			.GetProperty("RootSkills", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
			.SetValue(tree, new[] { root });

		// Act
		var skills = tree.GetAllSkills().ToList();

		// Assert
		Assert.AreEqual(3, skills.Count);
		CollectionAssert.Contains(skills, root);
		CollectionAssert.Contains(skills, child1);
		CollectionAssert.Contains(skills, child2);
	}

	[TestMethod]
	[ExpectedException(typeof(InvalidOperationException))]
	public void AddSkillPoint_ThrowsIfNoPoints()
	{
		var hero = new Paladin();
		var tree = new SkillTree(hero);

		var root = new SkillTree.SkillNode(new Might(hero));
		typeof(SkillTree)
			.GetProperty("RootSkills", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
			.SetValue(tree, new[] { root });

		tree.AddSkillPoint(root);
	}

	[TestMethod]
	[ExpectedException(typeof(ArgumentException))]
	public void AddSkillPoint_ThrowsIfNodeNotInTree()
	{
		var hero = new Paladin();
		var tree = new SkillTree(hero);

		var root = new SkillTree.SkillNode(new Might(hero));
		var outsider = new SkillTree.SkillNode(new Vengeance(hero));

		typeof(SkillTree)
			.GetProperty("RootSkills", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
			.SetValue(tree, new[] { root });

		tree.OnLevelUp(); // +1 point
		tree.AddSkillPoint(outsider); // not in tree
	}

	[TestMethod]
	public void AddSkillPoint_UpgradesSkillAndConsumesPoint()
	{
		var hero = new Paladin();
		var tree = new SkillTree(hero);

		var rootSkill = new Might(hero);
		var root = new SkillTree.SkillNode(rootSkill);

		typeof(SkillTree)
			.GetProperty("RootSkills", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
			.SetValue(tree, new[] { root });

		tree.OnLevelUp(); // +1 point

		// Act
		tree.AddSkillPoint(root);

		// Assert
		Assert.AreEqual(1, root.Skill.Level);
		Assert.AreEqual(0, tree.AvaliableSkillPoints);
	}

	[TestMethod]
	[ExpectedException(typeof(ArgumentOutOfRangeException))]
	public void AddChild_ThrowsIfChildTierLessThanParent()
	{
		var hero = new Paladin();
		var parent = new SkillTree.SkillNode(new Vengeance(hero));
		var child = new SkillTree.SkillNode(new Might(hero)); // lower tier

		parent.AddChild(child);
	}
}

#nullable enable
