using Boss_Tracker.CS_Services;
using Boss_Tracker.CS_States;

namespace Boss_Tracker.CS_ControlHandlers
{
    public class BossPartyFactory
    {
        AppServices _appServices;
        UIState_BossPanel _uiState_BossPanel;
        FlowLayoutPanel flowPanelBossParties;

        public BossPartyFactory(AppServices AS, UIState_BossPanel UI_BS, FlowLayoutPanel FPBP) 
        {
            _appServices = AS;
            _uiState_BossPanel = UI_BS;
            flowPanelBossParties = FPBP;
        }

        // finalize initial loading and add all boss panels with no filtering
        public void AddBossPanel(string bossName, string players, string classes, string partyType, string partyID, string cleared)
        {
            Panel bPanel = _appServices.form1_BossPartiesHandler.CreateBossPanel(bossName, players, classes, partyType, partyID, cleared);
            flowPanelBossParties.Controls.Add(bPanel);
            _uiState_BossPanel.panelList.Add(bPanel);
        }

        public void AddPlayerToggle(Panel panel, string player, int offset)
        {
            Button ptButton = _appServices.form1_BossPartiesHandler.CreatePlayerToggleButton(panel, player, offset);
            panel.Controls.Add(ptButton);
            _uiState_BossPanel.playerToggleButtons.Add(ptButton);
        }

        public void AddJobToggle(Panel panel, string job, int offset)
        {
            Button jtButton = _appServices.form1_BossPartiesHandler.CreateJobToggleButton(panel, job, offset);
            panel.Controls.Add(jtButton);
            _uiState_BossPanel.jobToggleButtons.Add(jtButton);
        }
    }
}
