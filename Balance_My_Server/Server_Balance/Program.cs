using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server_Balance
{
    public class MyClientServer
    {
        //идентификатор клиента
        public string id { get; private set; }
        //имя клиента
        public string name { get; private set; }
        //сам клиент
        public TcpClient tcpClient { get; private set; }
        //поток связаный с сервером и клиентом
        public NetworkStream stream { get; private set; }
        /// <summary>
        /// конструктор по умолчанию лучше не использовать
        /// </summary>
        public MyClientServer()
        {
            tcpClient = null;
            stream = null;
            id = Guid.NewGuid().ToString();
            name = "noname";
        }
        /// <summary>
        /// передача конструктору клиента
        /// </summary>
        /// <param name="client">клиент пользователь</param>
        public MyClientServer(TcpClient client)
        {
            tcpClient = client;
            stream = null;
            id = Guid.NewGuid().ToString();
            name = "constructor";
        }
        public void setUser(string str)
        {
            name = str;
        }
        private bool checkUsers(BinaryReader _br, BinaryWriter _bw)
        {
            bool flag = false;
            while (!flag)
            {
                string load_and_create = _br.ReadString();

                if (load_and_create == "create")
                {
                    setUser(_br.ReadString());
                    bool flagCreate = !Program.CheckUser(name);

                    if (!flagCreate)
                    {
                        Console.WriteLine("такой юзер есть ({0}) не возможно создать такого.", name);
                        _bw.Write(false);
                        continue;
                    }
                    else
                    {
                        using (BinaryWriter bw = new BinaryWriter(File.Create(Program.dir_source + "\\" + name + ".dat")))
                        {
                            Console.WriteLine("такой юзер ({0}) был успешно создан.", name);
                            _bw.Write(true);
                        }
                        continue;
                    }
                }
                else
                    setUser(load_and_create);

                flag = Program.CheckUser(name);

                if (flag)
                {
                    Console.WriteLine("такой пользователь есть ({0})", name);
                    _bw.Write(true);
                }
                else
                {
                    Console.WriteLine("такого пользователя нет! ({0})", name);
                    _bw.Write(false);
                }
            }
            return flag;
        }
        private void LoadData(string _filePath, BinaryWriter bw)
        {
            using (BinaryReader _br = new BinaryReader(File.Open(_filePath, FileMode.Open)))
            {
                int count = (int)_br.BaseStream.Length;
                byte[] temp = new byte[count];
                _br.BaseStream.Read(temp, 0, count);
                bw.Write(count);
                stream.Write(temp, 0, count);
            }
        }
        private void SaveData(string _filePath, int lenght)
        {
            using (BinaryWriter _bw = new BinaryWriter(File.Open(_filePath, FileMode.Open)))
            {
                //byte[] b = new byte[lenght];
                List<byte> lb = new List<byte>();
                byte b;
                for (int i = 0; i < lenght; i++)
                {
                    b = (byte)stream.ReadByte();
                    lb.Add(b);
                }
                _bw.Write(lb.ToArray());
            }
        }
        public void update()
        {
            try
            {
                //беру ссылку потока от клиента
                stream = tcpClient.GetStream();

                BinaryReader br = new BinaryReader(stream);
                BinaryWriter bw = new BinaryWriter(stream);
                //проверка на пользователя есть ли такой
                bool userOk = checkUsers(br, bw);
                //если есть то продолжаем
                if(userOk)
                {
                    //передаю клиенту данные
                    LoadData(Program.dir_source + "\\" + name + ".dat", bw);
                    bool isok = br.ReadBoolean();
                    Console.WriteLine("load = {0} {1}", DateTime.Now, isok);
                    //ожидаю ответа от клиента на сохранение
                    while (true)
                    {
                        string str_temp = br.ReadString();
                        Console.WriteLine("клиент хочет = {0}", str_temp);
                        if (str_temp == "save")
                        {
                            SaveData(Program.dir_source + "\\" + name + ".dat", br.ReadInt32());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("клиент вышел по причине(" + e.Message + ")");
            }
            finally
            {
                close();
            }
        }
        public void close()
        {
            if (stream != null)
                stream.Close();
            if (tcpClient != null)
                tcpClient.Close();
        }
    }
    class MyClient
    {
        TcpClient client;
        public MyClient(TcpClient _c)
        {
            client = _c;
        }
        public void Update()
        {
            Console.WriteLine("создаеться новый поток.");

            NetworkStream client_stream = client.GetStream();

            BinaryReader binaryReader = new BinaryReader(client_stream);
            BinaryWriter binaryWriter = new BinaryWriter(client_stream);

            string user = "";
            try
            {
                user = binaryReader.ReadString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Пользователь закрыл соединение.");
                binaryWriter.Close();
                binaryReader.Close();
                client.Close();
                return;
            }

            Console.WriteLine("клиент запросил имя базы данных: {0}", user);
            if (Program.CheckUser(user, Program.dir_source))
            {
                Console.WriteLine("Такой пользователь есть");
                binaryWriter.Write(true);

                BinaryReader br = new BinaryReader(File.Open(Program.dir_source + "\\" + user + ".dat", FileMode.Open));
                Stream st = br.BaseStream;
                st.CopyTo(client_stream);
                st.Close();
                br.Close();

                bool u = client_stream.CanWrite;
                //BinaryReader read = new BinaryReader(client_stream);
                //bool f = read.ReadBoolean();
                Console.WriteLine("--------{0}--------", u);
                //read.Close();
            }
            else
            {
                Console.WriteLine("Такого пользователя нет");
                binaryWriter.Write(false);
            }

            binaryWriter.Close();
            binaryReader.Close();
            client_stream.Close();
            client.Close();
        }
    }

    class MyServer
    {
        //сервер прослушка сообщений
        TcpListener server;
        //список клиентов
        List<MyClientServer> listClient;
        
        public MyServer()
        {
            listClient = new List<MyClientServer>();            
        }
        /// <summary>
        /// прослушивание новых клиентов и потоков
        /// </summary>
        public void Listen()
        {
            try
            {
                server = new TcpListener(IPAddress.Any, 8080);
                server.Start();
                Console.WriteLine("Сервер запущен. Ожидание подключений...");

                while (true)
                {
                    TcpClient tcpClient = server.AcceptTcpClient();

                    MyClientServer clientObject = new MyClientServer(tcpClient);
                    listClient.Add(clientObject);

                    Thread clientThread = new Thread(new ThreadStart(clientObject.update));
                    clientThread.Start();

                    for(int i = 0; i < listClient.Count; i++)
                    {
                        if (!listClient[i].tcpClient.Connected)
                            removeClient(listClient[i].id);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                disconnect();
            }
        }
        /// <summary>
        /// удаляем со списка клиента по id
        /// </summary>
        /// <param name="id">идентификатор по которому происходит удаление</param>
        public void removeClient(string id)
        {
            MyClientServer clientObject = listClient.FirstOrDefault(c => c.id == id);
            Console.WriteLine("пльзователь вышел (name = {0}, id = {1})", clientObject.name, id);
            if (clientObject != null)
                listClient.Remove(clientObject);
        }
        /// <summary>
        /// разорвать связь сервера и всех клиентов
        /// </summary>
        public void disconnect()
        {
            server.Stop();
            foreach (var item in listClient)
            {
                item.close();
            }
            listClient.Clear();
        }
    }
    class Program
    {
        public static string dir_source = "source";
        static bool check_directory_source(string _path)
        {
            return Directory.Exists(Directory.GetCurrentDirectory() + "\\" + _path);
        }
        /// <summary>
        /// Создать папку если такой не существует
        /// </summary>
        /// <param name="_path">Имя папки</param>
        /// <returns>Вернет полный путь к папке созданной</returns>
        static string create_directory(string _path)
        {
            string flag = "";

            if(!check_directory_source(_path))
            {
                DirectoryInfo dir_i = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\" + _path);
                dir_i.Create();
                flag = dir_i.FullName;
            }

            return flag;
        }
        /// <summary>
        /// Проверка на существования файла пользователя
        /// </summary>
        /// <param name="_user">Имя пользователя</param>
        /// <param name="_dirFileName">Папка в которой смотреть имя пользователя</param>
        /// <returns>Если файл пользователя существует то вернет true</returns>
        public static bool CheckUser(string _user, string _dirFileName = "source")
        {
            //проверяю всех возможных пользователей в данной папке (sourse)
            DirectoryInfo dir = new DirectoryInfo(_dirFileName);
            FileInfo[] files = dir.GetFiles();

            //читаю и записываю в строку всех пользователей
            List<string> temps = new List<string>();
            for (int i = 0; i < files.Length; i++)
            {
                string tt = "";
                tt = files[i].Name.Substring(0, files[i].Name.Length - 4);
                temps.Add(tt);
            }

            //смотрю на совпадения есть ли такой
            for (int i = 0; i < temps.Count; i++)
            {
                if (_user.ToLower().CompareTo(temps[i].ToLower()) == 0)
                    return true;
            }

            return false;
        }

        static MyServer myServer;
        static Thread thread;
        static void Main(string[] args)
        {
            string full_name_source = "";
            Console.WriteLine("Проверка папки базы данных: {0}", check_directory_source(dir_source));
            if (!check_directory_source(dir_source))
            {
                full_name_source = create_directory(dir_source);
                Console.WriteLine("Папка была создана. Полный путь к папке: {0}", full_name_source);
            }
            else Console.WriteLine("Папка существует.");

            try
            {
                myServer = new MyServer();
                thread = new Thread(new ThreadStart(myServer.Listen));
                thread.Start();
            }
            catch(Exception e)
            {
                Console.WriteLine("Ошибка сервера!!! {0}", e.Message);
                myServer.disconnect();
            }
            Console.ReadLine();
        }
    }
}
