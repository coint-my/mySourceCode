using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace ShoopSallon
{
    internal class ModelSallon
    {
        /// <summary>
        /// Импортёр базы продаж
        /// </summary>
        /// <param name="_fileName">Путь к файлу</param>
        /// <returns>Список записей</returns>
        public List<SallonObject> ImportBaseColectionSalle(string _fileName)
        {
            List<SallonObject> list = new List<SallonObject>();

            try
            {
                StreamReader sr = new StreamReader(File.Open(_fileName, FileMode.Open));

                string[] tempString = new string[sr.ReadLine().Split('_').Length];

                while (!sr.EndOfStream)
                {
                    tempString = sr.ReadLine().Split('_');

                    if (tempString[0][0] != '$')
                    {
                        int id = int.Parse(tempString[1]);
                        int money = int.Parse(tempString[4]);
                        int count = int.Parse(tempString[5]);

                        SallonObject sallone = new SallonObject(tempString[2], tempString[3], DateTime.Parse(tempString[0]), id,
                            money, count);

                        list.Add(sallone);
                    }
                }

                sr.Close();
                sr.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return list;
        }
        /// <summary>
        /// Импортёр базы Закупок
        /// </summary>
        /// <param name="_fileName">Путь к файлу</param>
        /// <returns>Список записей</returns>
        public List<SallonObject> ImportBaseColectionStorage(string _fileName)
        {
            List<SallonObject> list = new List<SallonObject>();

            try
            {
                StreamReader sr = new StreamReader(File.Open(_fileName, FileMode.Open));

                string[] tempString = new string[sr.ReadLine().Split('_').Length];

                int id = 0;

                while (!sr.EndOfStream)
                {
                    tempString = sr.ReadLine().Split('_');

                    id++;
                    int money = int.Parse(tempString[2]);
                    int count = int.Parse(tempString[4]);

                    SallonObject sallone = new SallonObject(tempString[0], tempString[1], DateTime.Parse(tempString[3]), id,
                        money, count);

                    list.Add(sallone);
                }

                sr.Close();
                sr.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return list;
        }
        /// <summary>
        /// Сохранение списка продаж
        /// </summary>
        /// <param name="_listSalle">Список продаж</param>
        /// <param name="_filePath">Путть к файлу</param> 
        public void SaveListSalle(List<SallonObject> _listSalle, string _filePath)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Open(_filePath, FileMode.Open)))
            {
                try
                {
                    for (int i = 0; i < Presenter.ListSalle.Count; i++)
                    {
                        bw.Write(Presenter.ListSalle[i].Id);
                        bw.Write(Presenter.ListSalle[i].Date.ToString("d"));
                        bw.Write(Presenter.ListSalle[i].Name);
                        bw.Write(Presenter.ListSalle[i].Description);
                        bw.Write(Presenter.ListSalle[i].Money);
                        bw.Write(Presenter.ListSalle[i].Count);
                    }

                    bw.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// Сохранение списка закупок
        /// </summary>
        /// <param name="_listSalle">Список закупок</param>
        /// <param name="_filePath">Путь к файлу</param>
        public void SaveListStore(List<SallonObject> _listSalle, string _filePath)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Open(_filePath, FileMode.Open)))
            {
                try
                {
                    for (int i = 0; i < Presenter.ListSalle.Count; i++)
                    {
                        bw.Write(Presenter.ListStorage[i].Id);
                        bw.Write(Presenter.ListStorage[i].Date.ToString("d"));
                        bw.Write(Presenter.ListStorage[i].Name);
                        bw.Write(Presenter.ListStorage[i].Description);
                        bw.Write(Presenter.ListStorage[i].Money);
                        bw.Write(Presenter.ListStorage[i].Count);
                    }

                    bw.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// Сохранение списка расходов
        /// </summary>
        /// <param name="_listSalle">Список расходы</param>
        /// <param name="_filePath">Путь к файлу</param>
        public void SaveListCredit(List<SallonObject> _listCredit, string _filePath)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Open(_filePath, FileMode.Open)))
            {
                try
                {
                    for (int i = 0; i < Presenter.ListSalle.Count; i++)
                    {
                        bw.Write(Presenter.ListCredit[i].Date.ToString("d"));
                        bw.Write(Presenter.ListCredit[i].Description);
                        bw.Write(Presenter.ListCredit[i].Money);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        /// <summary>
        /// Загрузка файла продаж и закупок
        /// </summary>
        /// <param name="_filePath">Путь к файлу</param>
        public void LoadList(string _filePathSalle, string _filePathStore, string _filePathCedit)
        {
            using (BinaryReader br = new BinaryReader(File.Open(_filePathSalle, FileMode.Open)))
            {
                try
                {
                    while (true)
                    {
                        int id = br.ReadInt32();
                        string date = br.ReadString();
                        string name = br.ReadString();
                        string descr = br.ReadString();
                        int money = br.ReadInt32();
                        int count = br.ReadInt32();

                        Presenter.ListSalle.Add(new SallonObject(name, descr, DateTime.Parse(date), id, money, count));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Загрузка успешно!!!\n" + ex.Message, "Загрузка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            using (BinaryReader br = new BinaryReader(File.Open(_filePathStore, FileMode.Open)))
            {
                try
                {
                    while (true)
                    {
                        int id = br.ReadInt32();
                        string date = br.ReadString();
                        string name = br.ReadString();
                        string descr = br.ReadString();
                        int money = br.ReadInt32();
                        int count = br.ReadInt32();

                        Presenter.ListStorage.Add(new SallonObject(name, descr, DateTime.Parse(date), id, money, count));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            using (BinaryReader br = new BinaryReader(File.Open(_filePathCedit, FileMode.Open)))
            {
                try
                {
                    while (true)
                    {
                        string date = br.ReadString();
                        string descr = br.ReadString();
                        int money = br.ReadInt32();

                        Presenter.ListCredit.Add(new SallonObject("", descr, DateTime.Parse(date),
                            0, money, 0));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
