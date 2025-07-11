using Deroes.Core.Items.Wearables;
using System.Linq.Expressions;
using System.Reflection;

namespace Deroes.Core
{
	public class Gear
	{
		private readonly Hero _hero;

		public Helm? Helm { get; private set; }
		public Armor? Armor { get; private set; }
		public Belt Belt { get; private set; }
		public Weapon? LeftHand { get; private set; }
		public Shield? RightHand { get; private set; }
		public Gloves? Gloves { get; private set; }
		public Boots? Boots { get; private set; }
		public Ring? LeftRing { get; private set; }
		public Ring? RightRing { get; private set; }
		public Amulet? Amulet { get; private set; }

		public Gear(Hero h)
		{
			_hero = h;
			Belt = new Belt(new BeltItemSpec(0, 1));
		}

		/// <returns>If gear can be equiped or compatible</returns>
		public bool CanEquip(Wearable item)
		{
			return (_hero.Strength >= item.ItemSpec.RequiredStrength &&
				_hero.Dexterity >= item.ItemSpec.RequiredDexterity &&
				_hero.Level >= item.ItemSpec.RequiredLevel);
		}

		/// <summary>
		/// Equip a specific gear slot
		/// </summary>
		/// <param name="to">Slot to equip</param>
		/// <returns>If gear can be equiped or compatible</returns>
		public bool Equip<T>(T? item, Expression<Func<Gear, T?>> to) where T : Wearable
		{
			ArgumentNullException.ThrowIfNull(item);

			if (to.Body is MemberExpression memberExpr &&
				memberExpr.Member is PropertyInfo propInfo)
			{
				var existing = propInfo.GetValue(this);
				if (existing == null)
				{
					if (CanEquip(item))
					{
						propInfo.SetValue(this, item);
						foreach (var m in item.ItemSpec.Modifiers)
						{
							m.ApplyModification(_hero);
						}

						return true;
					}

					return false;
				}
				else
				{
					throw new InvalidOperationException("You must uneqip first");
				}
			}
			else
			{
				throw new ArgumentException("Selector must be a property");
			}
		}

		/// <summary>
		/// Unequip a slot and returns the item
		/// </summary>
		/// <returns>Item from the slot</returns>
		public T Unequip<T>(Expression<Func<Gear, T?>> from) where T : Wearable
		{
			if (from.Body is MemberExpression memberExpr &&
				memberExpr.Member is PropertyInfo propInfo)
			{
				var existingObj = propInfo.GetValue(this);
				ArgumentNullException.ThrowIfNull(existingObj);

				var item = (T)existingObj;
				foreach (var m in item.ItemSpec.Modifiers)
				{
					m.RemoveModification(_hero);
				}

				propInfo.SetValue(this, null);

				return item;
			}
			else
			{
				throw new InvalidOperationException("Selector must be a property");
			}
		}
	}
}
