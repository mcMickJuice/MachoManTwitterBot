using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterTest.Objects
{
    class Log
    {

        public static Logger LoggerCtr(string logmessage, string result)
        {
            Logger log = new Logger();
            log.LogMessage = logmessage;
            log.Result = result;
            log.CreatedDate = DateTime.Now;

            return log;
        }
        
    }
}
