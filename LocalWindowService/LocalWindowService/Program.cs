using System.ServiceProcess;


namespace LocalWindowService
{
  static class Program
    {     
        /// The main entry point for the application.      
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);
        }      
    }
}
