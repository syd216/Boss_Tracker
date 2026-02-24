using Boss_Tracker.CS_Contexts;
using Boss_Tracker.CS_States;

namespace Boss_Tracker.CS_Services
{
    public class BossCrystal_FilterHandler
    {
        public void ApplyFilter(UIState_BossCrystal uiState_BossCrystal, BossPanel_FilterState bossPanel_FS)
        {
            if (bossPanel_FS.ActivePlayers.Count > 1)
            {
                MessageBox.Show("Please choose one player", "Notice");
                return;
            }
            else if (bossPanel_FS.ActivePlayers.Count <= 0)
            {
                // show everything if no player is selected
                uiState_BossCrystal.filteredPanelList.Clear();
                TogglePanels(uiState_BossCrystal);
                return;
            }

            uiState_BossCrystal.filteredPanelList.Clear();

            string targetPlayer = bossPanel_FS.ActivePlayers[0];
            long totalMeso = 0;

            foreach (Panel p in uiState_BossCrystal.panelList)
            {
                if (p.Tag is BossCrystalContext context)
                {
                    if (context.BossPanelPlayers.Contains(targetPlayer))
                    {
                        uiState_BossCrystal.filteredPanelList.Add(p);
                        if (context.BossName != "BM")
                        {
                            totalMeso += context.Meso;
                        }
                    }
                }
            }

            Console.WriteLine(totalMeso.ToString("N0"));
            TogglePanels(uiState_BossCrystal);
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
