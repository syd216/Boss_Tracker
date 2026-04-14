using Boss_Tracker.CS_ControlHandlers;
using Boss_Tracker.CS_Services;
using Boss_Tracker.CS_States;
using Boss_Tracker.CS_Utility;
using Boss_Tracker.Properties;
using System.Diagnostics;

namespace Boss_Tracker
{
    public partial class Form1 : Form
    {
        // need to have the forms saved to memory in order to make sure only one instance exists
        JobOwnerForm? _jobOwnerForm;
        CrystalReportForm? _crystalReportForm;

        // dictionary for preserving data from the config file
        private readonly Dictionary<string, string> _configDict = new Dictionary<string, string>();

        // classes for setters/getters
        private BossPanel_FilterState _bossPanel_FS;
        private UIState_BossPanel _uiState_BossPanel;
        private UIState_BossCrystal _uiState_BossCrystal;
        private AppServices _appServices;
        private UIFilterOptions _uiFilterOptions;
        private BossPartyFactory _bossPartyFactory;
        private BossCrystalFactory _bossCrystalFactory;
        private BossCrystal_Prices _bossCrystal_Prices;
        private CustomImageLoader _customImageLoader;

        // instead of adding elements directly to the control, a flowpanel is used to avoid resizing issues
        FlowLayoutPanel flowPanelBossParties = new FlowLayoutPanel()
        {
            Dock = DockStyle.Fill,
            AutoScroll = true,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            Padding = new Padding(5)
        };

        FlowLayoutPanel flowPanelBossCrystals = new FlowLayoutPanel()
        {
            Dock = DockStyle.Fill,
            AutoScroll = true,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            Padding = new Padding(5)
        };

        public Form1(string filePathBTT, string filePathCharacters, string filePathBosses, Dictionary<string, string> configDict)
        {
            InitializeComponent();

            // initialize check boxes if settings exists
            if (File.Exists("settings"))
            {
                string settings = File.ReadAllText("settings");

                Console.WriteLine(settings);

                CheckBox[] checkBoxes = { soloCheckBox, excludeSoloCheckBox, excludeClearsCheckBox, updateCheckBox };

                for (int i = 0; i < checkBoxes.Length && i < settings.Length; i++)
                {
                    checkBoxes[i].Checked = int.TryParse(settings[i].ToString(), out int value) && value != 0;
                }
            }

            // initialize CustomImageLoader.cs
            _customImageLoader = new CustomImageLoader();
            _customImageLoader.LoadImagePathFromJSON();

            // some custom images are loaded here if available
            String tp1_bg = _customImageLoader.GetTabPage1Images("tabPage1BG");

            if (!String.IsNullOrEmpty(tp1_bg)) 
            { 
                tabPage1.BackgroundImage = Image.FromFile(tp1_bg);
                tabPage1.BackColor = Color.Transparent;
            }

            // check for update AFTER settings are loaded
            if (updateCheckBox.Checked) { new UpdaterCheck().Check(); } 

            this.MaximizedBounds = Screen.FromControl(this).WorkingArea;

            // set CSVLoader and ControlHandler in constructor
            _configDict = configDict; // set from Program.cs
            _bossPanel_FS = new BossPanel_FilterState();
            _uiState_BossPanel = new UIState_BossPanel();
            _bossCrystal_Prices = new BossCrystal_Prices();
            _uiState_BossCrystal = new UIState_BossCrystal();
            _appServices = new AppServices(filePathBTT, filePathCharacters, filePathBosses, _customImageLoader, _bossPanel_FS, _bossCrystal_Prices);

            _uiFilterOptions = new UIFilterOptions // set default state of filterOptions
            {
                Mode = "group",
                FilterBossTextBoxText = filterbossComboBox.Text,
                SoloCheckBox = soloCheckBox,
                ExcludeSoloCheckBox = excludeSoloCheckBox,
                ExcludeClearsButton = excludeClearsCheckBox,
                ElementAmountLabel = ElementAmount
            };

            // holds form builders
            _bossPartyFactory = new BossPartyFactory(_appServices, _uiState_BossPanel, flowPanelBossParties);
            _bossCrystalFactory = new BossCrystalFactory(_appServices, _uiState_BossCrystal, flowPanelBossCrystals);

            FirstLoad();

            // reposition jobtogglePanel after playertogglePanel has finished populating
            /* jobtogglePanel.Location = new Point(
                playertogglePanel.Location.X,
                playertogglePanel.Location.Y + playertogglePanel.Height + 13);

            jobtoggleLabel.Location = new Point(jobtoggleLabel.Location.X, jobtogglePanel.Location.Y);
            jobownerButton.Location = new Point(jobownerButton.Location.X, jobtoggleLabel.Location.Y);*/

            // add relevant flow panels to each tab page
            tabControl1.TabPages[0].Controls.Add(flowPanelBossParties);
            tabControl1.TabPages[1].Controls.Add(flowPanelBossCrystals);
        }

