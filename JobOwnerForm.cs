using Boss_Tracker.CS_ControlHandlers;
using Boss_Tracker.CS_States;
using System.Windows.Forms.ComponentModel.Com2Interop;

namespace Boss_Tracker
{
    public partial class JobOwnerForm : Form
    {
        private readonly JobOwnerForm_ControlHandler _controlHandler;
        public List<Button> buttons = new List<Button>(); 

        public JobOwnerForm(Dictionary<string, List<string>> jobOwnersDict, FilterState filterState)
        {
            InitializeComponent();
            _controlHandler = new JobOwnerForm_ControlHandler(filterState);

            // recreate all forms everytime the window is opened
            CreateJobPanels(jobOwnersDict);
            
            // create list of buttons from the panels created in CreatedJobPanels for each player
            // used in Form1 clear button function to reset colors

            List<Panel> panels = new List<Panel>();

            foreach (Control c in this.Controls)
            {
                if (c is Panel) { panels.Add((Panel)c); }
            }

            foreach (Panel p in panels)
            {
                foreach (Control c in p.Controls)
                {
                    if (c is Button) { buttons.Add((Button)c); } 
                }
            }
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
