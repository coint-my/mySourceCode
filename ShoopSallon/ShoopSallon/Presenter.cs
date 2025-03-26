using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoopSallon
{
    internal static class Presenter
    {
        private static List<SallonObject> listSalle = new List<SallonObject>();

        private static List<SallonObject> listStorage = new List<SallonObject>();

        private static List<SallonObject> listCredit = new List<SallonObject>();

        /// <summary>
        /// Список продаж
        /// </summary>
        public static List<SallonObject> ListSalle
        {
            get { return listSalle; }
        }

        /// <summary>
        /// Список закупок
        /// </summary>
        public static List<SallonObject> ListStorage
        {
            get { return listStorage; }
        }

        /// <summary>
        /// Список расходов
        /// </summary>
        public static List<SallonObject> ListCredit
        {
            get { return listCredit; }
        }

        /// <summary>
        /// Декрементация списка закупок
        /// </summary>
        /// <param name="_id">ид элемента</param>
        /// <param name="_count">количество штук</param>
        /// <param name="_description">новое описание</param>
        public static void DecrementCountID(int _id, int _count, string _description)
        {
            for (int i = 0; i < ListStorage.Count; i++)
            {
                if (_id == ListStorage[i].Id)
                {
                    listStorage[i].Count -= _count;
                    listStorage[i].Description = _description;
                }
            }
        }

        /// <summary>
        /// Удаление обьекта
        /// </summary>
        /// <param name="_obj">обьект списка</param>
        public static void RemoveObject(SallonObject _obj)
        {
            listStorage.Remove(_obj);
        }

        /// <summary>
        /// добавить в список продаж
        /// </summary>
        /// <param name="obj">обьект продаж</param>
        public static void AddSalle(SallonObject obj)
        {
            listSalle.Add(obj);
        }

        /// <summary>
        /// Добавить в список расходов
        /// </summary>
        /// <param name="_obj">Обьект расходов</param>
        public static void AddCredit(SallonObject _obj)
        {
            listCredit.Add(_obj);
        }

        /// <summary>
        /// добавить колекцию продаж в список
        /// </summary>
        /// <param name="listObj">колекция</param>
        public static void LoadListSalle(List<SallonObject> listObj)
        {
            listSalle = listObj;
        }

        /// <summary>
        /// добавить колекцию закупок в список
        /// </summary>
        /// <param name="listObj">колекция</param>
        public static void LoadListStore(List<SallonObject> listObj)
        {
            listStorage = listObj;
        }

        /// <summary>
        /// Добавить в колекцию расходов
        /// </summary>
        /// <param name="_listObj">колекция расходов</param>
        public static void LoadListCredit(List<SallonObject> _listObj)
        {
            listCredit = _listObj;
        }

        /// <summary>
        /// Самая дорогая вещь
        /// </summary>
        /// <returns>сумма</returns>
        public static string WinerCost()
        {
            int wCost = 0;
            string s = "";

            for (int i = 0; i < ListSalle.Count; i++)
            {
                if (ListSalle[i].Money > wCost)
                {
                    wCost = ListSalle[i].Money;
                    s = ListSalle[i].Name;
                }
            }

            return String.Format("\"{0}\" = {1} грн.", s, wCost);
        }
    }
}
