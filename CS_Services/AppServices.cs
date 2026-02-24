using Boss_Tracker.CS_ControlHandlers;
using Boss_Tracker.CS_States;

namespace Boss_Tracker.CS_Services
{
    public class AppServices
    {
        public CSVLoader csvLoader { get; }
        public CSVDownloader csvDownloader { get; } = new();
        public BossPanel_FilterHandler bp_filterHandler { get; } = new();
        public BossCrystal_FilterHandler bc_filterHandler { get; } = new();
        public Form1_BossPartyHandler form1_BossPartiesHandler { get; }
        public Form1_BossCrystalHandler form1_BossCrystalsHandler { get; }

        public AppServices(string bttPath, string charPath, string fpbPath, BossPanel_FilterState BP_FS, BossCrystal_Prices BC_P)
        {
            csvLoader = new CSVLoader(bttPath, charPath, fpbPath);
            form1_BossPartiesHandler = new Form1_BossPartyHandler(BP_FS, bttPath);
            form1_BossCrystalsHandler = new Form1_BossCrystalHandler(BC_P);
        }
    }
}
