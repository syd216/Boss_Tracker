using Boss_Tracker.CS_ControlHandlers;
using Boss_Tracker.CS_Services;
using Boss_Tracker.CS_States;
using Boss_Tracker.Properties;

namespace Boss_Tracker
{
    public partial class Form1 : Form
    {
        // list of panels and important controls
        List<Panel> panelList = new List<Panel>();
        List<Panel> filteredPanelList = new List<Panel>();
        List<Button> playerToggleButtons = new List<Button>();
        List<Button> jobToggleButtons = new List<Button>();

        // for simple job filter
        public List<string> activePlayers = new List<string>();
        public List<string> activeJobs = new List<string>();
        public List<string> excludedPlayers = new List<string>();
        public List<string> excludedJobs = new List<string>();

        // for advanced job owner filter
        Dictionary<string, List<string>> jobOwnersDict = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> jobOwnersActive = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> jobOwnersExcluded = new Dictionary<string, List<string>>();

        // other classes
        JobOwnerForm? _jobOwnerForm; // need to have the form saved to memory in order to make sure only one instance exists

        private readonly Form1_ControlHandler _controlHandler;
        private readonly FilterHandler _filterHandler = new FilterHandler();
        private readonly CSVLoader _csvLoader;
        private readonly CSVDownloader _csvDownloader = new CSVDownloader();
        private readonly Dictionary<string, string> _configDict = new Dictionary<string, string>();

        // refactoring job for state cs files 1/11/2026 | not finished yet, most variables above will be removed
        private readonly FilterState _filterState;
        private readonly UIState _uiState;
        private readonly AppServices _appServices;
        private FilterOptions _filterOptions;

        // instead of adding elements directly to the control, a flowpanel is used to avoid resizing issues
        FlowLayoutPanel flowPanel = new FlowLayoutPanel()
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

            _csvLoader = new CSVLoader(filePathBTT, filePathCharacters);
            _controlHandler = new Form1_ControlHandler(this, filePathBTT);
            _configDict = configDict;

            FirstLoad();

            // reposition jobtogglePanel after playertogglePanel has finished populating
            jobtogglePanel.Location = new Point(
                playertogglePanel.Location.X,
                playertogglePanel.Location.Y + playertogglePanel.Height + 13);

            jobtoggleLabel.Location = new Point(jobtoggleLabel.Location.X, jobtogglePanel.Location.Y);

            jobownerButton.Location = new Point(jobownerButton.Location.X, jobtoggleLabel.Location.Y);

            Controls.Add(flowPanel);
        }

        // boss[0], partyID[1], partyType[2], charID[3], owner[4], class[5], partySize[6], cleared[7], notes[8]
        private void FirstLoad()
        {
            // load values from CSV
            List<string[]> csvData = _csvLoader.LoadBosses();
            List<string[]> csvDataSolo = _csvLoader.LoadBossesSolo();
            List<string> toggleablePlayers = _csvLoader.LoadPlayerNames();
            List<string> toggleableJobs = _csvLoader.LoadPlayerJobs();
            jobOwnersDict = _csvLoader.LoadPlayerJobOwners();

            // set value to first partyID to begin comparing to the next partyID
            string currentPartyID = csvData[0][1];
            string players = "", jobs = "";

            // fill combobox
            filterbossComboBox.Items.Add(""); // blank option

            List<string> uniqueBosses = _csvLoader.LoadBossNames();
            foreach (string boss in uniqueBosses)
            {
                filterbossComboBox.Items.Add(boss);
            }

            // iterate through retrieved list from CSV
            for (int i = 0; i < csvData.Count; i++)
            {
                players += csvData[i][4] + " ";
                jobs += csvData[i][5] + " ";

                // check if the next element would be out-of-bounds
                if (i + 1 < csvData.Count)
                {
                    // begin checking if the next element is a different partyID
                    if (currentPartyID != csvData[i + 1][1])
                    {
                        AddBossPanel(csvData[i][0], players, jobs, csvData[i][2], csvData[i][1], csvData[i][7]);
                        players = "";
                        jobs = "";

                        currentPartyID = csvData[i + 1][1];
                    }
                }
                else // if the next element is out-of-bounds, finish up and add the final boss panel with the remaining collected data
                {
                    AddBossPanel(csvData[i][0], players, jobs, csvData[i][2], csvData[i][1], csvData[i][7]);
                    players = "";
                    jobs = "";
                }
            }

            // add boss panels
            foreach (string[] line in csvDataSolo) { AddBossPanel(line[0], line[4], line[5], line[2], line[1], line[7]); }

            // add player toggle buttons
            foreach (string player in toggleablePlayers) { AddPlayerToggle(playertogglePanel, player, toggleablePlayers.IndexOf(player)); }

            // add job toggle buttons
            foreach (string job in toggleableJobs) { AddJobToggle(jobtogglePanel, job, toggleableJobs.IndexOf(job)); }

            PopulateFilterOptions();
            SubmitFilter("group"); // this loads FilterOptions into memory
        }

