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
        /// категория зарaботка
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

        public BalanceBase(BalanceBase _balance)
        {
            date = _balance.DateBalance;
            category = _balance.Category;
            money = _balance.money;
            description = _balance.Description;
        }

        //public BalanceBase(BalanceBase _balance)
        //{
        //    date = new string(_balance.DateBalance.ToCharArray());
        //    category = new string(_balance.Category.ToCharArray());
        //    money = new IntPtr(_balance.money).ToInt32();
        //    description = new string(_balance.Description.ToCharArray());
        //}

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
