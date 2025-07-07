namespace Deroes.Core.Stats
{
	public class Stat<T> where T: IStatModifiable<T>
	{
		private readonly List<IStatModifier<T>> _modifiers;

		/// <summary>
		/// Original base value, without gear modifications
		/// </summary>
		public T BaseValue { get; private set; }
		/// <summary>
		/// Value with gear modifiers applied
		/// </summary>
		public T Value
		{
			get
			{
				T result = BaseValue;
				foreach (var mod in _modifiers.OrderBy(m => m.Order))
				{
					result = result.Modify(mod.GetModificator(result));
				}

				return result;
			}
		}

		public Stat(T baseValue)
		{
			BaseValue = baseValue;
			_modifiers = [];
		}

		public void AddModifier(IStatModifier<T> modifier)
		{
			_modifiers.Add(modifier);
		}

		public void RemoveModifier(IStatModifier<T> modifier)
		{
			_modifiers.Remove(modifier);
		}
	}
}
