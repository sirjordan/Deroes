using Deroes.Core.Skills;
using Deroes.Core.Units;

namespace Deroes.Core
{
	public class Combat(Hero h, Monster m)
	{
		private Hero _hero = h;
		private Monster _monster = m;

		public Combat HeroAttacks(Func<Unit, Skill> attack)
		{
			if (_monster.IsAlive)
			{
				attack.Invoke(_hero).Apply(_monster);

				if (!_monster.IsAlive)
				{
					var xp = _monster.CalcExperience();
					_hero.AddExperience(xp);

					_monster.Die();
				}
			}

			return this;
		}

		public Combat MonsterAttacks(Func<Unit, Skill> attack)
		{
			attack.Invoke(_monster).Apply(_hero);
			if (!_hero.IsAlive)
			{
				_hero.Die();
			}

			return this;
		}
	}
}
