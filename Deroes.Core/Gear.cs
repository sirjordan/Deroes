using Deroes.Core.Items;

namespace Deroes.Core
{
	public class Gear
	{
		private readonly Hero _hero;

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

		public Gear(Hero h)
		{
			_hero = h;
		}

		private void AddModification()
		{
			// On Equip
		}

		private void ChangeLook()
		{
			// On Equip
		}

		public bool Equip(WearableItem item)
		{
			if (_hero.Strength >= item.RequiredStrength &&
				_hero.Dexterity >= item.RequiredDexterity &&
				_hero.Level >= item.RequiredLevel)
			{
				// TODO:
				// Equip

				return true;
			}
			else
			{
				return false;
			}
			// Equip if empty gear slot
			// Apply modifications and change look
		}

		public void Unequip()
		{

		}

		public void DropAll()
		{
			// You get killed and drop all gear
		}
	}
}
