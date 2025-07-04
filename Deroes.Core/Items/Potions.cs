namespace Deroes.Core.Items
{
	public interface IPotion
	{
		public void Drink(Hero h);
	}

	public class HealthPotion : Item, IPotion
	{
		public double Value { get; private init; }

		public void Drink(Hero h)
		{
			var hp = Value * h.Life.LevelCoef;
			h.Life.OnAction(hp);
		}

		public static HealthPotion Minor => new() { Value = 30 };
		public static HealthPotion Light => new() { Value = 60 };
		public static HealthPotion Normal => new() { Value = 100 };
		public static HealthPotion Greater => new() { Value = 180 };
		public static HealthPotion Super => new() { Value = 320 };
	}

	public class StaminaPotion : IPotion
	{
		public void Drink(Hero h)
		{
			h.Stamina.OnAction(h.Stamina.Max);
		}
	}

	public class ManaPotion : IPotion
	{
		public double Value { get; private init; }

		public void Drink(Hero h)
		{
			var mana = Value * h.Mana.LevelCoef;
			h.Mana.OnAction(mana);
		}

		public static ManaPotion Minor => new() { Value = 20 };
		public static ManaPotion Light => new() { Value = 40 };
		public static ManaPotion Normal => new() { Value = 80 };
		public static ManaPotion Greater => new() { Value = 150 };
		public static ManaPotion Super => new() { Value = 250 };
	}

	public class RejuvenationPotion : IPotion
	{
		public double Value { get; private init; }

		public void Drink(Hero h)
		{
			var life = Value / 100 * h.Life.Max;
			var mana = Value / 100 * h.Mana.Max;

			h.Life.OnAction(life);
			h.Mana.OnAction(mana);
		}

		public static RejuvenationPotion Normal => new() { Value = 35 };
		public static RejuvenationPotion Full => new() { Value = 100 };
	}
}
