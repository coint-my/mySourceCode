using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace console_CSarp_2019_lesson1
{
    struct Card
    {
        public string suit;
        public string rank;
    }
    class Deck
    {
        public const int size = 36;
        private Card[] cards;

        public Deck()
        {
            cards = new Card[size];

            for (int i = 0; i < 4; i++)
            {
                cards[i * 9 + 0].rank = "шесть";
                cards[i * 9 + 0].suit = GetSuit(i + 1);
                cards[i * 9 + 1].rank = "семь";
                cards[i * 9 + 1].suit = GetSuit(i + 1);
                cards[i * 9 + 2].rank = "восемь";
                cards[i * 9 + 2].suit = GetSuit(i + 1);
                cards[i * 9 + 3].rank = "девять";
                cards[i * 9 + 3].suit = GetSuit(i + 1);
                cards[i * 9 + 4].rank = "десять";
                cards[i * 9 + 4].suit = GetSuit(i + 1);
                cards[i * 9 + 5].rank = "валет";
                cards[i * 9 + 5].suit = GetSuit(i + 1);
                cards[i * 9 + 6].rank = "дама";
                cards[i * 9 + 6].suit = GetSuit(i + 1);
                cards[i * 9 + 7].rank = "король";
                cards[i * 9 + 7].suit = GetSuit(i + 1);
                cards[i * 9 + 8].rank = "туз";
                cards[i * 9 + 8].suit = GetSuit(i + 1);
            }

            Reshuffle_Deck(ref cards, (int)DateTime.Now.Ticks);
        }
        public ref Card[] GetCards { get { return ref cards; } }    
        private string GetSuit(int val)
        {
            switch (val)
            {
                case 1:
                    return "трефа";
                case 2:
                    return "пика";
                case 3:
                    return "бубна";
                case 4:
                    return "черва";
                default:
                    return "ошибка неправильное значение";
            }
        }
        public void Reshuffle_Deck(ref Card[] _cards, int _seed)
        {
            Random r = new Random(_seed);
            Card[] cards_new = new Card[size];
            bool[] cards_old_pos = new bool[size];

            for (int i = 0; i < size; i++)
            {
                int new_ind = r.Next(0, size);
                while (true)
                {
                    if (!cards_old_pos[new_ind])
                    {
                        cards_new[new_ind] = _cards[i];
                        cards_old_pos[new_ind] = true;
                        break;
                    }
                    else
                        new_ind = r.Next(0, size);
                }
            }

            for (int i = 0; i < size; i++)
            {
                _cards[i] = cards_new[i];
            }
        }
        public override string ToString()
        {
            string temp = "";
            int current = 0;
            while(current <= (size - 1))
            {
                temp += "карта " + cards[current].rank + " " + cards[current].suit + "\n";
                current++;
            }
            return temp;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(args);

            Deck deck = new Deck();

            Console.Write(deck);

            Console.WriteLine("-------------------------------");

            deck.Reshuffle_Deck(ref deck.GetCards, (int)DateTime.Now.Ticks);

            Console.WriteLine(deck);

            Console.ReadKey();
        }
    }
}
