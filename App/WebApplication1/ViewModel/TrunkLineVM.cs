using static Domain_Train.Station;

namespace App_NET6.ViewModel
{
    public class TrunkLineVM
    {
        public string ChiName { get; set; }
        public int No { get; set; }
        public TrunkLineVM(TrunkLine trunkLine)
        {
            ChiName = trunkLine.ToString();
            No = (int)trunkLine;
        }
    }
}
