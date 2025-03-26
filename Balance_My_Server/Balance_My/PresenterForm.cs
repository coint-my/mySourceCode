using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Net.Sockets;

namespace Balance_My
{
    public struct ForAnalys
    {
        public DateTime date;
        public int money;
    }
    public class PresenterForm
    {
        private static List<BalanceBase> listBalance;
        private static List<BalanceBase> listCredit;
        private static List<BalanceBase> bufferMounth;

        /// <summary>
        /// буфер временной даты
        /// </summary>
        public static DateTime BufferDateTime { get; set; }

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

        /// <summary>
        /// добавить в буфер временных месячных данных дохода
        /// </summary>
        public static List<BalanceBase> BufferMounth
        {
            get { return bufferMounth; }
            set { bufferMounth = value; }       
        }

        /// <summary>
        /// добавить в буфер времменых месячных данных расхода
        /// </summary>
        public static List<BalanceBase> BufferMounthCredit { get; set; }       

        private void Initialize(BinaryReader _br, BinaryWriter _bw, NetworkStream _stream)
        {
            bool isFlag = false;

            BinaryReader br = new BinaryReader(File.Create("test"));
            try
            {
                int count = _br.ReadInt32();
                if(count == 0)
                {
                    br.Close();
                    _bw.Write(false);
                    return;
                }
                byte[] temp = new byte[count];
                _stream.Read(temp, 0, count);

                br.BaseStream.Write(temp, 0, count);
                br.BaseStream.Seek(0, SeekOrigin.Begin);
                //------------------------------------

                int count_category = br.ReadInt32();
                for (int i = 0; i < count_category; i++)
                {
                    Settings.category_list.Add(br.ReadString());
                }

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
                        isFlag = true;
                        int mony = br.ReadInt32();
                        string date = br.ReadString();
                        string descr = br.ReadString();
                        BalanceBase bb = new BalanceBase(mony, date, "", descr);
                        listCredit.Add(bb);
                    }
                    cout++;
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message, "загрузка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //MessageBox.Show("Загрузка прошла успешно! " + DateTime.Now.ToString(), "загрузка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            _bw.Write(isFlag);
            br.Close();
        }
        /// <summary>
        /// сохранить доходы и расходы
        /// </summary>
        internal static void SaveListBalance(NetworkStream _clientStream, BinaryWriter _bw, BinaryReader _br)
        {
            BinaryWriter bw = new BinaryWriter(File.Create("testSave"));

            try
            {
                bw.Write(Settings.category_list.Count);
                for (int i = 0; i < Settings.category_list.Count; i++)
                {
                    bw.Write(Settings.category_list[i]);
                }

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
                
                _bw.Write("save");
                //отправить данные на сервер
                bw.Seek(0, SeekOrigin.Begin);
                byte[] b = new byte[bw.BaseStream.Length];
                int tmp = bw.BaseStream.Read(b, 0, b.Length);
                MessageBox.Show("база данных (" + b.Length + ")байт.\nУспешно сохранено.", "сохранение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _bw.Write(b.Length);
                                
                _clientStream.Write(b, 0, (int)bw.BaseStream.Length);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "сохранение" + bw.BaseStream.Length, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            bw.Close();
        }

        public PresenterForm(BinaryReader _br, BinaryWriter _bw, NetworkStream _cStream)
        {
            listBalance = new List<BalanceBase>();
            listCredit = new List<BalanceBase>();
            bufferMounth = new List<BalanceBase>();
            BufferMounthCredit = new List<BalanceBase>();

            Initialize(_br, _bw, _cStream);
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
        public static string LoadSumMounth()
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
        public static List<string> LoadSumMounth(List<BalanceBase> list)
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

        public static List<ForAnalys> LoadSumMounth(List<BalanceBase> list, bool _flag)
        {
            DateTime dt = DateTime.Parse(list[0].DateBalance);

            List<ForAnalys> mounth = new List<ForAnalys>();

            int summa = 0;

            bool flag = false;
            ForAnalys analys = new ForAnalys();

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
                    analys.date = dt;
                    analys.money = summa;
                    mounth.Add(analys);
                    summa = 0;
                    dt = d;
                    flag = false;
                    summa += list[i].Money;
                }

                analys.date = dt;
                analys.money = summa;

                if (list.Count - 1 == i)
                    mounth.Add(analys);
            }

            if (mounth.Count == 0)
                mounth.Add(analys);

            return mounth;
        }

        public static List<int> LoadSumMounthInt(List<BalanceBase> list)
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
