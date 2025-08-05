using Deroes.Core.Units;

namespace Deroes.Core
{
	public class Combat(Hero h, Monster m)
	{
		private Hero _hero = h;
		private Monster _monster = m;

		public Combat HeroAttacks()
		{
			if (_monster.IsAlive)
			{
				_hero.Skills.Attack.Normal.Apply(_monster);

				if (!_monster.IsAlive)
				{
					var xp = _monster.CalcExperience();
					_hero.AddExperience(xp);

					_monster.Die();
				}
			}

			return this;
		}

		public Combat MonsterAttacks()
		{
			_monster.Skills.Attack.Normal.Apply(_hero);
			if (!_hero.IsAlive)
			{
				_hero.Die();
			}

			return this;
		}
	}
}
