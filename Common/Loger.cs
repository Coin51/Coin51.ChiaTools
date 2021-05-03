using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Coin51_chia.Common
{
    public static class LogerHelper
    {
        public static  ILoggerFactory  loggerFactory = new LoggerFactory();

        /// <summary>
        /// 日志信息
        /// </summary>
        public static readonly NLog.Logger logger = NLog.LogManager.GetLogger("NLogConsole");

        static LogerHelper() {
            loggerFactory.AddNLog();
        }
    }
}
