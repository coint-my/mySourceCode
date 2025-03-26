using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TestServer
{
    class Program
    {
        static bool saveStreamFile(NetworkStream _ns)
        {
            bool flag = true;
            BinaryWriter bw = new BinaryWriter(File.Create(Directory.GetCurrentDirectory() + "//data.dt"));

            try
            {
                //byte[] bytes = new byte[512];
                //_ns.Read(bytes, 0, bytes.Length);
                BinaryReader br = new BinaryReader(_ns);
                bw.Write(br.ReadString());
                bw.Write(br.ReadInt32());

                bw.Flush();
                flag = true;
            }
            catch(Exception e)
            {
                Console.WriteLine("{0}", e.Message);
                Console.WriteLine("Произошла ошибка сохранения.");
                flag = false;
            }
            finally
            {
                bw.Close();
            }
            return flag;
        }
        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 8080);

            server.Start();

            while(true)
            {
                TcpClient client = server.AcceptTcpClient();

                StringBuilder builder = new StringBuilder();

                Console.WriteLine("Принято подключение от клиента:");
                NetworkStream ss = client.GetStream();

                //byte[] bytes = new byte[256];

                //do
                //{
                //    int byt = ss.Read(bytes, 0, bytes.Length);
                //    builder.Append(Encoding.UTF8.GetString(bytes, 0, byt));
                //}
                //while (ss.DataAvailable); // пока данные есть в потоке

                Console.WriteLine("Получены данные от клиента: {0}", builder);
                if (saveStreamFile(ss))
                    Console.WriteLine("запись прошла успешно.");
                ss.Close();

                client.Close();
            }

            server.Stop();

            Console.ReadLine();
        }
    }
}
