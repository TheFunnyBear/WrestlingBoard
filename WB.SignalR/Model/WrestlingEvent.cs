using System;

namespace WB.SignalR.Model
{
    public class WrestlingEvent
    {
        public WrestlingEventType Type { get; set; }
        public DateTime PeriodTimeLeft { get; set; }
        public Wrestler Wrestler { get; set; }
    }
}
