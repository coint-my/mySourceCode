using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Balance_My
{
    public class PresenterForm
    {
        private static List<BalanceBase> listBalance;
        private static List<BalanceBase> listCredit;
        /// <summary>
        /// список доходов только для чтения
        /// </summary>
        public static List<BalanceBase> ListBalance
        {
            get { return listBalance; }
        }
        /// <summary>
        /// список расходов только для чтения
        /// </summary>
        public static List<BalanceBase> ListCredit
        {
            get { return listCredit; }
        }
        /// <summary>
        /// добавить в список расход
        /// </summary>
        /// <param name="bb">тип баланса</param>
        public static void AddCredit(BalanceBase bb)
        {
            listCredit.Add(bb);
        }
        /// <summary>
        /// добавить в список доход
        /// </summary>
        /// <param name="bb">тип баланса</param>
        public static void AddBalance(BalanceBase bb)
        {
            listBalance.Add(bb);
            listBalance.Sort();
            
        }

        private void Initialize()
        {
            BinaryReader br = new BinaryReader(File.Open(Form1.DirSorce + "\\" + Form1.FileAccount, FileMode.Open));

            try
            {
                int cout = 0;
                int coutStop = br.ReadInt32();

                while (true)
                {
                    if (cout < coutStop)
                    {
                        string date = br.ReadString();
                        string category = br.ReadString();
                        int mony = br.ReadInt32();
                        string descr = br.ReadString();
                        BalanceBase bb = new BalanceBase(mony, date, category, descr);
                        listBalance.Add(bb);
                    }
                    else
                    {
                        int mony = br.ReadInt32();
                        string date = br.ReadString();
                        string descr = br.ReadString();
                        BalanceBase bb = new BalanceBase(mony, date, "", descr);
                        listCredit.Add(bb);
                    }
                    cout++;
                }
            }
            catch (EndOfStreamException ex)
            {
                MessageBox.Show("Загрузка прошла успешно!", "загрузка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            br.Close();
        }
        /// <summary>
        /// сохранить доходы и расходы
        /// </summary>
        internal static void SaveListBalance()
        {
            BinaryWriter bw = new BinaryWriter(File.Open(Form1.DirSorce + "\\" + Form1.FileAccount, FileMode.Open));

            bw.Write(listBalance.Count);

            for (int i = 0; i < listBalance.Count; i++)
            {
                bw.Write(listBalance[i].DateBalance);
                bw.Write(listBalance[i].Category);
                bw.Write(listBalance[i].Money);
                bw.Write(listBalance[i].Description);
            }

            for (int j = 0; j < listCredit.Count; j++)
            {
                bw.Write(listCredit[j].Money);
                bw.Write(listCredit[j].DateBalance);
                bw.Write(listCredit[j].Description);
            }

            bw.Close();
        }

        public PresenterForm()
        {
            listBalance = new List<BalanceBase>();
            listCredit = new List<BalanceBase>();

            Initialize();
            LoadSumMounth();

            listBalance.Sort();
        }

        ~PresenterForm()
        {
            
        }
        /// <summary>
        /// сумма дохода за текущий месяц
        /// </summary>
        /// <returns>строка месяц сумма</returns>
        internal static string LoadSumMounth()
        {
            DateTime dt = DateTime.Now;

            int summa = 0;

            for (int i = 0; i < listBalance.Count; i++)
            {
                DateTime d = DateTime.Parse(listBalance[i].DateBalance);

                if (dt.Month == d.Month && d.Year == dt.Year)
                {
                    summa += listBalance[i].Money;
                }
            }

            return string.Format("сумма за ({0}) : {1} грн.", dt.ToString("y"), summa);
        }
        /// <summary>
        /// сумма дохода за все месяца
        /// </summary>
        /// <param name="list">сумма дохода за все месяца</param>
        /// <returns>масив строк месяцев</returns>
        internal static List<string> LoadSumMounth(List<BalanceBase> list)
        {
            DateTime dt = DateTime.Parse(list[0].DateBalance);

            List<string> mounth = new List<string>();

            int summa = 0;

            bool flag = false;

            for (int i = 0; i < list.Count; i++)
            {
                DateTime d = DateTime.Parse(list[i].DateBalance);

                if (dt.Month != d.Month || dt.Month == d.Month && dt.Year != d.Year)
                {
                    flag = true;
                }

                if (!flag)
                {
                    summa += list[i].Money;
                }
                else
                {
                    mounth.Add(String.Format("{0} итог : {1} грн.", dt.ToString("y"), summa));
                    summa = 0;
                    dt = d;
                    flag = false;
                    summa += list[i].Money;
                }

                if (list.Count -1 == i)
                    mounth.Add(String.Format("{0} итог : {1} грн.", dt.ToString("y"), summa));
            }

            if(mounth.Count == 0)
                mounth.Add(String.Format("{0} итог : {1} грн.", dt.ToString("y"), summa));

            return mounth;
        }

        internal static List<int> LoadSumMounthInt(List<BalanceBase> list)
        {
            DateTime dt = DateTime.Parse(list[0].DateBalance);

            List<int> mounth = new List<int>();

            int summa = 0;

            bool flag = false;

            for (int i = 0; i < list.Count; i++)
            {
                DateTime d = DateTime.Parse(list[i].DateBalance);

                if (dt.Month != d.Month || dt.Month == d.Month && dt.Year != d.Year)
                {
                    flag = true;
                }

                if (!flag)
                {
                    summa += list[i].Money;
                }
                else
                {
                    mounth.Add(summa);
                    summa = 0;
                    dt = d;
                    flag = false;
                    summa += list[i].Money;
                }

                if (list.Count - 1 == i)
                    mounth.Add(summa);
            }

            if (mounth.Count == 0)
                mounth.Add(summa);

            return mounth;
        }
    }
}
