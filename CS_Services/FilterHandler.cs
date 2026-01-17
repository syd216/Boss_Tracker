using Boss_Tracker.CS_States;
using Boss_Tracker.CS_Utility;

// fix exclude list (ex: exlcuding NL but including all of zak's jobs and blaster shows nothing)

namespace Boss_Tracker.CS_Services
{
    public class FilterHandler
    {
        StringUtils _stringUtils = new StringUtils();

        string bossLBLText = "";

        string jobsLBLText = "";
        string[] cleanedJobList = { "" };

        string playersLBLText = "";
        string[] cleanedPlayerList = { "" };

        public void ApplyFilter(UIState uiState, UIFilterOptions uiFilterOptions, FilterState filterState)
        {
            uiState.filteredPanelList.Clear(); // clear list on every new filter
            string fbTBText = uiFilterOptions.FilterBossTextBoxText;

            foreach (Panel p in uiState.panelList)
            {
                bool isValid = true;
                bool cleared = false;

                foreach (Control c in p.Controls)
                {
                    if (c is Label lbl) // retrieve current boss panel labels & text fields
                    {
                        // playerLBLText/jobLBLText has a ' ' character registered as an assumed player at the end of a label
                        if (lbl.Name == "bossLabel") { bossLBLText = lbl.Text; }
                        if (lbl.Name == "playersLabel") { playersLBLText = lbl.Text; cleanedPlayerList = _stringUtils.TrimStringArrayEnd(playersLBLText); }
                        if (lbl.Name == "jobsLabel") { jobsLBLText = lbl.Text; cleanedJobList = _stringUtils.TrimStringArrayEnd(jobsLBLText); }
                    }

                    // check if boss is cleared or not. This is optionally used for the exclude clears checkbox
                    if (c is Button btn)
                    {
                        if (btn.Name == "clearButton") 
                        {
                            Console.WriteLine(btn.Text);

                            if (btn.Text == "Unclear")
                            {
                                cleared = true;
                            }
                        }
                    }
                }

                // check if any variables from the advanced job owner filter are being used
                if (filterState.JobOwnersActive.Count > 0 || filterState.JobOwnersExcluded.Count > 0)
                {
                    isValid = AdvancedJobOwnerFilter(filterState);
                }
                else
                {
                    // this applies to all filter types. Check if the job toggle list is active
                    if (filterState.ActiveJobs.Count > 0)
                    {
                        // if so, bool LINQ. For each job in ActiveJobs, check if the lable contains that job
                        // using .Any(), even if one job is found while iterating, return true
                        bool jobFound = filterState.ActiveJobs.Any(job => jobsLBLText.IndexOf(job) >= 0);

                        if (!jobFound) { isValid = false; }
                    }

                    if (filterState.ExcludedJobs.Count > 0)
                    {
                        bool excludedJobFound = filterState.ExcludedJobs.Any(job => jobsLBLText.IndexOf(job) >= 0);

                        if (excludedJobFound) { isValid = false; }
                    }
                }

                if (uiFilterOptions.ExcludeClearsButton.Checked)
                {
                    if (cleared) { isValid = false; }
                }

                if (!bossLBLText.Contains(fbTBText))
                {
                    Console.WriteLine("YUP");
                    isValid = false;
                }

                // to check if any of the players of the player list in the main panel should be excluded or not
                if (filterState.ExcludedPlayers.Count > 0)
                {
                    bool excludedPlayerFound = filterState.ExcludedPlayers.Any(player => playersLBLText.IndexOf(player) >= 0);

                    if (excludedPlayerFound) { isValid = false; }
                }

                if (uiFilterOptions.ExcludeSoloCheckBox.Checked)
                {
                    if (cleanedPlayerList.Length <= 1) { isValid = false; }
                }

                // the conditionals ABOVE are checks that must be true in order to reach this point in the process
                // if they are not meant there is no point in further filtering through that specific panel

                // the conditionals BELOW check for the correct boss in the panel and other factors such as the
                // currently filtered players (ActivePlayers), correct amount of players, and group/solo play
                if ((uiFilterOptions.Mode == "Group" && !uiFilterOptions.SoloCheckBox.Checked) && isValid) // filters for parties only
                {
                    // begin conditional checking for every case
                    if (filterState.ActivePlayers.Count > 0) // are the player toggle buttons being used
                    {
                        foreach (string name in filterState.ActivePlayers) // begin checking if any instance of a name from players text box exists
                        {
                            if (!cleanedPlayerList.Contains(name)) { isValid = false; }

                            if (cleanedPlayerList.Length < filterState.ActivePlayers.Count) { isValid = false; }
                        }
                    }
                }
                else if ((uiFilterOptions.Mode == "Solo" || uiFilterOptions.SoloCheckBox.Checked) && isValid)
                {
                    uiFilterOptions.ExcludeSoloCheckBox.Checked = false; // make sure exclude solo is unchecked

                    if (bossLBLText.Contains("Solo"))
                    {
                        Console.WriteLine("HERE");
                        if (bossLBLText.Contains(fbTBText) && filterState.ActivePlayers.Count > 0)
                        {
                            if (!filterState.ActivePlayers.Contains(playersLBLText)) { isValid = false; }
                        }
                    }
                    else { isValid = false; }
                }

                if (isValid) { uiState.filteredPanelList.Add(p);  }
            }

            if (uiState.filteredPanelList.Count > 0)
            {
                foreach (Panel p in uiState.panelList)
                {
                    p.Enabled = false;
                    p.Visible = false;
                }

                foreach (Panel p in uiState.filteredPanelList)
                {
                    p.Enabled = true;
                    p.Visible = true;
                }

                // DEBUG LABEL HERE
                uiFilterOptions.ElementAmountLabel.Text = uiState.filteredPanelList.Count.ToString();
            }
            else { MessageBox.Show("No results found", "Notice"); }
        }