        private void PopulateFilterOptions()
        {
            _filterOptions = new FilterOptions
            {
                Mode = "group",
                ActivePlayers = activePlayers,
                ActiveJobs = activeJobs,
                ExcludedPlayers = excludedPlayers,
                ExcludedJobs = excludedJobs,
                JobOwnersActive = jobOwnersActive,
                JobOwnersExcluded = jobOwnersExcluded,
                FilterBossTextBoxText = filterbossComboBox.Text,
                SoloCheckBox = soloCheckBox,
                ExcludeSoloCheckBox = excludeSoloCheckBox,
                ExcludeClearsButton = ExcludeClearsButton,
                ElementAmountLabel = ElementAmount
            };
        }

        // set variables and send off paramters to FilterHandler.cs
        private void SubmitFilter(string mode)
        {
            if (_filterOptions != null)
            {
                _filterOptions.Mode = mode;
                _filterOptions.FilterBossTextBoxText = filterbossComboBox.Text;
                _filterHandler.ApplyFilter(panelList, filteredPanelList, _filterOptions);
            }
        }

        // finalize initial loading and add all boss panels with no filtering
        private void AddBossPanel(string bossName, string players, string classes, string partyType, string partyID, string cleared)
        {
            Panel bPanel = _controlHandler.CreateBossPanel(bossName, players, classes, partyType, partyID, cleared);
            flowPanel.Controls.Add(bPanel);
            panelList.Add(bPanel);
        }

        private void AddPlayerToggle(Panel panel, string player, int offset)
        {
            Button ptButton = _controlHandler.CreatePlayerToggleButton(panel, player, offset);
            panel.Controls.Add(ptButton);
            playerToggleButtons.Add(ptButton);
        }

        private void AddJobToggle(Panel panel, string job, int offset)
        {
            Button jtButton = _controlHandler.CreateJobToggleButton(panel, job, offset);
            panel.Controls.Add(jtButton);
            jobToggleButtons.Add(jtButton);
        }

        // group is the default filter mode. Checkboxes will be handled in other conditionals
        private void filterButton_Click(object sender, EventArgs e) { SubmitFilter("Group"); }
        private void filterbossTextBox_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) { SubmitFilter("Group"); } }

        private void clearfilterButton_Click(object sender, EventArgs e)
        {
            // DEBUG LABEL HERE
            ElementAmount.Text = panelList.Count.ToString();

            // reset the state of check boxes, activePlayers list, and buttons
            soloCheckBox.Checked = false;
            excludeSoloCheckBox.Checked = false;
            ExcludeClearsButton.Checked = false;

            activePlayers.Clear();
            excludedPlayers.Clear();
            activeJobs.Clear();
            excludedJobs.Clear();

            jobOwnersActive.Clear();
            jobOwnersExcluded.Clear();

            filterbossComboBox.Text = "";

            foreach (Button btn in playerToggleButtons) { btn.BackColor = SystemColors.ControlLightLight; }
            foreach (Button btn in jobToggleButtons) { btn.BackColor = SystemColors.ControlLightLight; }

            // filtered panel list panels are disabled and reset in FilterHandler.cs
            // default panels are then shown
            _filterHandler.ClearFilterRefresh(panelList, filteredPanelList);
        }

        // jobownerButton Functions
        private void jobownerButton_MouseHover(object sender, EventArgs e) { jobownerButton.Image = Resources.GearHover; }
        private void jobownerButton_MouseLeave(object sender, EventArgs e) { jobownerButton.Image = Resources.Gear; }
        private void jobownerButton_Click(object sender, EventArgs e)
        {
            // pass the main form (Form1) into the JobOwnerForm.cs constructor to access dict
            if (_jobOwnerForm != null) { _jobOwnerForm.Close(); }

            _jobOwnerForm = new JobOwnerForm(this, jobOwnersDict, _filterOptions);
            _jobOwnerForm.StartPosition = FormStartPosition.Manual;

            Point parentCenter = new Point(
                this.Left + (this.Width - _jobOwnerForm.Width) / 2,
                this.Top + (this.Height - _jobOwnerForm.Height) / 2
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

            await _csvDownloader.Download(BTTPath, "BossTradeTracker.csv");
            await _csvDownloader.Download(CharactersPath, "Characters.csv");

            MessageBox.Show("Please re-open the program to load the downloaded CSVs");
        }
    }
}