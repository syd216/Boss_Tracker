namespace Boss_Tracker.CS_States
{
    public class UIState
    {
        // list of panels and important controls
        public List<Panel> panelList { get; set; } = new();
        public List<Panel> filteredPanelList { get; set; } = new();
        public List<Button> playerToggleButtons { get; set; } = new();
        public List<Button> jobToggleButtons { get; set; } = new();
    }
}
