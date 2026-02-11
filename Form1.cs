using Boss_Tracker.CS_ControlHandlers;
using Boss_Tracker.CS_Services;
using Boss_Tracker.CS_States;
using Boss_Tracker.Properties;
using System.Diagnostics;

namespace Boss_Tracker
{
    public partial class Form1 : Form
    {
        JobOwnerForm? _jobOwnerForm; // need to have the form saved to memory in order to make sure only one instance exists

        // dictionary for preserving data from the config file
        private readonly Dictionary<string, string> _configDict = new Dictionary<string, string>();

        // classes for setters/getters
        private FilterState _filterState;
        private UIState_BossPanel _uiState_BossPanel;
        private UIState_BossCrystal _uiState_BossCrystal;
        private AppServices _appServices;
        private UIFilterOptions _uiFilterOptions;
        private BossPartyFactory _bossPartyFactory;
        private BossCrystalFactory _bossCrystalFactory;

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

        public Form1(string filePathBTT, string filePathCharacters, Dictionary<string, string> configDict)
        {
            InitializeComponent();

            this.MaximizedBounds = Screen.FromControl(this).WorkingArea;

            // set CSVLoader and ControlHandler in constructor
            _configDict = configDict; // set from Program.cs

            _filterState = new FilterState();
            _uiState_BossPanel = new UIState_BossPanel();
            _uiState_BossCrystal = new UIState_BossCrystal();
            _appServices = new AppServices(filePathBTT, filePathCharacters, _filterState);

            _uiFilterOptions = new UIFilterOptions // set default state of filterOptions
            {
                Mode = "group",
                FilterBossTextBoxText = filterbossComboBox.Text,
                SoloCheckBox = soloCheckBox,
                ExcludeSoloCheckBox = excludeSoloCheckBox,
                ExcludeClearsButton = ExcludeClearsButton,
                ElementAmountLabel = ElementAmount
            };

            // holds form builders
            _bossPartyFactory = new BossPartyFactory(_appServices, _uiState_BossPanel, flowPanelBossParties);
            _bossCrystalFactory = new BossCrystalFactory(_appServices, _uiState_BossCrystal, flowPanelBossCrystals);

            FirstLoad();

            // reposition jobtogglePanel after playertogglePanel has finished populating
            jobtogglePanel.Location = new Point(
                playertogglePanel.Location.X,
                playertogglePanel.Location.Y + playertogglePanel.Height + 13);

            jobtoggleLabel.Location = new Point(jobtoggleLabel.Location.X, jobtogglePanel.Location.Y);
            jobownerButton.Location = new Point(jobownerButton.Location.X, jobtoggleLabel.Location.Y);

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
            _filterState.JobOwnersDict = _appServices.csvLoader.LoadPlayerJobOwners();

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

            SubmitFilter("group"); // this loads FilterOptions into memory
        }

        // set variables and send off paramters to FilterHandler.cs
        private void SubmitFilter(string mode)
        {
            if (_uiFilterOptions != null)
            {
                _uiFilterOptions.Mode = mode;
                _uiFilterOptions.FilterBossTextBoxText = filterbossComboBox.Text;
                _appServices.filterHandler.ApplyFilter(_uiState_BossPanel, _uiFilterOptions, _filterState);
            }
        }

        // group is the default filter mode. Checkboxes will be handled in other conditionals
        private void filterButton_Click(object sender, EventArgs e) { SubmitFilter("Group"); }
        private void filterbossTextBox_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) { SubmitFilter("Group"); } }

        private void clearfilterButton_Click(object sender, EventArgs e)
        {
            // DEBUG LABEL HERE
            ElementAmount.Text = _uiState_BossPanel.panelList.Count.ToString();

            // reset the state of check boxes, activePlayers list, and buttons
            soloCheckBox.Checked = false;
            excludeSoloCheckBox.Checked = false;
            ExcludeClearsButton.Checked = false;

            _filterState.ActivePlayers.Clear();
            _filterState.ExcludedPlayers.Clear();
            _filterState.ActiveJobs.Clear();
            _filterState.ExcludedJobs.Clear();

            _filterState.JobOwnersActive.Clear();
            _filterState.JobOwnersExcluded.Clear();

            filterbossComboBox.Text = "";

            foreach (Button btn in _uiState_BossPanel.playerToggleButtons) { btn.BackColor = SystemColors.ControlLightLight; }
            foreach (Button btn in _uiState_BossPanel.jobToggleButtons) { btn.BackColor = SystemColors.ControlLightLight; }

            // filtered panel list panels are disabled and reset in FilterHandler.cs
            // default panels are then shown
            _appServices.filterHandler.ClearFilterRefresh(_uiState_BossPanel.panelList, _uiState_BossPanel.filteredPanelList);

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

            _jobOwnerForm = new JobOwnerForm(_filterState.JobOwnersDict, _filterState);
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
    }
}