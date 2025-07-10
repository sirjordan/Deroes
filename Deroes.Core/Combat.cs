namespace Deroes.Core
{
	public class Combat(Hero h, Monster m)
	{
		private Hero _hero = h;
		private Monster _monster = m;

		public void HeroAttacks()
		{
			_hero.Attack(_monster);

			if (!_monster.IsAlive)
			{
				var xp = _monster.CalcExperience();
				_hero.AddExperience(xp);

				var drop = _monster.DropItems();
				var gold = _monster.DropGold();

				_monster.Die();
			}
		}

		public void MonsterAttacks()
		{
			_monster.Attack(_hero);
			if (!_hero.IsAlive)
			{
				_hero.Die();
			}
		}
	}
}
