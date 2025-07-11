using Deroes.Core.Stats;

namespace Deroes.Core.Items.Wearables
{
	public record WearableItemSpec
	{
		public int RequiredLevel { get; init; }
		public IList<IStatModifier> Modifiers { get; init; }
		public int RequiredStrength { get; init; }
		public int RequiredDexterity { get; init; }
		public int Durability { get; init; }
		
		public WearableItemSpec()
		{
			RequiredLevel = 1;
			Modifiers = [];
			RequiredStrength = 0;
			RequiredDexterity = 0;
			Durability = 0;
		}
	}

	public record DefenseItemSpec : WearableItemSpec
	{
		/// <summary>
		/// Initial
		/// </summary>
		public int Defense { get; init; }

		public DefenseItemSpec(int defense)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(defense);

			Defense = defense;
		}
	}

	public record WeaponItemSpec : WearableItemSpec
	{
		public int Min { get; init; }
		public int Max { get; init; }

		public WeaponItemSpec(int min, int max)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(min);
			ArgumentOutOfRangeException.ThrowIfGreaterThan(min, max);

			Min = min;
			Max = max;
		}
	}
}
