namespace Deroes.Core.Stats.Modifiers
{
	public class FlatDamageModifier(int damage) : IStatModifier<Damage>
	{
		private int _damage = damage;

		public string Description => $"{_damage}";
		public int Order => 1;

		public int GetModificator(Damage @base) => _damage;
	}

	public abstract class DamageRangeModifier(IStatModifier<Damage> min, IStatModifier<Damage> max) 
		: IStatModifier
	{
		private IStatModifier<Damage> _bonusMin = min;
		private IStatModifier<Damage> _bonusMax = max;

		protected abstract Func<Hero, DamageRange> Selector { get; }

		public void ApplyModification(Hero h)
		{
			var range = Selector(h);

			range.Min.AddModifier(_bonusMin);
			range.Max.AddModifier(_bonusMax);
		}

		public void RemoveModification(Hero h)
		{
			var range = Selector(h);

			range.Min.RemoveModifier(_bonusMin);
			range.Max.RemoveModifier(_bonusMax);
		}
	}

	public class PhysicalDamageModifier(IStatModifier<Damage> min, IStatModifier<Damage> max) 
		: DamageRangeModifier(min, max)
	{
		protected override Func<Hero, DamageRange> Selector => h => h.Damage.Physical;
	}

	public class ColdDamageModifier(IStatModifier<Damage> min, IStatModifier<Damage> max)
		: DamageRangeModifier(min, max)
	{
		protected override Func<Hero, DamageRange> Selector => h => h.Damage.Cold;
	}
}
