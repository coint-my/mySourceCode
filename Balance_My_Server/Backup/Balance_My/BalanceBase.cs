using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Balance_My
{
    public class BalanceBase : IComparable
    {
        /// <summary>
        /// сумма денег
        /// </summary>
        int money;
        /// <summary>
        /// дата число месяц год
        /// </summary>
        string date;
        /// <summary>
        /// категория зароботка
        /// </summary>
        string category;
        /// <summary>
        /// пожеланию описание
        /// </summary>
        string description;

        /// <summary>
        /// сумма денег
        /// </summary>
        public int Money
        {
            get { return money; }
        }

        public string DateBalance
        {
            get { return date; }
        }

        public string Category
        {
            get { return category; }
        }

        public string Description
        {
            get { return description; }
        }

        public BalanceBase(int _money, string _date, string _category, string _description)
        {
            money = _money;
            date = _date;
            category = _category;
            description = _description;
        }

        public int CompareTo(object obj)
        {
            BalanceBase bb = (BalanceBase)obj;

            DateTime dt = DateTime.Parse(this.date);
            DateTime d = DateTime.Parse(bb.date);

            if (dt > d)
                return 1;

            return -1;
        }

        public override string ToString()
        {
            return String.Format("{0} {1}", category, description);
        }
    }
}
