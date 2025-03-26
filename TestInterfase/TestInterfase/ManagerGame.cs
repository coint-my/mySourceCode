using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestHero
{
    public class ManagerGame
    {
        public Heroies[] _EnemyHero;
        public Heroies _Hero;

        private int jumpLevel, jumpLevel2, jumpLevel3, jumpLevel4, jumpLevel5;
        private int extraLive;
        public int heroGold;

        private ManagerGame()
        {
            jumpLevel = jumpLevel2 = jumpLevel3 = jumpLevel4 = jumpLevel5 = 0;
            extraLive = 1;
            heroGold = 500;
        }

        public ManagerGame(Heroies hero, Heroies[] enemyHero) : this()
        {
            _EnemyHero = enemyHero;
            _Hero = hero;
        }

        public void Game()
        {
        Start:
            Console.Clear();
            Console.WriteLine("Уровень 1\n");
            int varible = 0;
            Console.WriteLine("1) Направо в волчий лес ({0}) Уровень {1}\n2) Налево в паучю пещеру ({2}) Уровень {3}\n3) Прямо",
                _EnemyHero[0].Name, jumpLevel + 1, _EnemyHero[1].Name, jumpLevel2 + 1);
            Console.WriteLine("4) У вас есть [{0}] экстра здоровье", extraLive);

            Int32.TryParse(Console.ReadLine(), out varible);

            if ((varible >= 1 && varible <= 4))
            {
                switch (varible)
                {
                    case 0:
                        Console.WriteLine("Вас убили!");
                        Console.ReadKey();
                        break;
                    case 1:
                        jumpLevel++;
                        _EnemyHero[0].LevelUp(jumpLevel);
                        int i = Battle(_Hero, _EnemyHero[0]);
                        int x = 0;

                        if (i == -1)
                            goto Stop;
                    Resume:
                        Console.Clear();
                        Console.WriteLine(_Hero.ToString());
                        if (jumpLevel == 2)
                        {
                            extraLive += 1;
                            Console.WriteLine("\nУ вас + 1 зелье здоровья");
                        }

                        Console.WriteLine("Выберите награду \n1) Здоровье ({0})\n2) Опыт ({1}) [xp]", _EnemyHero[0].LiveHero * 3 / 8,
                            _EnemyHero[0].LiveHero * 5 / 4);
                        Int32.TryParse(Console.ReadLine(), out x);
                        if ((x < 1) || (x > 2))
                        {
                            Console.WriteLine("Неверный выбор");
                            Console.ReadKey();
                            goto Resume;
                        }
                        if (x == 1)
                            _Hero.LiveInt += _EnemyHero[0].LiveHero * 3 / 8;
                        if (x == 2)
                            _Hero.LevelExpirience(_EnemyHero[0].LiveHero * 5 / 4);
                        Console.Clear();
                        Console.WriteLine(_Hero.ToString());
                        Console.WriteLine("Выберите награду \n1) Здоровье ({0})\n2) Опыт ({1}) [xp]", _EnemyHero[0].LiveHero * 3 / 8,
                            _EnemyHero[0].LiveHero * 5 / 4);

                        Console.ReadKey();

                        _EnemyHero[0].isHeroesDead = true;
                        _EnemyHero[0].LiveInt = _EnemyHero[0].LiveHero;

                        if (i == 0)
                            goto Start;
                        break;
                    case 2:
                        jumpLevel2++;

                        _EnemyHero[1].LevelUp(jumpLevel2);

                        int y = Battle(_Hero, _EnemyHero[1]);

                        int z = 0;

                        if (y == -1)
                            goto Stop;

                        Console.WriteLine(_Hero.ToString());
                        if (jumpLevel2 == 3)
                        {
                            extraLive += 1;
                            Console.WriteLine("\nУ вас + 1 зелье здоровья");
                        }
                        Console.WriteLine("Выберите награду \n\n1) Здоровье ({0})\n2) Опыт ({1}) [xp]", _EnemyHero[1].LiveHero * 3 / 8,
                            _EnemyHero[1].LiveHero * 5 / 4);
                        Int32.TryParse(Console.ReadLine(), out z);
                        if (z == 1)
                            _Hero.LiveInt += _EnemyHero[1].LiveHero * 3 / 8;
                        if (z == 2)
                            _Hero.LevelExpirience(_EnemyHero[1].LiveHero * 5 / 4);
                        Console.Clear();
                        Console.WriteLine(_Hero.ToString());
                        Console.WriteLine("Выберите награду \n\n1) Здоровье ({0})\n2) Опыт ({1}) [xp]", _EnemyHero[1].LiveHero * 3 / 8,
                            _EnemyHero[1].LiveHero * 5 / 4);

                        Console.ReadKey();

                        _EnemyHero[1].isHeroesDead = true;
                        _EnemyHero[1].LiveInt = _EnemyHero[1].LiveHero;

                        if (y == 0)
                            goto Start;
                        break;
                    case 3:
                        Game2();
                        break;
                    case 4:
                        if (extraLive == 0)
                        {
                            Console.WriteLine("У вас болше нет зелья здоровье");
                            Console.ReadKey();
                            goto Start;
                        }
                        else
                        {
                            extraLive -= 1;
                            _Hero.LiveInt = _Hero.LiveHero;
                            Console.WriteLine("Вы использовали зелье здоровья");
                            Console.WriteLine(_Hero.ToString());
                            Console.ReadKey();
                            goto Start;
                        }
                }
            }
            else
            {
                Console.WriteLine("Не верный выбор {0}", varible);
                Console.ReadKey();
                goto Start;
            }
        Stop:
            Console.WriteLine("Вас убили");
            Console.ReadKey();
        }

        public void Game2()
        {
            int choiseVar = 0;
        Start2:
            Console.Clear();
            Console.WriteLine("Вы попали на (уровень 2)\n");
            Console.WriteLine("1) Вернутся назад на (уровень 1)\n\n2) Зайти в (Склеп) {0} Уровень {1}\n3) Зайти в заброшенные (Руины) {2} Уровень {3}\n\n4) \"Оружейный магазин\"\n",
                _EnemyHero[2].Name, jumpLevel3 + 1,_EnemyHero[3].Name, jumpLevel4 + 1);
            Console.WriteLine("5) пройти на (луга) {0} Уровень {1}", _EnemyHero[4].Name, jumpLevel5 + 1);
            Int32.TryParse(Console.ReadLine(), out choiseVar);
            if ((choiseVar >= 1) && (choiseVar <= 5))
            { }
            else
            {
                Console.WriteLine("Неверный выбор");
                Console.ReadKey();
                goto Start2;
            }

            switch (choiseVar)
            {
                case 1:
                    Game();
                    break;
                case 2:
                    jumpLevel3++;
                    _EnemyHero[2].LevelUp(jumpLevel3);
                    int i = BattleHero(_Hero, _EnemyHero[2]);
                    if (i == -1)
                        goto Stop2;
                    heroPoint(_Hero, _EnemyHero[2]);
                    goto Start2;
                    
                case 3:
                    jumpLevel4++;
                    _EnemyHero[3].LevelUp(jumpLevel4);
                    int j = BattleHero(_Hero, _EnemyHero[3]);
                    if (j == -1)
                        goto Stop2;
                    heroPoint(_Hero, _EnemyHero[3]);
                    goto Start2;
                case 4:
                    shopHero();
                    break;
                case 5:
                    jumpLevel5++;
                    _EnemyHero[4].LevelUp(jumpLevel5);
                    int d = BattleHero(_Hero, _EnemyHero[4]);
                    if (d == -1)
                        goto Stop2;
                    heroPoint(_Hero, _EnemyHero[4]);
                    goto Start2;
            }
        Stop2:
            Console.Clear();
        }

        private int Battle(Heroies _hero, Heroies _enemyHero)
        {
            int count = 0;

            Console.WriteLine("Начинается бой [ ({0}) против ({1}) Уровень {2} ]", _hero.Name, _enemyHero.Name, _enemyHero.LevelInt);
            Console.ReadKey();
            Console.Clear();

            while ((_enemyHero.isHeroesDead == true) && (_hero.isHeroesDead == true))
            {
                int varible = 0;

                Console.WriteLine(_hero.ToString() + "\n\n" + _enemyHero.ToString() + "\n");
                string str1 = "выберите обычную атаку (1)";
                Console.WriteLine("Сейчас ходит Герой ({0}) {1}", count == 0 ? _hero.Name : _enemyHero.Name,
                    count == 1 ? "" : str1);
                if (count == 0)
                    Int32.TryParse(Console.ReadLine(), out varible);

                int heroUr = 0;

                if (count == 0)
                {
                    heroUr = _hero.HeroUron(_enemyHero.Def);
                    _enemyHero.LiveInt -= heroUr;
                }
                if (count == 1)
                {
                    heroUr = _enemyHero.HeroUron(_hero.Def);
                    _hero.LiveInt -= heroUr;
                    count = -1;
                }

                Console.WriteLine("\nГерой ({0}) наносит урон в [{1}]", count == 0 ? _hero.Name : _enemyHero.Name,
                    heroUr);

                if (_enemyHero.isHeroesDead != true)
                {
                    Console.WriteLine("\nУра победа! (Герой ({0}) + {1}[XP])", _hero.Name, _enemyHero.LiveHero * 5 / 6 * _enemyHero.LevelInt);
                    _hero.LevelExpirience(_enemyHero.LiveHero * 5 / 6 * _enemyHero.LevelInt);
                }

                count++;
                Console.ReadKey();
                Console.Clear();
            }
            if (_hero.isHeroesDead != true)
                return -1;
            return 0;
        }

        private int BattleHero(Heroies _hero, Heroies _enemyHero)
        {
            int count = 0;


            Console.WriteLine("Начинается бой ({0}) против ({1})", _hero.Name, _enemyHero.Name);
            Console.ReadKey();
            Console.Clear();

            while ((_enemyHero.isHeroesDead == true) && (_hero.isHeroesDead == true))
            {
                int varible = 0;

                Console.WriteLine(_hero.ToString() + "\n\n" + _enemyHero.ToString() + "\n");
                string str1 = "\n1) Атака \n2) Зелье здоровья " + "[" + extraLive + " шт]";
                Console.WriteLine("Ходит Герой ({0}) {1}", count == 0 ? _hero.Name : _enemyHero.Name,
                    count == 1 ? "" : str1, extraLive);
                if (count == 0)
                {
                    Int32.TryParse(Console.ReadLine(), out varible);
                    if (varible == 2)
                    {
                        if (extraLive != 0)
                        {
                            extraLive -= 1;
                            _hero.LiveInt = _hero.LiveHero;
                            goto position;
                        }
                    }
                }
                int heroUr = 0;

                if (count == 0)
                {
                    heroUr = _hero.HeroUron(_enemyHero.Def);
                    _enemyHero.LiveInt -= heroUr;
                }
                if (count == 1)
                {
                    heroUr = _enemyHero.HeroUron(_hero.Def);
                    _hero.LiveInt -= heroUr;
                    count = -1;
                }

                Console.WriteLine("Герой ({0}) наносит урон в [{1}]", count == 0 ? _hero.Name : _enemyHero.Name,
                    heroUr);

                if (_enemyHero.isHeroesDead != true)
                {
                    Console.WriteLine("\nУра победа! (Герой ({0}) + {1}[XP])", _hero.Name,
                        _enemyHero._AttributeVal[5] * _enemyHero.LevelInt / 3);
                    _hero.LevelExpirience(_enemyHero._AttributeVal[5] * _enemyHero.LevelInt / 3);
                }
            position:
                count++;
                Console.ReadKey();
                Console.Clear();
            }

            if (_hero.isHeroesDead != true)
                return -1;
            return 0;
        }

        private void heroPoint(Heroies _hero, Heroies _enemyHero)
        {
            Console.WriteLine(_hero.ToString() + "\n");
            Console.WriteLine("Выберите награду\n\n1) Здоровье ({0})\n2) Опыт ({1})\n",
                _enemyHero.LiveHero * 5 / 7 * _enemyHero.LevelInt, _enemyHero._AttributeVal[5] / 4);
            Console.WriteLine("бонус золота ({0})", _enemyHero._AttributeVal[5] / 5);
            Console.WriteLine("Ваше общее золото ({0})", heroGold);
            heroGold += _enemyHero._AttributeVal[5] / 5;
            int var = 0;
            int.TryParse(Console.ReadLine(), out var);
            if (var == 1)
                _hero.LiveInt += (_enemyHero.LiveHero * 5 / 7 * _enemyHero.LevelInt);
            if (var == 2)
                _hero.LevelExpirience(_enemyHero._AttributeVal[5] / 4);

            Console.Clear();
            Console.WriteLine(_hero.ToString() + "\n");
            Console.WriteLine("Ваше общее золото ({0})", heroGold);
            _enemyHero.isHeroesDead = true;
            Console.ReadKey();
        }

        private void shopHero()
        {
        Shop:
            int def = 200, att = 500, heal = 60;
            Console.Clear();
            Console.WriteLine("Приветстую тебя славный великий герой ({0})\n", _Hero.Name);
            Console.WriteLine("У меня есть хороший товар, можешь взгянуть");
            Console.WriteLine("1) сыромятная броня ({0}$) + 2 def\n2) крепкий стальной меч({1}$) + 3 att\n3) зелье здоровья({2}$) + 1\n4) Выход\n",
                def, att, heal);
            Console.WriteLine(_Hero.ToString());
            Console.WriteLine("У вас имеется [{0}$]", heroGold);
            if (heroGold == - att)
                goto StopShop;
            int var = 0;
            Int32.TryParse(Console.ReadLine(), out var);
            switch (var)
            {
                case 1:
                    if (def <= heroGold)
                    {
                        heroGold -= def;
                        _Hero.Def += 2;
                        Console.WriteLine("увеличина защита ({0})\nВаше текущее золото ({1})", 2, heroGold);
                        Console.WriteLine(_Hero.ToString());
                        Console.ReadKey();
                        goto Shop;
                    }
                    else
                    {
                        Console.WriteLine("У вас не хватает золота!");
                        Console.ReadKey();
                        goto Shop;
                    }
                case 2:
                    if (att <= heroGold)
                    {
                        heroGold -= att;
                        _Hero.Attack += 3;
                        goto Shop;
                    }
                    else
                    {
                        Console.WriteLine("У вас недостаточно средств");
                        Console.ReadKey();
                        goto Shop;
                    }
                case 3:
                    if (heal <= heroGold)
                    {
                        heroGold -= heal;
                        extraLive += 1;
                        goto Shop;
                    }
                    else
                    {
                        Console.WriteLine("У вас недостаточно средств");
                        Console.ReadKey();
                        goto Shop;
                    }
                case 4:
                    Game2();
                    break;
                default:
                    Console.WriteLine("Неверный выбор");
                    Console.ReadKey();
                    goto Shop;
            }
        StopShop:
            Console.ReadKey();
        }
    }
}
