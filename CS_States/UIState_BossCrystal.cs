namespace Boss_Tracker.CS_States
{
    public class UIState_BossCrystal
    {
        // list of panels for boss crystal page
        public List<Panel> panelList { get; set; } = new();
        public List<Panel> filteredPanelList { get; set; } = new();

        // used to track who is going to be analyzed in the crystal report
        public String player { get; set; } = "";
        public long totalMeso { get; set; } = 0;
    }
}