        // boss[0], partyID[1], partyType[2], charID[3], owner[4], class[5], partySize[6], cleared[7], notes[8]
        private void FirstLoad() // INITIALIZES BOSS PANELS AND BOSS CRYSTAL PANELS
        {
            // load values from CSV
            List<string[]> csvData = _appServices.csvLoader.LoadBosses();
            List<string[]> csvDataSolo = _appServices.csvLoader.LoadBossesSolo();
            List<string> toggleablePlayers = _appServices.csvLoader.LoadPlayerNames();
            List<string> toggleableJobs = _appServices.csvLoader.LoadPlayerJobs();
            _bossPanel_FS.JobOwnersDict = _appServices.csvLoader.LoadPlayerJobOwners();
            _bossCrystal_Prices.BossCrystalPricesDict = _appServices.csvLoader.LoadBossCrystalPrices();

            // set value to first partyID to begin comparing to the next partyID
            string currentPartyID = csvData[0][1];
            string players = "", jobs = "";

            // fill combobox
            filterbossComboBox.Items.Add(""); // blank option

            // add unique names of bosses to combo box on main form
            List<string> uniqueBosses = _appServices.csvLoader.LoadBossNames();
            foreach (string boss in uniqueBosses) { filterbossComboBox.Items.Add(boss); }

            // iterate through retrieved list from CSV and construct initial boss panels
            for (int i = 0; i < csvData.Count; i++)
            {
                players += csvData[i][4] + " "; // get player at position 4
                jobs += csvData[i][5] + " ";    // get job at position 5

                // check if the next element would be out-of-bounds
                if (i + 1 < csvData.Count)
                {
                    // begin checking if the next element is a different partyID
                    if (currentPartyID != csvData[i + 1][1])
                    {
                        _bossPartyFactory.AddBossPanel(csvData[i][0], players, jobs, csvData[i][2], csvData[i][1], csvData[i][7]);
                        _bossCrystalFactory.AddBossPanel(csvData[i][0], players, jobs, csvData[i][2], csvData[i][1], csvData[i][7]);
                        players = "";
                        jobs = "";

                        currentPartyID = csvData[i + 1][1];
                    }
                }
                else // if the next element is out-of-bounds, finish up and add the final boss panel with the remaining collected data
                {
                    _bossPartyFactory.AddBossPanel(csvData[i][0], players, jobs, csvData[i][2], csvData[i][1], csvData[i][7]);
                    _bossCrystalFactory.AddBossPanel(csvData[i][0], players, jobs, csvData[i][2], csvData[i][1], csvData[i][7]);
                    players = "";
                    jobs = "";
                }
            }

            // then add boss panels for solo runs
            foreach (string[] line in csvDataSolo)
            {
                _bossPartyFactory.AddBossPanel(line[0], line[4], line[5], line[2], line[1], line[7]);
                _bossCrystalFactory.AddBossPanel(line[0], line[4], line[5], line[2], line[1], line[7]);
            }

            // add player toggle buttons
            foreach (string player in toggleablePlayers) { _bossPartyFactory.AddPlayerToggle(playertogglePanel, player, toggleablePlayers.IndexOf(player)); }

            // add job toggle buttons
            foreach (string job in toggleableJobs) { _bossPartyFactory.AddJobToggle(jobtogglePanel, job, toggleableJobs.IndexOf(job)); }

            // hide fake scroll bar and enable auto scroll if elements exceed 5
            if (toggleablePlayers.Count > 5)
            {
                fakeBarTogglePlayer.Hide();
                playertogglePanel.AutoScroll = true;
            }

            if (toggleableJobs.Count > 5)
            {
                fakeBarToggleJob.Hide();
                jobtogglePanel.AutoScroll = true;
            }

            BossPanel_SubmitFilter("group"); // this loads FilterOptions into memory
        }

