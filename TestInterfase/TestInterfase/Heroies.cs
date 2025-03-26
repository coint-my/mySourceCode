using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestHero
{
    public class Heroies
    {
        protected string _name;
        protected string[] _stringAtribute;

        protected int[] _intAtribute = new int[2];
        private int _levet;
        protected int _intLive;
        protected int _expirense;
        public int _expFinish;
        private int _liveDead;
        public bool isHeroesDead = true;
        private int[] _intUron = new int[2];
        Random rand = new Random();

        public readonly int[] _AttributeVal = new int[6];

        private Heroies()
        {
            _AttributeVal[5] = (_expFinish = 400);
            _expirense = 0;
            _name = "Нет имени";
            _stringAtribute = new string[4] { "Аттака ", "Защита ", "Опыт ", "Урон" };
            _AttributeVal[0] = _intAtribute[0] = 3;
            _AttributeVal[1] = _intAtribute[1] = 4;
            _AttributeVal[2] = _intUron[0] = 3;
            _AttributeVal[3] = _intUron[1] = 5;
            _AttributeVal[4] = _intLive = 48;
            _levet = 1;
            LevelUp(1);
            _liveDead = _AttributeVal[4];
        }

        public Heroies(string name) : this()
        {
            this._name = name;
        }
        public Heroies(string name, int live, int level, int defence, int attak, int uron1, int uron2, int exp)
        {
            _AttributeVal[5] = (_expFinish = exp);
            _name = name;
            _AttributeVal[4] = (_intLive = live);
            _AttributeVal[1] = (_intAtribute[0] = defence);
            _AttributeVal[0] = (_intAtribute[1] = attak);
            _liveDead = _intLive;
            _AttributeVal[2] = (_intUron[0] = uron1);
            _AttributeVal[3] = (_intUron[1] = uron2);

            _stringAtribute = new string[4] { "Аттака ", "Защита ", "Опыт ", "Урон" };

            _levet = level;
            LevelUp(level);
        }

        public int HeroUron(int defence)
        {
            int heroUron = 0;

            int x = rand.Next(_intUron[0], _intUron[1]);
            heroUron = (x * _intAtribute[0]) * 3 / defence;

            return heroUron;
        }

        public int LiveHero
        {
            get { return _intLive; }
        }

        public int LiveInt
        {
            get { return _liveDead; }
            set
            {
                _liveDead = value;

                if (_liveDead < 1)
                {
                    isHeroesDead = false;
                    _liveDead = 0;
                }
                if (_liveDead > _intLive)
                    _liveDead = _intLive;
            }
        }
        public int LevelInt
        {
            get { return _levet; }
        }

        public int Attack
        {
            get { return _intAtribute[0]; }
            set { _intAtribute[0] = value; }
        }

        public int Def
        {
            get { return _intAtribute[1]; }
            set { _intAtribute[1] = value; }
        }

        public string Name
        {
            get { return _name; }
        }

        public int LevelUp(int level)
        {
            _levet = level;
            if (level != 1)
            {
                _intAtribute[0] = (_AttributeVal[0] + level - 1);
                _intAtribute[1] = (_AttributeVal[1] + level - 1);
                _intUron[0] = (_AttributeVal[2] * level * 5 / 7 % 50);
                _intUron[1] = (_AttributeVal[3] * level * 5 / 6 % 50);
                int x = (_AttributeVal[4] * level * 5 / 7);
                _intLive = (_AttributeVal[4] + x % 1000);
                _intLive = x;
                _liveDead = _intLive;
                _expFinish = (_AttributeVal[5] * level * 4 / 3);
                _expirense = 0;
            }
            return _levet;
        }

        private void UronUp(int levelUp)
        {
            for (int i = 0; i < _intUron.Length; i++)
            {
                int x = _intUron[i] + 1 * levelUp;
                _intUron[i] = x;
            }
        }

        public void LevelExpirience(int expirience)
        {
            _expirense += expirience;

            if (_expirense >= _expFinish)
            {
                int val = _levet + 1;
                LevelUp(val);
            }
        }

        public override string ToString()
        {
            return String.Format("Герой ({0}) / уровень {1}\n\n{2}  : {3}\n{4}     : {5} - {6}\n{7}  : {8}\nЗдоровье : [{9} / {10}]\n{11}    : {12} / {13}\n------------------------",
                _name, _levet, _stringAtribute[0], _intAtribute[0], _stringAtribute[3], _intUron[0], _intUron[1], 
                _stringAtribute[1], _intAtribute[1], _liveDead,
                _intLive, _stringAtribute[2], _expirense, _expFinish);
        }
        
     }
}
