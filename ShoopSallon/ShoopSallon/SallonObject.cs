using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoopSallon
{
    internal class SallonObject : IComparable
    {
        private string name;
        private string description;
        private DateTime date;
        private int id;
        private int money;
        private int count;

        public SallonObject()
        {
           DateTime date = DateTime.Now;
        }
        /// <summary>
        /// конструктор обьекта магазина
        /// </summary>
        /// <param name="_name">Имя товара</param>
        /// <param name="_decr">Описание товара</param>
        /// <param name="_date">Дата</param>
        /// <param name="_id">Идентификатор товара</param>
        /// <param name="_money">Стоимость товара</param>
        /// <param name="_count">Количество товара</param>
        public SallonObject(string _name, string _decr, DateTime _date, int _id, int _money, int _count)
        {
            name = _name;
            description = _decr;
            date = _date;
            id = _id;
            money = _money;
            count = _count;
        }

        #region Property
        /// <summary>
        /// Имя товара
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        /// <summary>
        /// Описание товара
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date
        {
            get { return date; }
        }
        /// <summary>
        /// Номер (ид) товара
        /// </summary>
        public int Id
        {
            get { return id; }
        }
        /// <summary>
        /// Стоимость товара
        /// </summary>
        public int Money
        {
            get { return money; }
        }
        /// <summary>
        /// Количество товара
        /// </summary>
        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        #endregion

        #region IComparable Members

        public int CompareTo(object obj)
        {
            SallonObject so = (SallonObject)obj;

            if (this.date > so.date)
                return 1;
            else if (this.date < so.date)
                return -1;

            return 0;
            
        }

        #endregion

        public override string ToString()
        {
            return string.Format(id.ToString() + date.Date.ToString("d") + name + description
            + money.ToString() + count.ToString());
        }
    }
}
