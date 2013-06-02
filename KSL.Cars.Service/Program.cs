using System;
using System.ServiceProcess;

namespace KSL.Cars.Service
{

    static class Program
    {
        [STAThread]
        static void Main()
        {
            ServiceBase.Run(new SearchService());
        }
    }
}
