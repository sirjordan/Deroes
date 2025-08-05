using Deroes.Core.Units;

namespace Deroes.Core.Stats.Modifiers
{
	public class FlatDamageModifier(int damage) : IStatModifier<Damage>
	{
		private int _damage = damage;

		public string Description => $"{_damage}";
		public int Order => 1;

		public int GetModificator(Damage @base) => _damage;
	}

	public class PercentageDamageModifier(int percentage) : IStatModifier<Damage>
	{
		private int _percentage = percentage;

		public string Description => $"{_percentage}% damage";
		public int Order => 100;

		public int GetModificator(Damage @base) => (int)(@base.Amount * (_percentage / 100.0));
	}

	public abstract class DamageRangeModifier(IStatModifier<Damage> min, IStatModifier<Damage> max) 
		: IStatModifier
	{
		private IStatModifier<Damage> _bonusMin = min;
		private IStatModifier<Damage> _bonusMax = max;

		protected abstract Func<Unit, DamageRange> Selector { get; }

		public void ApplyModification(Unit h)
		{
			var range = Selector(h);

			range.Min.AddModifier(_bonusMin);
			range.Max.AddModifier(_bonusMax);
		}

		public void RemoveModification(Unit h)
		{
			var range = Selector(h);

			range.Min.RemoveModifier(_bonusMin);
			range.Max.RemoveModifier(_bonusMax);
		}
	}

	public class PhysicalDamageModifier(IStatModifier<Damage> min, IStatModifier<Damage> max) 
		: DamageRangeModifier(min, max)
	{
		protected override Func<Unit, DamageRange> Selector => h => h.Melee.Physical;
	}

	public class ColdDamageModifier(IStatModifier<Damage> min, IStatModifier<Damage> max)
		: DamageRangeModifier(min, max)
	{
		protected override Func<Unit, DamageRange> Selector => h => h.Melee.Cold;
	}

	public class FireDamageModifier(IStatModifier<Damage> min, IStatModifier<Damage> max)
		: DamageRangeModifier(min, max)
	{
		protected override Func<Unit, DamageRange> Selector => h => h.Melee.Fire;
	}

	public class LightiningDamageModifier(IStatModifier<Damage> min, IStatModifier<Damage> max)
		: DamageRangeModifier(min, max)
	{
		protected override Func<Unit, DamageRange> Selector => h => h.Melee.Lightining;
	}
}
