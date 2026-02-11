using Boss_Tracker.CS_Services;
using Boss_Tracker.CS_States;

namespace Boss_Tracker.CS_ControlHandlers
{
    public class BossCrystalFactory
    {
        AppServices _appServices;
        UIState_BossCrystal _uiState_BossCrystal;
        FlowLayoutPanel flowPanelBossCrystals;

        public BossCrystalFactory(AppServices AS, UIState_BossCrystal UI_BC, FlowLayoutPanel FPBC)
        {
            _appServices = AS;
            _uiState_BossCrystal = UI_BC;
            flowPanelBossCrystals = FPBC;
        }

        public void AddBossPanel(string bossName, string players, string classes, string partyType, string partyID, string cleared)
        {
            Panel bPanel = _appServices.form1_BossCrystalsHandler.CreateBossCrystalPanel(bossName, players, classes, partyType);
            flowPanelBossCrystals.Controls.Add(bPanel);
            _uiState_BossCrystal.panelList.Add(bPanel);
        }
    }
}
