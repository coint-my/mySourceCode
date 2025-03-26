using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TestServer_Client
{
    class Program
    {
        private const string ip_my_server = "192.168.0.104";
        private const string ip_local_server = "127.0.0.1";
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient(ip_my_server, 8080);

            try
            {
                NetworkStream networkStream = client.GetStream();                
                string str = Console.ReadLine();
                //byte[] bytes = Encoding.UTF8.GetBytes(str.ToCharArray());
                //networkStream.Write(bytes, 0, bytes.Length);

                BinaryWriter bw = new BinaryWriter(networkStream);
                bw.Write(str);
                bw.Write(10268943);
                
                Console.WriteLine("передал данные.");
                bw.Close();
                Console.ReadLine();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
            finally
            {
                client.Close();
            }
        }
    }
}
