using System;
using System.ServiceProcess;
using System.Diagnostics;
using System.Threading;
using KSL.Cars.Parse;

namespace KSL.Cars.Service
{

    public class SearchService : ServiceBase
    {
        // This is a flag to indicate the service status
        private bool serviceStarted = false;

        // the thread that will do the work
        Thread workerThread;

        private ServiceInstaller searchServiceInstaller;

        public SearchService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            this.EventLog.WriteEntry(this.ServiceName + " Service Has Started");
            // Create worker thread; this will invoke the WorkerFunction
            // when we start it.
            // Since we use a separate worker thread, the main service
            // thread will return quickly, telling Windows that service has started
            ThreadStart st = new ThreadStart(KSL.Cars.Parse.Parser.parsePage);
            workerThread = new Thread(st);

            // set flag to indicate worker thread is active
            serviceStarted = true;

            // start the thread
            workerThread.Start();

        }

        protected override void OnStop()
        {
            this.EventLog.WriteEntry(this.ServiceName + " Service Has Stopped");
        }

        private void InitializeComponent()
        {
            this.searchServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // searchServiceInstaller
            // 
            this.searchServiceInstaller.Description = "Searches the car classifieds on KSL.Com";
            this.searchServiceInstaller.DisplayName = "KSL.Cars Search Service";
            this.searchServiceInstaller.ServiceName = "KSL.Cars.Search";
            // 
            // SearchService
            // 
            this.ServiceName = "KSL.Search";
        }
    }
}
