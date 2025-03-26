using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_event_console_2019
{
    class Base
    {
        private delegate void message(object _sender, string _msg);
        private event message Msg;

        public void onMessage()
        {
            Msg += sayMessage;
        }
        public void offMessage()
        {
            Msg -= sayMessage;
        }
        public void callMessage(string _msg)
        {
            Msg?.Invoke(this, _msg);
        }

        private void sayMessage(object _sender, string _msg)
        {
            Console.WriteLine("{0} = событие, {1} = обьект", _msg, _sender.GetType().Name);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Base _base = new Base();
            _base.onMessage();

            var dict = new Dictionary<string, int>();
            dict.Add("Vova", 638587911);
            dict.Add("Oksana", 930428542);

            foreach (var item in dict)
            {
                _base.callMessage(item.Key + " 0" + item.Value.ToString());
            }

            DirectoryInfo info = new DirectoryInfo("C:\\");
            Console.WriteLine("name directory ({0}) = {1}", info.Name, info.Exists);
            foreach (var item in info.GetDirectories())
            {
                Console.WriteLine("folder({0});", item.Name);
            }
            foreach (var item in info.GetFiles())
            {
                Console.WriteLine("file({0});", item.Name);
            }

            Console.ReadKey();
        }
    }
}
