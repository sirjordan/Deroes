namespace Deroes.Core.Stats
{
	public class Stat<T> where T: IStatModifiable<T>
	{
		private readonly List<IStatModifier<T>> _modifiers = new();

		public T BaseValue { get; private set; }
		public T Value
		{
			get
			{
				T result = BaseValue;
				foreach (var mod in _modifiers)
				{
					result = result.Modify(mod.GetModificator(result));
				}

				return result;
			}
		}

		public Stat(T baseValue)
		{
			BaseValue = baseValue;
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
