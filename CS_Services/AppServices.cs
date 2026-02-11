using Boss_Tracker.CS_ControlHandlers;
using Boss_Tracker.CS_States;

namespace Boss_Tracker.CS_Services
{
    public class AppServices
    {
        public CSVLoader csvLoader { get; }
        public CSVDownloader csvDownloader { get; } = new();
        public FilterHandler filterHandler { get; } = new();
        public Form1_BossPartyHandler form1_BossPartiesHandler { get; }
        public Form1_BossCrystalHandler form1_BossCrystalsHandler { get; }

        public AppServices(string bttPath, string charPath, FilterState filterState)
        {
            csvLoader = new CSVLoader(bttPath, charPath);
            form1_BossPartiesHandler = new Form1_BossPartyHandler(filterState, bttPath);
            form1_BossCrystalsHandler = new Form1_BossCrystalHandler();
        }
    }
}
