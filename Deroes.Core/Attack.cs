namespace Deroes.Core
{
	public class Attack(Hero h, Monster m)
	{
		private Hero _hero = h;
		private Monster _monster = m;

		public void ByHero()
		{
			_hero.Attack(_monster);

			if (!_monster.IsAlive)
			{
				var xp = _monster.CalcExperience();
				_hero.AddExperience(xp);

				var drop = _monster.DropItems();

				_monster.Die();
			}
		}

		public void ByMonster()
		{
			_monster.Attack(_hero);
			if (!_hero.IsAlive)
			{
				_hero.Die();
			}
		}
	}
}