        bool AdvancedJobOwnerFilter(FilterState filterState)
        {
            if (filterState.JobOwnersActive.Count > 0)
            {
                // DictionaryFormat<string, List<string>>
                foreach (string player in filterState.JobOwnersActive.Keys)
                {
                    if (cleanedPlayerList.Contains(player))
                    {
                        // the job and player list from the labels are always equal
                        // they are also ordered by who plays the job 
                        for (int i = 0; i < cleanedPlayerList.Length; i++)
                        {
                            foreach (string job in filterState.JobOwnersActive[player])
                            {
                                bool jobFound = filterState.JobOwnersActive[player].Any(job => cleanedJobList[i].IndexOf(job) >= 0);

                                if (player == cleanedPlayerList[i])
                                {
                                    if (!jobFound)
                                    {
                                        Console.WriteLine($"{bossLBLText} | {player} | cleaned: {cleanedPlayerList[i]} | job: {job} && {cleanedJobList[i]}: Would not be valid");
                                        return false;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"{bossLBLText} | {player} | cleaned: {cleanedPlayerList[i]} | job: {job} && {cleanedJobList[i]}: Would be valid");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            if (filterState.JobOwnersExcluded.Count > 0)
            {
                // DictionaryFormat<string, List<string>>
                foreach (string player in filterState.JobOwnersExcluded.Keys)
                {
                    if (cleanedPlayerList.Contains(player))
                    {
                        for (int i = 0; i < cleanedPlayerList.Length; i++)
                        {
                            foreach (string job in filterState.JobOwnersExcluded[player])
                            {
                                bool jobFound = filterState.JobOwnersExcluded[player].Any(job => cleanedJobList[i].IndexOf(job) >= 0);

                                if (player == cleanedPlayerList[i])
                                {
                                    if (jobFound)
                                    {
                                        Console.WriteLine($"{bossLBLText} | {player} | cleaned: {cleanedPlayerList[i]} | job: {job} && {cleanedJobList[i]}: Would not be valid");
                                        return false;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"{bossLBLText} | {player} | cleaned: {cleanedPlayerList[i]} | job: {job} && {cleanedJobList[i]}: Would be valid");
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }

        public void ClearFilterRefresh(List<Panel> sourcePanelList, List<Panel> filteredPanelList)
        {
            foreach (Panel p in filteredPanelList)
            {
                p.Enabled = false;
                p.Visible = false;
            }

            filteredPanelList.Clear();

            foreach (Panel p in sourcePanelList)
            {
                p.Enabled = true;
                p.Visible = true;
            }
        }
    }
}
