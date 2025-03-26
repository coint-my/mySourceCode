using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestHero
{
    class Program
    {
        static void Main()
        {
            Heroies hero = new Heroies("Vovan");

            Heroies[] enemyHero = new Heroies[10];
            enemyHero[0] = new Heroies("Волк", 32, 1, 1, 1, 1, 2, 65);
            enemyHero[1] = new Heroies("Паук", 34, 1, 2, 1, 2, 4, 83);
            enemyHero[2] = new Heroies("Скилет", 60, 1, 5, 2, 3, 4, 243);
            enemyHero[3] = new Heroies("Змея", 26, 1, 3, 2, 3, 5, 210);
            enemyHero[4] = new Heroies("Бык", 67, 1, 3, 2, 3, 5, 325);
            enemyHero[5] = new Heroies("Птица Рух", 84, 1, 5, 3, 4, 7, 390);
            enemyHero[6] = new Heroies("Орк", 56, 1, 5, 6, 4, 7, 312);
            enemyHero[7] = new Heroies("Пантера", 64, 1, 5, 5, 10, 14, 145);
            enemyHero[8] = new Heroies("Скорпион", 48, 1, 3, 8, 9, 13, 49);
            enemyHero[9] = new Heroies("Дракон", 120, 1, 13, 15, 17, 23, 400);

            ManagerGame manager = new ManagerGame(hero, enemyHero);
            manager.Game();

            Console.WriteLine("Вы проиграли!!!");
            Console.ReadKey();
        }
    }
}
