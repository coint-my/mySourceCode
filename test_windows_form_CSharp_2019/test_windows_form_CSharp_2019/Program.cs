using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_windows_form_CSharp_2019
{
    /// <summary>
    /// Ранг карты
    /// </summary>
    public enum RANK { ACE = 1, TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN, JACK, QUIN, KING }
    /// <summary>
    /// Масть карты
    /// </summary>
    public enum SUIT { CLUB = 1, HEART, PEAK, BUBA }
    /// <summary>
    /// Карта игральная
    /// </summary>
    public struct Card
    {
        public RANK rank;
        public SUIT suit;

        public PictureBox picture;
        public Panel panel;
        public Label label;

        /// <summary>
        /// Установить позицию карты
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        public void SetPosition(int x, int y) { panel.Location = new System.Drawing.Point(x, y); }

        public static bool operator >(Card c1, Card c2)
        {
            return (int)c1.rank > (int)c2.rank;
        }
        public static bool operator <(Card c1, Card c2)
        {
            return (int)c1.rank < (int)c2.rank;
        }
        public static bool operator ==(Card c1, Card c2)
        {
            return ((((int)c1.rank) == ((int)c2.rank)) && ((int)c1.suit) == ((int)c2.suit));
        }
        public static bool operator !=(Card c1, Card c2)
        {
            return !(c1 == c2);
        }

        public override string ToString()
        {
            return string.Format(rank + " " + suit);
        }
    }
    /// <summary>
    /// Колода Карт
    /// </summary>
    public class Deck
    {
        /// <summary>
        /// количество карт в колоде
        /// </summary>
        private const int count = 52;
        /// <summary>
        /// количество карт в колоде
        /// </summary>
        public int Count { get { return count; } }      
        /// <summary>
        /// Список карт
        /// </summary>
        private List<Card> cards;
        /// <summary>
        /// хранится изображение карт
        /// </summary>
        private PictureBox pictureBox;
        public Deck()
        {
            pictureBox = new PictureBox();
            pictureBox.Load("texture//playing-cards.png");
            cards = new List<Card>(count);
            //инициализация колоды карт
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 13; x++)
                {
                    Card card = new Card();
                    PictureBox p = new PictureBox();
                    card.picture = p;
                    card.picture.Image = pictureBox.Image;
                    int wid = p.Image.Width / 13;
                    int hei = p.Image.Height / 4;
                    p.SizeMode = PictureBoxSizeMode.AutoSize;
                    p.SetBounds(-(wid * x), -(hei * y), p.Image.Width, p.Image.Height);
                    card.panel = new Panel();
                    card.picture.Parent = card.panel;
                    card.suit = (SUIT)y + 1;
                    card.rank = (RANK)x + 1;
                    card.panel.SetBounds(0, 0, wid, hei);
                    card.picture.Name = "" + card.rank + " " + card.suit;
                    card.panel.Name = "none";
                    card.picture.Tag = card;

                    cards.Add(card);
                }
            }
        }
        /// <summary>
        /// Перемешивание карт
        /// </summary>
        /// <param name="_cards">Ссылка на массив одномерный карт</param>
        /// <param name="_seed">Случайное зерно для перемешивания карт. Желательно привязать ко времени прошедших милисекунд, или cloak.</param>
        public void Reshuffle_Deck(int _seed)
        {
            Random r = new Random(_seed);
            Card[] cards_new = new Card[Count];
            bool[] cards_old_pos = new bool[Count];

            for (int i = 0; i < Count; i++)
            {
                int new_ind = r.Next(0, Count);
                while (true)
                {
                    if (!cards_old_pos[new_ind])
                    {
                        cards_new[new_ind] = cards[i];
                        cards_old_pos[new_ind] = true;
                        break;
                    }
                    else
                        new_ind = r.Next(0, Count);
                }
            }

            for (int i = 0; i < Count; i++)
            {
                cards[i] = cards_new[i];
            }
        }
        /// <summary>
        /// Перемешивание карт
        /// </summary>
        /// <param name="_cards">Ссылка на массив одномерный карт</param>
        /// <param name="_seed">Случайное зерно для перемешивания карт. Желательно привязать ко времени прошедших милисекунд, или cloak.</param>
        public void Reshuffle_Deck(ref List<Card> _cards, int _seed)
        {
            Random r = new Random(_seed);
            Card[] cards_new = new Card[Count];
            bool[] cards_old_pos = new bool[Count];

            for (int i = 0; i < Count; i++)
            {
                int new_ind = r.Next(0, Count);
                while (true)
                {
                    if (!cards_old_pos[new_ind])
                    {
                        cards_new[new_ind] = _cards[i];
                        cards_old_pos[new_ind] = true;
                        break;
                    }
                    else
                        new_ind = r.Next(0, Count);
                }
            }

            for (int i = 0; i < Count; i++)
            {
                _cards[i] = cards_new[i];
            }
        }
        /// <summary>
        /// карта из колоды
        /// </summary>
        /// <param name="_ind">номер карты</param>
        /// <returns>Карту</returns>
        public Card GetCardRef(int _ind)
        {
            if((_ind >= 0) && (_ind <= cards.Count))
            {
                return cards[_ind];
            }
            else
            {
                MessageBox.Show("выход за пределы массива в колоде", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new Card();
            }
        }
    }
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
