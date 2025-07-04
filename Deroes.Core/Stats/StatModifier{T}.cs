namespace Deroes.Core.Stats
{
	public interface IStatModifier<T>
	{
		public string Description { get; }

		/// <summary>
		/// Calculated value to include in modification
		/// Static value or Percentage
		/// </summary>
		public int GetModificator(T @base);
	}

	public class DamageModifier : IStatModifier<Damage>
	{
		private Damage _bonus;

		public string Description => $"{_bonus.Min} - {_bonus.Max} {_bonus.GetType().Name} damage";

		public DamageModifier(Damage bonus)
		{
			_bonus = bonus;
		}

		public int GetModificator(Damage @base)
		{
			throw new NotImplementedException();
		}
	}

	public class VitalModifier : IStatModifier<Vital>
	{
		private readonly int _value;

		public string Description => $"{_value} to vital";

		public VitalModifier(int value)
		{
			_value = value;
		}

		public int GetModificator(Vital @base)
		{
			return _value;
		}
	}

	public class VitalPercentageModifier : IStatModifier<Vital>
	{
		private readonly int _value;

		public string Description => $"{_value} % to maximum vital";

		public VitalPercentageModifier(int value)
		{
			_value = value;
		}

		public int GetModificator(Vital @base)
		{
			return (int)(@base.Max * (_value / 100.0));
		}
	}

	public class AttributeModifier
	{

	}
}