        // set variables and send off paramters to BossPanel_FilterHandler.cs
        private void BossPanel_SubmitFilter(string mode)
        {
            _uiFilterOptions.Mode = mode;
            _uiFilterOptions.FilterBossTextBoxText = filterbossComboBox.Text;
            _appServices.bp_filterHandler.ApplyFilter(_uiState_BossPanel, _uiFilterOptions, _bossPanel_FS);
        }

        // group is the default filter mode. Checkboxes will be handled in other conditionals
        private void filterButton_Click(object sender, EventArgs e) { BossPanel_SubmitFilter("Group"); }
        private void filterbossTextBox_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) { BossPanel_SubmitFilter("Group"); } }

        private void clearfilterButton_Click(object sender, EventArgs e)
        {
            // DEBUG LABEL HERE
            ElementAmount.Text = _uiState_BossPanel.panelList.Count.ToString();

            // reset the state of check boxes, activePlayers list, and buttons
            soloCheckBox.Checked = false;
            excludeSoloCheckBox.Checked = false;
            excludeClearsCheckBox.Checked = false;

            _bossPanel_FS.ActivePlayers.Clear();
            _bossPanel_FS.ExcludedPlayers.Clear();
            _bossPanel_FS.ActiveJobs.Clear();
            _bossPanel_FS.ExcludedJobs.Clear();

            _bossPanel_FS.JobOwnersActive.Clear();
            _bossPanel_FS.JobOwnersExcluded.Clear();

            filterbossComboBox.Text = "";

            foreach (Button btn in _uiState_BossPanel.playerToggleButtons) { btn.BackColor = SystemColors.ControlLightLight; }
            foreach (Button btn in _uiState_BossPanel.jobToggleButtons) { btn.BackColor = SystemColors.ControlLightLight; }

            // filtered panel list panels are disabled and reset in FilterHandler.cs
            // default panels are then shown
            _appServices.bp_filterHandler.ClearFilterRefresh(_uiState_BossPanel.panelList, _uiState_BossPanel.filteredPanelList);

            // reset colors in jobOwnerForm if it is open
            if (_jobOwnerForm != null)
            {
                foreach (Button btn in _jobOwnerForm.buttons) { btn.BackColor = SystemColors.ControlLightLight; }
            }
        }

        // jobownerButton Functions
        private void jobownerButton_MouseHover(object sender, EventArgs e) { jobownerButton.Image = Resources.GearHover; }
        private void jobownerButton_MouseLeave(object sender, EventArgs e) { jobownerButton.Image = Resources.Gear; }
        private void jobownerButton_Click(object sender, EventArgs e)
        {
            // pass the main form (Form1) into the JobOwnerForm.cs constructor to access dict
            if (_jobOwnerForm != null) { _jobOwnerForm.Close(); }

            _jobOwnerForm = new JobOwnerForm(_bossPanel_FS.JobOwnersDict, _bossPanel_FS);
            _jobOwnerForm.StartPosition = FormStartPosition.Manual;

            Point parentCenter = new Point(
                Left + (Width - _jobOwnerForm.Width) / 2,
                Top + (Height - _jobOwnerForm.Height) / 2
            );

            _jobOwnerForm.Location = parentCenter;
            _jobOwnerForm.Show();
        }

        // DOWNLOAD LIVE VERSIONS OF THE CSV FILES
        private async void DownloadButton_Click(object sender, EventArgs e)
        {
            string BTTPath = _configDict["bossTradeTrackerPath"];
            string CharactersPath = _configDict["charactersPath"];

            if (File.Exists(BTTPath)) { File.Delete(BTTPath); }
            if (File.Exists(CharactersPath)) { File.Delete(CharactersPath); }

            await _appServices.csvDownloader.Download(BTTPath, "BossTradeTracker.csv");
            await _appServices.csvDownloader.Download(CharactersPath, "Characters.csv");

            MessageBox.Show("Program will re-open after closing this message box");
            Process.Start(Environment.ProcessPath!);
            Environment.Exit(0); // this kills the process entirely (this.close performs standard application closure)
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 1 is crystal page, 0 is boss page
            // reveal/hide boss crystal buttons
            if (tabControl1.SelectedIndex == 1)
            {
                filterCrystal.Show();
                filterReport.Show();

                filterButton.Hide();

                jobtoggleLabel.Hide();
                jobownerButton.Hide();
                jobtogglePanel.Hide();

                bossnameLabel.Hide();
                filterbossComboBox.Hide();

            }
            else if (tabControl1.SelectedIndex == 0)
            {
                filterCrystal.Hide();
                filterReport.Hide();

                filterButton.Show();

                jobtoggleLabel.Show();
                jobownerButton.Show();
                jobtogglePanel.Show();

                bossnameLabel.Show();
                filterbossComboBox.Show();

            }
        }

        private void filterCrystal_Click(object sender, EventArgs e)
        {
            BossCrystal_SubmitFilter();
        }

        // set variables and send off paramters to BossCrystal_FilterHandler.cs
        private void BossCrystal_SubmitFilter()
        {
            _appServices.bc_filterHandler.ApplyFilter(_uiState_BossCrystal, _bossPanel_FS);
        }

        private void filterReport_Click(object sender, EventArgs e)
        {
            if (_appServices.bc_filterHandler.ApplyFilter(_uiState_BossCrystal, _bossPanel_FS) == 0)
            {
                // pass the main form (Form1) into the JobOwnerForm.cs constructor to access dict
                if (_crystalReportForm != null) { _crystalReportForm.Dispose(); }

                _crystalReportForm = new CrystalReportForm(_uiState_BossCrystal, _appServices);
                _crystalReportForm.StartPosition = FormStartPosition.Manual;

                Point parentCenter = new Point(
                    Left + (Width - _crystalReportForm.Width) / 2,
                    Top + (Height - _crystalReportForm.Height) / 2
                );

                _crystalReportForm.Location = parentCenter;
                _crystalReportForm.BringToFront();
                _crystalReportForm.Show();
            }
        }

        private void UpdateSettings(int index, bool isChecked)
        {
            new SaveToFile().WriteToSettings(
                index,
                isChecked ? '1' : '0');
        }

        // for updating the settings file
        private void soloCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSettings(0, soloCheckBox.Checked);
            if (soloCheckBox.Checked) { excludeSoloCheckBox.Checked = false; }
        }

        private void excludeSoloCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSettings(1, excludeSoloCheckBox.Checked);
            if (excludeSoloCheckBox.Checked) { soloCheckBox.Checked = false; }
        }

        private void ExcludeClearsButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSettings(2, excludeClearsCheckBox.Checked);
        }

        private void UpdateCheckButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSettings(3, updateCheckBox.Checked);
            if (updateCheckBox.Checked) { MessageBox.Show("Restart Boss Tracker to check for updates", "Notice"); }
        }
    }
}