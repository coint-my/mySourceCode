using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace Balance_My
{
    public class ForAnalizator : IComparable
    {
        public DateTime date;
        public int debit;
        public int credit;

        public int CompareTo(object obj)
        {
            ForAnalizator fa = (ForAnalizator)obj;
            DateTime dt = fa.date;
            if (date > dt)
                return 1;
            else if (date == dt)
                return 0;
            return -1;
        }
    }
    public struct ForAnalys
    {
        public DateTime date;
        public int balance;
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
        /// удаление из базы
        /// </summary>
        /// <param name="index">Индекс масива</param>
        public static void removeBalance(int index)
        {
            listBalance.RemoveAt(index);
            //listBalance.Sort();
        }

        public static void removeCredit(int index)
        {
            listCredit.RemoveAt(index);
            //listBalance.Sort();
        }

        /// <summary>
        /// изменить обьект
        /// </summary>
        /// <param name="balance">Сам Объект</param>
        /// <param name="index">Индекс Базы Данных</param>
        public static void setBalance(BalanceBase balance, int index)
        {
            listBalance[index] = balance;
            //listBalance.Sort();
        }

        public static void setCredit(BalanceBase balance, int index)
        {
            listCredit[index] = balance;
            //listBalance.Sort();
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

        private void Initialize()
        {
            BinaryReader br = new BinaryReader(File.Open(Form1.DirSorce + "\\" + Form1.FileAccount + ".dat", FileMode.Open));

            try
            {
                Int32 count_category = br.ReadInt32();
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
                //MessageBox.Show("Загрузка прошла успешно!", "загрузка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            br.Close();
        }

        private void LoadBalance()
        {
            BinaryReader br = new BinaryReader(File.Open(Form1.DirSorce + "\\" + Form1.FileAccount + ".dat", FileMode.Open));

            Int32 count_category = br.ReadInt32();
            for (int i = 0; i < count_category; i++)
            {
                Settings.category_list.Add(br.ReadString());
            }

            //загружаю шаблон на описание расхода
            Int32 count_tamplate = br.ReadInt32();
            for (int i = 0; i < count_tamplate; i++)
            {
                Settings.tamplate_credit.Add(br.ReadString());
            }
            //-----------------------------------

            int countDebit = br.ReadInt32();
            int countCredit = br.ReadInt32();

            for (int i = 0; i < countDebit; i++)
            {
                string date = br.ReadString();
                string category = br.ReadString();
                int mony = br.ReadInt32();
                string descr = br.ReadString();
                BalanceBase bb = new BalanceBase(mony, date, category, descr);
                listBalance.Add(bb);
            }

            for (int i = 0; i < countCredit; i++)
            {
                int mony = br.ReadInt32();
                string date = br.ReadString();
                string descr = br.ReadString();
                BalanceBase bb = new BalanceBase(mony, date, "", descr);
                listCredit.Add(bb);
            }

            br.Close();
        }

        /// <summary>
        /// сохранить доходы и расходы
        /// </summary>
        internal static void SaveListBalance(string fileName)
        {
            BinaryWriter bw = new BinaryWriter(File.Open(fileName, FileMode.OpenOrCreate));

            bw.Write(Settings.category_list.Count);
            for (int i = 0; i < Settings.category_list.Count; i++)
            {
                bw.Write(Settings.category_list[i]);
            }

            //сохраняю шаблон на описание расхода (новое)
            bw.Write(Settings.tamplate_credit.Count);
            for (int i = 0; i < Settings.tamplate_credit.Count; i++)
            {
                bw.Write(Settings.tamplate_credit[i]);
            }
            //-------------------------------------------

            bw.Write(listBalance.Count);
            bw.Write(listCredit.Count);

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

            SaveListBalanceTXT(ListBalance, ListCredit);
        }

        private static bool SaveListBalanceTXT(List<BalanceBase> balance, List<BalanceBase> credit)
        {
            using (StreamWriter sw = new StreamWriter(Form1.DirSorce + "text\\" + Form1.FileAccount + ".txt"))
            {
                sw.WriteLine(Settings.category_list.Count);
                
                for (int i = 0; i < Settings.category_list.Count; i++)
                {
                    sw.WriteLine(Settings.category_list[i]);
                }

                sw.WriteLine(listBalance.Count);
                sw.WriteLine(ListCredit.Count);

                for (int i = 0; i < listBalance.Count; i++)
                {
                    sw.WriteLine(listBalance[i].DateBalance + "|" + listBalance[i].Category + "|" + listBalance[i].Money +
                        "|" + listBalance[i].Description);
                }

                for (int i = 0; i < listCredit.Count; i++)
                {
                    sw.WriteLine(listCredit[i].DateBalance + "|" + listCredit[i].Money + "|" + listCredit[i].Description);
                }
            }

            return true;
        }

        public static bool SaveListBalanceTXT(string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine(Settings.category_list.Count);

                for (int i = 0; i < Settings.category_list.Count; i++)
                {
                    sw.WriteLine(Settings.category_list[i]);
                }

                sw.WriteLine(listBalance.Count);
                sw.WriteLine(ListCredit.Count);

                for (int i = 0; i < listBalance.Count; i++)
                {
                    sw.WriteLine(listBalance[i].DateBalance + "|" + listBalance[i].Category + "|" + listBalance[i].Money +
                        "|" + listBalance[i].Description);
                }

                for (int i = 0; i < listCredit.Count; i++)
                {
                    sw.WriteLine(listCredit[i].DateBalance + "|" + listCredit[i].Money + "|" + listCredit[i].Description);
                }
            }

            return true;
        }

        public PresenterForm()
        {
            listBalance = new List<BalanceBase>();
            listCredit = new List<BalanceBase>();
            bufferMounth = new List<BalanceBase>();
            BufferMounthCredit = new List<BalanceBase>();

            //Initialize();
            LoadBalance();
            LoadSumMounth();

            listBalance.Sort();
        }

        ~PresenterForm()
        {
            
        }
        /// <summary>
        /// Получить доход расход и дату помесячно
        /// </summary>
        /// <returns>Возврат списка помесячных доходов и расходов</returns>
        public static List<ForAnalizator> my_get_parser_month()
        {
            List<ForAnalizator> temp_analys = null;// Список доходов и расходов в месяц
            if (ListBalance.Count > 0)
            {
                temp_analys = new List<ForAnalizator>();
                int debit = 0;
                listBalance.Sort(); //Сортируем все доходы по дате
                DateTime old_dt = DateTime.Parse(ListBalance[0].DateBalance);// Старая дата
                DateTime curr_dt = old_dt;// Текущяя дата
                for (int ind = 0; ind < listBalance.Count; ind++)
                {
                    curr_dt = DateTime.Parse(ListBalance[ind].DateBalance);// Записываю текущюю дату
                    if (old_dt.Month == curr_dt.Month && old_dt.Year == curr_dt.Year)// Проверка на месяц и год
                        debit += listBalance[ind].Money;
                    else //Создаём запись дохода за месяц
                    {
                        ForAnalizator analizator = new ForAnalizator();
                        analizator.date = old_dt;
                        analizator.debit = debit;
                        temp_analys.Add(analizator);

                        debit = listBalance[ind].Money;
                        old_dt = curr_dt;
                    }
                }
                ForAnalizator analizator1 = new ForAnalizator();// Последний месяц из общего списка
                analizator1.date = curr_dt;
                analizator1.debit = debit;
                temp_analys.Add(analizator1);
                //----------------------------------------------------
                listCredit.Sort(); //Сортируем все расходы по дате
                old_dt = DateTime.Parse(ListCredit[0].DateBalance);// Записываю предыдущую дату
                debit = 0;// Обнуляю расходы
                List<ForAnalizator> list_credit = new List<ForAnalizator>();
                for (int ind = 0; ind < listCredit.Count; ind++)
                {
                    curr_dt = DateTime.Parse(ListCredit[ind].DateBalance);// Записываю текущюю дату
                    if (old_dt.Month == curr_dt.Month && old_dt.Year == curr_dt.Year)// Проверка на месяц и год
                        debit += listCredit[ind].Money;
                    else
                    {
                        ForAnalizator analizator = new ForAnalizator();
                        analizator.date = old_dt;
                        analizator.credit = debit;
                        list_credit.Add(analizator);

                        debit = listCredit[ind].Money;
                        old_dt = curr_dt;
                    }
                }
                ForAnalizator analizator2 = new ForAnalizator();// Последний месяц из общего списка
                analizator2.date = curr_dt;
                analizator2.credit = debit;
                list_credit.Add(analizator2);
                List<ForAnalizator> temp_fa = new List<ForAnalizator>();
                for (int cred = 0; cred < list_credit.Count; cred++)
                {
                    int ind = 0;
                    for (ind = 0; ind < temp_analys.Count; ind++)
                    {
                        if(temp_analys[ind].date.Month == list_credit[cred].date.Month &&
                            temp_analys[ind].date.Year == list_credit[cred].date.Year)
                        {
                            temp_analys[ind].credit = list_credit[cred].credit;                            
                            break;
                        }
                    }

                    if(ind == temp_analys.Count)
                    {
                        ForAnalizator fa = new ForAnalizator();
                        fa.credit = list_credit[cred].credit;
                        fa.date = list_credit[cred].date;
                        temp_fa.Add(fa);
                    }
                }
                temp_analys.AddRange(temp_fa);
                list_credit.Clear();
            }
            temp_analys.Sort(); //Сортируем месячные списки

            return temp_analys;
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

        public static List<ForAnalys> CounterSumMounth(List<BalanceBase> _balance)
        {
            List<ForAnalys> analys = new List<ForAnalys>();

            List<BalanceBase> tempBalance = new List<BalanceBase>(_balance);

            List<BalanceBase> mounthBalance = new List<BalanceBase>();

            while (tempBalance.Count > 0)
            {
                DateTime tempDate = DateTime.Parse(tempBalance[tempBalance.Count - 1].DateBalance);
                List<int> remove = new List<int>();

                for (int i = tempBalance.Count - 1; i >= 0; i--)
                {
                    BalanceBase bb = tempBalance[i];
                    DateTime locDate = DateTime.Parse(bb.DateBalance);

                    if((tempDate.Year == locDate.Year) && (tempDate.Month == locDate.Month))
                    {
                        mounthBalance.Add(bb);
                        remove.Add(i);
                    }
                }

                for (int i = 0; i < remove.Count; i++)
                    tempBalance.RemoveAt(remove[i]);

                ForAnalys result = new ForAnalys();
                result.date = DateTime.Parse(mounthBalance[0].DateBalance);
                for (int i = 0; i < mounthBalance.Count; i++)
                {
                    BalanceBase bb = mounthBalance[i];
                    result.balance += bb.Money;                    
                }
                analys.Add(result);

                mounthBalance.Clear();
            }

            return analys;
        }

        public static void my_show_DebitCreditMountAnalize(string _mounth, string _year, bool _isDebit)
        {
            string str_calculate = "01." + _mounth + "." + _year;

            PresenterForm.BufferDateTime = DateTime.Parse(str_calculate);
            PresenterForm.BufferMounth.Clear();
            PresenterForm.BufferMounthCredit.Clear();

            if (_isDebit)
            {
                for (int deb = 0; deb < PresenterForm.ListBalance.Count; deb++)
                {
                    BalanceBase _bb = PresenterForm.ListBalance[deb];
                    DateTime _dt = DateTime.Parse(_bb.DateBalance);

                    if ((_dt.Month == PresenterForm.BufferDateTime.Month) && (_dt.Year == PresenterForm.BufferDateTime.Year))
                    {
                        PresenterForm.BufferMounth.Add(_bb);
                    }
                }

                FormDebitMounthAnaliz fdma = new FormDebitMounthAnaliz();
                fdma.ShowDialog();
            }
            else
            {
                for (int cre = 0; cre < PresenterForm.ListCredit.Count; cre++)
                {
                    BalanceBase _bb = PresenterForm.ListCredit[cre];
                    DateTime _dt = DateTime.Parse(_bb.DateBalance);

                    if ((_dt.Month == PresenterForm.BufferDateTime.Month) && (_dt.Year == PresenterForm.BufferDateTime.Year))
                    {
                        PresenterForm.BufferMounthCredit.Add(_bb);
                    }
                }

                FormCreditMounthAnalize fcma = new FormCreditMounthAnalize();
                fcma.ShowDialog();
            }
        }

        public static void my_graph_radial_show(ref Graphics _g, int _debit, int _credit, Point _pos, string _nameMounth)
        {
            int size = 100;

            float divider = (_debit + _credit) / 360.0f;
            float result_rad_red = (_credit / divider);

            _g.FillPie(Brushes.Green, new Rectangle(_pos.X, _pos.Y, size, size), 0, 360);
            _g.FillPie(Brushes.Red, new Rectangle(_pos.X, _pos.Y, size, size), 0,
                result_rad_red);

            float percent_green = result_rad_red / 360.0f;
            string name_green = String.Format("{0:#.00}", percent_green).Substring(1) + "%";
            Font font = new Font("Arial", 16, FontStyle.Bold);

            float pos_centerX = _pos.X + (size / 2);
            float pos_centerY = _pos.Y + (size / 2);

            myDrawString(_nameMounth, pos_centerX, pos_centerY - (size + 15) / 2, ref _g, ref font);

            float pos_green_percentX = (float)Math.Cos((result_rad_red / 2.0d) * (Math.PI / 180)) * 25 + pos_centerX;
            float pos_green_percentY = (float)Math.Sin((result_rad_red / 2.0d) * (Math.PI / 180)) * 25 + pos_centerY;

            myDrawString(name_green, pos_green_percentX, pos_green_percentY, ref _g, ref font);

            float pos_red_percentX = (float)Math.Cos(((Math.Abs(result_rad_red - 360) / 2f) + result_rad_red) *
                (Math.PI / 180)) * 25 + pos_centerX;
            float pos_red_percentY = (float)Math.Sin(((Math.Abs(result_rad_red - 360) / 2f) + result_rad_red) *
                (Math.PI / 180)) * 25 + pos_centerY;
            string name_red = String.Format("{0:#.00}", Math.Abs(percent_green - 1.0f)).Substring(1) + "%";

            myDrawString(name_red, pos_red_percentX, pos_red_percentY, ref _g, ref font);
        }

        public static void my_graph_radial_test(ref PictureBox _picture, int _rad)
        {
            Graphics myGraphic = _picture.CreateGraphics();
            myGraphic.Clear(Color.White);

            //bool is_ok = _debit - _credit > 0;

            int posX = 150;
            int posY = 100;
            int size = 100;

            //float divider = (_debit + _credit) / 360.0f;
            float result_rad_red = _rad;

            myGraphic.FillPie(Brushes.Green, new Rectangle(posX, posY, size, size), 0, 360);
            myGraphic.FillPie(Brushes.Red, new Rectangle(posX, posY, size, size), 0,
                result_rad_red);

            float percent_green = result_rad_red / 360.0f;
            string name_green = String.Format("{0:#.00}", percent_green).Substring(1) + "%";
            Font font = new Font("Arial", 16, FontStyle.Bold);

            float pos_centerX = posX + (size / 2);
            float pos_centerY = posY + (size / 2);

            float pos_green_percentX = (float)Math.Cos((result_rad_red / 2.0d) * (Math.PI / 180)) * 25 + pos_centerX;
            float pos_green_percentY = (float)Math.Sin((result_rad_red / 2.0d) * (Math.PI / 180)) * 25 + pos_centerY;

            myDrawString(name_green, pos_green_percentX, pos_green_percentY, ref myGraphic, ref font);

            float pos_red_percentX = (float)Math.Cos(((Math.Abs(result_rad_red - 360) / 2f) + result_rad_red) * 
                (Math.PI / 180)) * 25 + pos_centerX;
            float pos_red_percentY = (float)Math.Sin(((Math.Abs(result_rad_red - 360) / 2f) + result_rad_red) *
                (Math.PI / 180)) * 25 + pos_centerY;
            string name_red = String.Format("{0:#.00}", Math.Abs(percent_green - 1.0f)).Substring(1) + "%";

            myDrawString(name_red, pos_red_percentX, pos_red_percentY, ref myGraphic, ref font);
        }

        private static void myDrawString(string _name, float _x, float _y, ref Graphics _graphic, ref Font _font)
        {
            float font_size = _font.Size * _name.Length;
            _graphic.DrawString(_name, _font, Brushes.Violet, new PointF(
                _x - (font_size / 2), _y - ((float)_font.Height / 2)));
        }
    }
}
