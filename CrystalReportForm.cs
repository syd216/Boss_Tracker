using Boss_Tracker.CS_Contexts;
using Boss_Tracker.CS_Services;
using Boss_Tracker.CS_States;
using Boss_Tracker.Properties;

namespace Boss_Tracker
{
    public partial class CrystalReportForm : Form
    {
        UIState_BossCrystal _uiState_BossCrystal;
        AppServices _appServices;

        // flat meso is an estimated amount a character makes after finishing all of their assigned parties
        // this is calculated based off of Akechi, CPap, and Vellum/HMag (~370,000,000)
        // later this value is multiplied against the amount of jobs a player has for the final total of all jobs' income
        long flatMeso = 370000000;
        long flatMesoAfterJobCount = 0;

        // font settings
        private Font fontStyleBold = new Font("Segoe UI", 9, FontStyle.Bold);
        private Font fontStyle = new Font("Segoe UI", 9);

        public CrystalReportForm(UIState_BossCrystal UI_BC, AppServices AS)
        {
            InitializeComponent();
            _uiState_BossCrystal = UI_BC;
            _appServices = AS;

            // make sure that there is a player to-be-analyzed
            if (!String.IsNullOrEmpty(UI_BC.player))
            {
                CreateReport();
            }
        }

        // pass UIState_BossCrystal as well as player's owned jobs to create breakdown per character
        public void CreateReport()
        {
            Dictionary<String, List<String>> jobOwnersDict = _appServices.csvLoader.LoadPlayerJobOwners();
            String player = _uiState_BossCrystal.player;
            flatMesoAfterJobCount = flatMeso * jobOwnersDict[player].Count;
            Console.WriteLine(flatMeso);

            ProcessPanels(jobOwnersDict[player]);
        }

        private void ProcessPanels(List<String> jobs)
        {
            // these panel lists are necssary to place the "Total" panel first, then the player's jobs
            List<Panel> finalPanels = new List<Panel>();
            List<Panel> createdPanels = new List<Panel>();

            foreach (string job in jobs)
            {
                createdPanels.Add(CreatePanel(_uiState_BossCrystal.player, job));
            }

            CrystalFlowPanel.Controls.Add(CreatePanel(_uiState_BossCrystal.player, "Total"));

            foreach (Panel panel in createdPanels)
            {
                CrystalFlowPanel.Controls.Add(panel);
            }
        }

        private Panel CreatePanel(String player, String job)
        {
            int panelWidth = 300;
            int elementWidth = panelWidth - 2;

            int labelHeight = 25;

            FlowLayoutPanel panel = new FlowLayoutPanel
            {
                Margin = new Padding(3),
                Width = panelWidth,
                Height = 480,
                Location = new Point(3, 3),
                BorderStyle = BorderStyle.FixedSingle, 
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Padding = Padding.Empty
            };

            Label jobNameLabel = new Label()
            {
                Text = job,
                AutoSize = false,
                BorderStyle = BorderStyle.FixedSingle,
                Width = elementWidth,
                Height = labelHeight,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = Padding.Empty,
                Font = fontStyleBold
            };

            // WEEKLY
            Label weeklyLabel = new Label()
            {
                Text = "Weekly",
                AutoSize = false,
                BorderStyle = BorderStyle.FixedSingle,
                Width = elementWidth,
                Height = labelHeight,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = Margin = new Padding(0, labelHeight, 0, 0),
                Font = fontStyleBold
            };

            PictureBox weeklyMesoPartyPictureBox = new PictureBox
            {
                Width = 25,
                Height = 25,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = Padding.Empty,
                BackgroundImage = Resources.ResourceManager.GetObject("MesoGold") as Image,
                BackgroundImageLayout = ImageLayout.Stretch,
            };

            Label weeklyMesoLabel = new Label()
            {
                Text = "",
                AutoSize = false,
                BorderStyle = BorderStyle.FixedSingle,
                Width = panelWidth - weeklyMesoPartyPictureBox.Width - 2,
                Height = labelHeight,
                TextAlign = ContentAlignment.MiddleRight,
                Margin = Padding.Empty,
                Font = fontStyle
            };

            // check if this is not the total panel
            if (job != "Total")
            {
                long finalMeso = 0;

                foreach (Panel p in _uiState_BossCrystal.filteredPanelList)
                {
                    if (p.Tag is BossCrystalContext context)
                    {
                        if (context.BossPanelPlayerJobPairs.ContainsKey(player))
                        {
                            if (context.BossPanelPlayerJobPairs[player] == job && context.BossName != "BM")
                            {
                                finalMeso += context.Meso;
                            }
                        }
                    }
                }

                weeklyMesoLabel.Text = (finalMeso + flatMeso).ToString("N0");
            }
            else
            {
                weeklyMesoLabel.Text = (_uiState_BossCrystal.totalMeso + flatMesoAfterJobCount).ToString("N0");
            }
            // WEEKLY

            // MONTHLY
            Label monthlyLabel = new Label()
            {
                Text = "Monthly",
                AutoSize = false,
                BorderStyle = BorderStyle.FixedSingle,
                Width = elementWidth,
                Height = labelHeight,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = Margin = new Padding(0, labelHeight, 0, 0),
                Font = fontStyleBold
            };

            PictureBox monthlyMesoPartyPictureBox = new PictureBox
            {
                Width = 25,
                Height = 25,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = Padding.Empty,
                BackgroundImage = Resources.ResourceManager.GetObject("MesoGold") as Image,
                BackgroundImageLayout = ImageLayout.Stretch,
            };

            Label monthlyMesoLabel = new Label()
            {
                Text = "",
                AutoSize = false,
                BorderStyle = BorderStyle.FixedSingle,
                Width = panelWidth - weeklyMesoPartyPictureBox.Width - 2,
                Height = labelHeight,
                TextAlign = ContentAlignment.MiddleRight,
                Margin = Padding.Empty,
                Font = fontStyle
            };
            
            // monthly estimated meso calculated here based off of 4 weeks in a month 
            long monthlyMeso = Convert.ToInt64(weeklyMesoLabel.Text.Replace(",", "")) * 4;
            monthlyMesoLabel.Text = monthlyMeso.ToString("N0");
            // MONTHY

            panel.Controls.Add(jobNameLabel);

            panel.Controls.Add(weeklyLabel);
            panel.Controls.Add(weeklyMesoPartyPictureBox);
            panel.Controls.Add(weeklyMesoLabel);

            panel.Controls.Add(monthlyLabel);
            panel.Controls.Add(monthlyMesoPartyPictureBox);
            panel.Controls.Add(monthlyMesoLabel);

            return panel;
        }
    }
}
