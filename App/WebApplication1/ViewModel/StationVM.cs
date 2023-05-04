using Domain_Train;
using static Domain_Train.Station;

namespace App_NET6.ViewModel
{
    public class StationVM
    {
        public StationVM(Station station, TrunkLine trunkLine) {
            this.TrunkLine = trunkLine.ToString();
            this.No = station.StationNo[trunkLine];
            this.Name = station.StationName;
        }
        public string TrunkLine { get; set; }
        public int No { get; set; }
        public string Name { get; set; }
    }
}
