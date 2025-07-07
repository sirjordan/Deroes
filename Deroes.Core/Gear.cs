using Deroes.Core.Items;
using System.Linq.Expressions;
using System.Reflection;

namespace Deroes.Core
{
	public class Gear(Hero h)
	{
		private readonly Hero _hero = h;

		public Helm? Helm { get; private set; }
		public Armor? Armor { get; private set; }
		public Belt? Belt { get; private set; }
		public Weapon? LeftHand { get; private set; }
		public Shield? RightHand { get; private set; }
		public Gloves? Gloves { get; private set; }
		public Boots? Boots { get; private set; }
		public Ring? LeftRing { get; private set; }
		public Ring? RightRing { get; private set; }
		public Amulet? Amulet { get; private set; }

		/// <returns>If gear can be equiped or compatible</returns>
		public bool CanEquip(WearableItem item)
		{
			return (_hero.Strength >= item.RequiredStrength &&
				_hero.Dexterity >= item.RequiredDexterity &&
				_hero.Level >= item.RequiredLevel);
		}

		/// <summary>
		/// Equip a specific gear slot
		/// </summary>
		/// <param name="to">Slot to equip</param>
		/// <returns>If gear can be equiped or compatible</returns>
		public bool Equip<T>(T? item, Expression<Func<Gear, T?>> to) where T : WearableItem
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
						foreach (var m in item.Modifiers)
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
		public T Unequip<T>(Expression<Func<Gear, T?>> from) where T : WearableItem
		{
			if (from.Body is MemberExpression memberExpr &&
				memberExpr.Member is PropertyInfo propInfo)
			{
				var existingObj = propInfo.GetValue(this);
				ArgumentNullException.ThrowIfNull(existingObj);

				var item = (T)existingObj;
				foreach (var m in item.Modifiers)
				{
					m.RemoveModification(_hero);
				}

				propInfo.SetValue(this, null);

				return item;
			}
			else
			{
				throw new InvalidOperationException("You must uneqip first");
			}
		}
	}
}
