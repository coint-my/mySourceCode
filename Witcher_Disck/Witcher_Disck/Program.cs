using System;
using System.IO;
using System.Threading;
//using System.Threading.Tasks;

namespace Witcher_Disck
{
    public static class Program
    {
        private static FileInfo info;
        private static StreamWriter writer;
        private static FileSystemWatcher witcher;
        private static Witcher_Disck.Form1 form;

        public static int _counter = 0;
        public static string _pathImageLog = @"C:\Users\вова\SkyDrive\Документы\Witcher\ImageLog" + DateTime.Now.Day + @"\";
        public static string _pathDirLog = @"C:\Users\вова\SkyDrive\Документы\Witcher\Log" + DateTime.Now.Day + @"\";

        private static void Main(string[] args)
        {
            form = new Witcher_Disck.Form1();

            TimerCallback tc = new TimerCallback(tick);
            Timer timer = new Timer(tc, null, 0, 60000);        

            info = new FileInfo(_pathDirLog + "Log" + DateTime.Now.Day + 
                "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + ".txt");

            DirectoryInfo _dirinfo = info.Directory;
            if (!_dirinfo.Exists)
                _dirinfo.Create();

            DirectoryInfo _dirinfoImage = new DirectoryInfo(_pathImageLog);
            if (!_dirinfoImage.Exists)
                _dirinfoImage.Create();

            writer = info.CreateText();

            witcher = new FileSystemWatcher(@"E:\");

            witcher.IncludeSubdirectories = true;

            witcher.Created += new FileSystemEventHandler(create);
            witcher.Deleted += new FileSystemEventHandler(delete);
            witcher.Renamed += new RenamedEventHandler(rename);

            witcher.EnableRaisingEvents = true;

            Console.ReadKey();
        }

        private static void tick(object obj)
        {
            form.ScreenShot(_pathImageLog, _counter++);
        }

        private static void rename(object sender, RenamedEventArgs e)
        {            
            writer.WriteLine("{0} | изменение на | {1} | дата | {2} ",
                e.OldFullPath, e.FullPath, DateTime.Now.ToString("F"));
            writer.Flush();

            form.ScreenShot(_pathImageLog, _counter);
        }

        private static void delete(object sender, FileSystemEventArgs e)
        {
            writer.WriteLine("{0} | удалил | {1} | дата | {2} ",
                e.FullPath, e.Name, DateTime.Now.ToString("F"));
            writer.Flush();
        }

        private static void create(object sender, FileSystemEventArgs e)
        {
            writer.WriteLine("{0} | создан | {1} | дата | {2} | {3}",
                e.FullPath, e.Name, DateTime.Now.ToString("F"), e.ChangeType);
            writer.Flush();
        }
    }
}
