using Boss_Tracker.CS_Filter;

namespace Boss_Tracker
{
    public partial class JobOwnerForm : Form
    {
        private readonly Form1 _mainForm;
        private readonly JobOwnerForm_ControlHandler _controlHandler;

        public JobOwnerForm(Form1 mainForm, Dictionary<string, List<string>> jobOwnersDict, FilterOptions options)
        {
            InitializeComponent();
            _mainForm = mainForm;
            _controlHandler = new JobOwnerForm_ControlHandler(options);

            // recreate all forms everytime the window is opened
            CreateJobPanels(jobOwnersDict);
        }

        private void CreateJobPanels(Dictionary<string, List<string>> jobOwnerDict)
        {
            // for each key in jobOwnerDict (playername)
            foreach (string p in jobOwnerDict.Keys)
            {
                Panel joPanel = _controlHandler.CreateJobOwnerSections(
                    jobOwnerDict.Keys.ToList().IndexOf(p), // convert dictionary keys to list to retrieve index position for UI offsets
                    p,                                     // pass the current player to CreateJobOwnerSections for UI creation
                    jobOwnerDict[p]);                      // pass the current player's job list to CreateJobOwnerSections
                this.Controls.Add(joPanel);
            }
        }
    }
}
