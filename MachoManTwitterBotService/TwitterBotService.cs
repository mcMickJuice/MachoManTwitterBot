using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using StatsTwitterBot.Classes;
using System.Configuration;
using NLog;

namespace MachoManTwitterBotService
{
    public partial class TwitterBotService : ServiceBase
    {
        private TwitterBot _tBot;
        private double _timerInterval = Double.Parse(ConfigurationManager.AppSettings["TimerInterval"]) * 60 * 1000;
        protected static Logger _logger = LogManager.GetCurrentClassLogger();

        public TwitterBotService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                _tBot = new TwitterBot();
                Timer timer = new Timer();
                timer.Interval = _timerInterval;
                timer.Elapsed += ((o, e) =>
                {
                    int numOfTweets = _tBot.Run();
                    _logger.Trace(String.Format("Here in Elapsed at {0}\r\n{1} Tweets tweeted", e.SignalTime, numOfTweets));
                });
                timer.Enabled = true;
                timer.Start();
            }
            catch (Exception e)
            {
                _logger.Error("Exception on Startup: {0}\r\n{1}\r\n{2}", e.Message, e.TargetSite, e.InnerException.Message);
                throw;
            }
            
        }

        protected override void OnStop()
        {
            _logger.Warn("Service stopping at {0}", DateTime.Now.ToLongDateString());
        }

    }
}
