namespace Deroes.Core.Items
{
	public interface IPotion
	{
		public void Drink(Hero h);
	}

	public abstract class Potion : Item, IPotion
	{
		public double Units { get; protected init; }
		public bool Empty { get; private set; }

		protected Potion()
		{
			Empty = false;
		}

		public void Drink(Hero h)
		{
			if (!Empty)
			{
				ApplyEffect(h);
				Empty = true;
			}
		}

		protected abstract void ApplyEffect(Hero h);
	}

	public class HealthPotion : Potion
	{
		protected override void ApplyEffect(Hero h)
		{
			var hp = Units * h.Life.Value.LevelCoef;
			h.Life.Value.OnAction(hp);
		}

		public static HealthPotion Minor => new() { Units = 30 };
		public static HealthPotion Light => new() { Units = 60 };
		public static HealthPotion Normal => new() { Units = 100 };
		public static HealthPotion Greater => new() { Units = 180 };
		public static HealthPotion Super => new() { Units = 320 };
	}

	public class StaminaPotion : Potion
	{
		protected override void ApplyEffect(Hero h)
		{
			h.Stamina.Value.OnAction(h.Stamina.Value.Max);
		}
	}

	public class ManaPotion : Potion
	{
		protected override void ApplyEffect(Hero h)
		{
			var mana = Units * h.Mana.Value.LevelCoef;
			h.Mana.Value.OnAction(mana);
		}

		public static ManaPotion Minor => new() { Units = 20 };
		public static ManaPotion Light => new() { Units = 40 };
		public static ManaPotion Normal => new() { Units = 80 };
		public static ManaPotion Greater => new() { Units = 150 };
		public static ManaPotion Super => new() { Units = 250 };
	}

	public class RejuvenationPotion : Potion
	{
		protected override void ApplyEffect(Hero h)
		{
			var life = Units / 100 * h.Life.Value.Max;
			var mana = Units / 100 * h.Mana.Value.Max;

			h.Life.Value.OnAction(life);
			h.Mana.Value.OnAction(mana);
		}

		public static RejuvenationPotion Normal => new() { Units = 35 };
		public static RejuvenationPotion Full => new() { Units = 100 };
	}
}
