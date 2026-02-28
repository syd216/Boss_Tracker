using Boss_Tracker.CS_Contexts;
using Boss_Tracker.CS_States;

namespace Boss_Tracker.CS_Services
{
    public class BossCrystal_FilterHandler
    {
        public int ApplyFilter(UIState_BossCrystal uiState_BossCrystal, BossPanel_FilterState bossPanel_FS)
        {
            if (bossPanel_FS.ActivePlayers.Count > 1)
            {
                MessageBox.Show("Please choose one player", "Notice");
                return 1; // error reason
            }
            else if (bossPanel_FS.ActivePlayers.Count <= 0)
            {
                // show everything if no player is selected
                uiState_BossCrystal.filteredPanelList.Clear();
                TogglePanels(uiState_BossCrystal);
                return 2; // default reason
            }

            // clear out the filtered panel list for each new search
            uiState_BossCrystal.filteredPanelList.Clear();
            // also set the currently analyzed player for future use in CrystalReportForm.cs
            uiState_BossCrystal.player = bossPanel_FS.ActivePlayers[0];

            string targetPlayer = bossPanel_FS.ActivePlayers[0];
            long totalMeso = 0;

            foreach (Panel p in uiState_BossCrystal.panelList)
            {
                if (p.Tag is BossCrystalContext context)
                {
                    if (context.BossPanelPlayerJobPairs.ContainsKey(targetPlayer))
                    {
                        uiState_BossCrystal.filteredPanelList.Add(p);
                        if (context.BossName != "BM")
                        {
                            totalMeso += context.Meso;
                        }
                    }
                }
            }

            uiState_BossCrystal.totalMeso = totalMeso;
            TogglePanels(uiState_BossCrystal);
            return 0; // success reason
        }

        private void TogglePanels(UIState_BossCrystal uiState_BossCrystal)
        {
            // HIDE AND REVEAL FILTERED PANEL LIST DEPENDING IF ANY PANELS EXIST IN THAT LIS
            if (uiState_BossCrystal.filteredPanelList.Count > 0)
            {
                foreach (Panel p in uiState_BossCrystal.panelList)
                {
                    p.Enabled = false;
                    p.Visible = false;
                }

                foreach (Panel p in uiState_BossCrystal.filteredPanelList)
                {
                    p.Enabled = true;
                    p.Visible = true;
                }
            }
            else if (uiState_BossCrystal.filteredPanelList.Count <= 0)
            {
                foreach (Panel p in uiState_BossCrystal.panelList)
                {
                    p.Enabled = true;
                    p.Visible = true;
                }

                foreach (Panel p in uiState_BossCrystal.filteredPanelList)
                {
                    p.Enabled = false;
                    p.Visible = false;
                }
            }
            else { MessageBox.Show("No results found", "Notice"); }
        }
    }
}
