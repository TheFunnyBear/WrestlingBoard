using System;
using System.Collections.Generic;

namespace WB.SignalR.Model
{
    public class PeriodModel
    {
        public int Number { get; set; }
        public DateTime TimeLeft { get; set; }
        public List<WrestlingEvent> Events { get; set; }
    }
}
