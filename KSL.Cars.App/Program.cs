﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace KSL.Cars.App
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
        //    AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
        //    {

        //        String resourceName = "AssemblyLoadingAndReflection." +

        //           new AssemblyName(args.Name).Name + ".dll";

        //        using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
        //        {

        //            Byte[] assemblyData = new Byte[stream.Length];

        //            stream.Read(assemblyData, 0, assemblyData.Length);

        //            return Assembly.Load(assemblyData);
        //        }
        //    };

            if (args.Contains("lastsearch"))
            {

            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmMain());
            }

        }
    }
}
