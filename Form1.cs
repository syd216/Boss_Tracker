using Boss_Tracker.CS_Services;
using Boss_Tracker.CS_States;
using Boss_Tracker.Properties;
using System.Diagnostics;

namespace Boss_Tracker // 1/16/2026
{
    public partial class Form1 : Form
    {
        // list of panels and important controls ** MOVED TO UIState.cs
        //List<Panel> panelList = new List<Panel>();
        //List<Panel> filteredPanelList = new List<Panel>();
        //List<Button> playerToggleButtons = new List<Button>();
        //List<Button> jobToggleButtons = new List<Button>();

        // for simple job filter                 ** MOVED TO FilterState.cs
        //public List<string> activePlayers = new List<string>();
        //public List<string> activeJobs = new List<string>();
        //public List<string> excludedPlayers = new List<string>();
        //public List<string> excludedJobs = new List<string>();

        // for advanced job owner filter         ** MOVED TO FilterState.cs
        //Dictionary<string, List<string>> jobOwnersDict = new Dictionary<string, List<string>>();
        //public Dictionary<string, List<string>> jobOwnersActive = new Dictionary<string, List<string>>();
        //public Dictionary<string, List<string>> jobOwnersExcluded = new Dictionary<string, List<string>>();

        JobOwnerForm? _jobOwnerForm; // need to have the form saved to memory in order to make sure only one instance exists

        // dictionary for preserving data from the config file
        private readonly Dictionary<string, string> _configDict = new Dictionary<string, string>();

        // other classes                         ** MOVED TO AppServices.cs
        //private readonly Form1_ControlHandler _controlHandler;
        //private readonly FilterHandler _filterHandler = new FilterHandler();
        //private readonly CSVLoader _csvLoader;
        //private readonly CSVDownloader _csvDownloader = new CSVDownloader();

        // refactoring job for state cs files 1/11/2026
        private FilterState _filterState;
        private UIState _uiState;
        private AppServices _appServices;
        private UIFilterOptions _uiFilterOptions;

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

            // refactor job [[
            // set CSVLoader and ControlHandler in constructor
            _configDict = configDict; // set from Program.cs

            _filterState = new FilterState();
            _uiState = new UIState();
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
            //]]

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

            SubmitFilter("group"); // this loads FilterOptions into memory
        }

        // set variables and send off paramters to FilterHandler.cs
        private void SubmitFilter(string mode)
        {
            if (_uiFilterOptions != null)
            {
                _uiFilterOptions.Mode = mode;
                _uiFilterOptions.FilterBossTextBoxText = filterbossComboBox.Text;
                _appServices.filterHandler.ApplyFilter(_uiState, _uiFilterOptions, _filterState);
            }
        }

        // finalize initial loading and add all boss panels with no filtering
        private void AddBossPanel(string bossName, string players, string classes, string partyType, string partyID, string cleared)
        {
            Panel bPanel = _appServices.form1_ControlHandler.CreateBossPanel(bossName, players, classes, partyType, partyID, cleared);
            flowPanel.Controls.Add(bPanel);
            _uiState.panelList.Add(bPanel);
        }

        private void AddPlayerToggle(Panel panel, string player, int offset)
        {
            Button ptButton = _appServices.form1_ControlHandler.CreatePlayerToggleButton(panel, player, offset);
            panel.Controls.Add(ptButton);
            _uiState.playerToggleButtons.Add(ptButton);
        }

        private void AddJobToggle(Panel panel, string job, int offset)
        {
            Button jtButton = _appServices.form1_ControlHandler.CreateJobToggleButton(panel, job, offset);
            panel.Controls.Add(jtButton);
            _uiState.jobToggleButtons.Add(jtButton);
        }

        // group is the default filter mode. Checkboxes will be handled in other conditionals
        private void filterButton_Click(object sender, EventArgs e) { SubmitFilter("Group"); }
        private void filterbossTextBox_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) { SubmitFilter("Group"); } }

        private void clearfilterButton_Click(object sender, EventArgs e)
        {
            // DEBUG LABEL HERE
            ElementAmount.Text = _uiState.panelList.Count.ToString();

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

            foreach (Button btn in _uiState.playerToggleButtons) { btn.BackColor = SystemColors.ControlLightLight; }
            foreach (Button btn in _uiState.jobToggleButtons) { btn.BackColor = SystemColors.ControlLightLight; }

            // filtered panel list panels are disabled and reset in FilterHandler.cs
            // default panels are then shown
            _appServices.filterHandler.ClearFilterRefresh(_uiState.panelList, _uiState.filteredPanelList);

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

            await _appServices.csvDownloader.Download(BTTPath, "BossTradeTracker.csv");
            await _appServices.csvDownloader.Download(CharactersPath, "Characters.csv");

            MessageBox.Show("Program will re-open after closing this message box");
            Process.Start(Environment.ProcessPath!);
            Environment.Exit(0); // this kills the process entirely (this.close performs standard application closure)
        }
    }
}