using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.LogEntry.Dto
{
    public class LogStats
    {
        public string DisplayText { get; set; }
        public int Count { get; set; }
        public string ColorClass { get; set; }

        public LogStats()
        {

        }
        public LogStats(string displayText, int count, string colorClass)
        {
            DisplayText = displayText;
            Count = count;
            ColorClass = colorClass;
        }
    }
}
