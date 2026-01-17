using Boss_Tracker.CS_ControlHandlers;
using Boss_Tracker.CS_States;

namespace Boss_Tracker.CS_Services
{
    public class AppServices
    {
        public CSVLoader csvLoader { get; }
        public CSVDownloader csvDownloader { get; } = new();
        public FilterHandler filterHandler { get; } = new();
        public Form1_ControlHandler form1_ControlHandler { get; }

        public AppServices(string bttPath, string charPath, FilterState filterState)
        {
            csvLoader = new CSVLoader(bttPath, charPath);
            form1_ControlHandler = new Form1_ControlHandler(filterState, bttPath);
        }
    }
}
