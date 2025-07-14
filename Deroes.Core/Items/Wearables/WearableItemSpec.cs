using Deroes.Core.Stats.Modifiers;

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
		/// Initial/base
		/// </summary>
		public int Defense { get; init; }

		public DefenseItemSpec(int defense)
		{
			ArgumentOutOfRangeException.ThrowIfNegative(defense);

			Defense = defense;
		}
	}

	public record WeaponItemSpec : WearableItemSpec
	{
		/// <summary>
		/// Initial/base
		/// </summary>
		public int Min { get; init; }
		/// <summary>
		/// Initial/base
		/// </summary>
		public int Max { get; init; }

		public WeaponItemSpec(int min, int max)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(min);
			ArgumentOutOfRangeException.ThrowIfGreaterThan(min, max);

			Min = min;
			Max = max;
		}
	}

	public record BeltItemSpec : DefenseItemSpec
	{
		public const int MAX_ROWS = 4;
		public int Rows { get; private set; }

		/// <summary>
		/// 1-4 rows
		/// </summary>
		public BeltItemSpec(int defense, int rows) : base(defense)
		{
			ArgumentOutOfRangeException.ThrowIfGreaterThan(rows, MAX_ROWS);
			ArgumentOutOfRangeException.ThrowIfLessThan(rows, 1);

			Rows = rows;
		}
	}
}
