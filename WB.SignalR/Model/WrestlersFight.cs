using System.Collections.Generic;

namespace WB.SignalR.Model
{
    public class WrestlersFight
    {
        public Wrestler FirstWrestler { get; set; }
        public WrestlerTightsColor FirstWrestlerColor { get; set; }
        public int FirstWrestlerScor { get; set; }
        public Wrestler SecondWrestler { get; set; }
        public WrestlerTightsColor SecondWrestlerColor { get; set; }
        public int SecondWrestlerScor { get; set; }
        public List<PeriodModel> Periods { get; set; }

        public WrestlersFight()
        {
            Periods = new List<PeriodModel>();
        }
    }
}
