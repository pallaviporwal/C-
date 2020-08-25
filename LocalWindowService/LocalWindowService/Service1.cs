using System;
using System.ServiceProcess;
using System.Timers;
using System.IO;


namespace LocalWindowService
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();
        public Service1()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            WriteToFile("Service is Started at" + DateTime.Now);
            //timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            //timer.Interval = 60000;   // Service will call every 60 Seconds
            //timer.Enabled = true;
        }
        //private void OnElapsedTime(object source, ElapsedEventArgs e)
        //{
        //    WriteToFile("Service is recall at " + DateTime.Now);
        //}
        public void WriteToFile(string message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + "txt";
            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(message);
                }
            }
        }
        protected override void OnStop()
        {
            WriteToFile("Service is stopped at" + DateTime.Now);
        }
    }
}
