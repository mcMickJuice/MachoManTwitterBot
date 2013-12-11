﻿using System.ServiceProcess;

namespace MachoManTwitterBotService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new TwitterBotService() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
