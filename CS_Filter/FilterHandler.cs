using Boss_Tracker.CS_Utility;

// fix exclude list (ex: exlcuding NL but including all of zak's jobs and blaster shows nothing)

namespace Boss_Tracker.CS_Filter
{
    public class FilterHandler
    {
        StringUtils _stringUtils = new StringUtils();

        string bossLBLText = "";

        string jobsLBLText = "";
        string[] cleanedJobList = { "" };

        string playersLBLText = "";
        string[] cleanedPlayerList = { "" };

        public void ApplyFilter(List<Panel> sourcePanelList, List<Panel> filteredPanelList, FilterOptions options)
        {
            filteredPanelList.Clear(); // clear list on every new filter
            string fbTBText = options.FilterBossTextBoxText;

            foreach (Panel p in sourcePanelList)
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
                if (options.JobOwnersActive.Count > 0 || options.JobOwnersExcluded.Count > 0)
                {
                    isValid = AdvancedJobOwnerFilter(options);
                }
                else
                {
                    // this applies to all filter types. Check if the job toggle list is active
                    if (options.ActiveJobs.Count > 0)
                    {
                        // if so, bool LINQ. For each job in ActiveJobs, check if the lable contains that job
                        // using .Any(), even if one job is found while iterating, return true
                        bool jobFound = options.ActiveJobs.Any(job => jobsLBLText.IndexOf(job) >= 0);

                        if (!jobFound) { isValid = false; }
                    }

                    if (options.ExcludedJobs.Count > 0)
                    {
                        bool excludedJobFound = options.ExcludedJobs.Any(job => jobsLBLText.IndexOf(job) >= 0);

                        if (excludedJobFound) { isValid = false; }
                    }
                }

                if (options.ExcludeClearsButton.Checked)
                {
                    if (cleared) { isValid = false; }
                }

                if (!bossLBLText.Contains(fbTBText))
                {
                    Console.WriteLine("YUP");
                    isValid = false;
                }

                // to check if any of the players of the player list in the main panel should be excluded or not
                if (options.ExcludedPlayers.Count > 0)
                {
                    bool excludedPlayerFound = options.ExcludedPlayers.Any(player => playersLBLText.IndexOf(player) >= 0);

                    if (excludedPlayerFound) { isValid = false; }
                }

                if (options.ExcludeSoloCheckBox.Checked)
                {
                    if (cleanedPlayerList.Length <= 1) { isValid = false; }
                }

                // the conditionals ABOVE are checks that must be true in order to reach this point in the process
                // if they are not meant there is no point in further filtering through that specific panel

                // the conditionals BELOW check for the correct boss in the panel and other factors such as the
                // currently filtered players (ActivePlayers), correct amount of players, and group/solo play
                if ((options.Mode == "Group" && !options.SoloCheckBox.Checked) && isValid) // filters for parties only
                {
                    // begin conditional checking for every case
                    if (options.ActivePlayers.Count > 0) // are the player toggle buttons being used
                    {
                        foreach (string name in options.ActivePlayers) // begin checking if any instance of a name from players text box exists
                        {
                            if (!cleanedPlayerList.Contains(name)) { isValid = false; }

                            if (cleanedPlayerList.Length < options.ActivePlayers.Count) { isValid = false; }
                        }
                    }
                }
                else if ((options.Mode == "Solo" || options.SoloCheckBox.Checked) && isValid)
                {
                    options.ExcludeSoloCheckBox.Checked = false; // make sure exclude solo is unchecked

                    if (bossLBLText.Contains("Solo"))
                    {
                        Console.WriteLine("HERE");
                        if (bossLBLText.Contains(fbTBText) && options.ActivePlayers.Count > 0)
                        {
                            if (!options.ActivePlayers.Contains(playersLBLText))
                            {
                                isValid = false;
                            }
                        }
                    }
                    else { isValid = false; }
                }

                if (isValid) { filteredPanelList.Add(p);  }
            }

            if (filteredPanelList.Count > 0)
            {
                foreach (Panel p in sourcePanelList)
                {
                    p.Enabled = false;
                    p.Visible = false;
                }

                foreach (Panel p in filteredPanelList)
                {
                    p.Enabled = true;
                    p.Visible = true;
                }

                // DEBUG LABEL HERE
                options.ElementAmountLabel.Text = filteredPanelList.Count.ToString();
            }
            else { MessageBox.Show("No results found", "Notice"); }
        }

        bool AdvancedJobOwnerFilter(FilterOptions options)
        {
            if (options.JobOwnersActive.Count > 0)
            {
                // DictionaryFormat<string, List<string>>
                foreach (string player in options.JobOwnersActive.Keys)
                {
                    if (cleanedPlayerList.Contains(player))
                    {
                        // the job and player list from the labels are always equal
                        // they are also ordered by who plays the job 
                        for (int i = 0; i < cleanedPlayerList.Length; i++)
                        {
                            foreach (string job in options.JobOwnersActive[player])
                            {
                                bool jobFound = options.JobOwnersActive[player].Any(job => cleanedJobList[i].IndexOf(job) >= 0);

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

            if (options.JobOwnersExcluded.Count > 0)
            {
                // DictionaryFormat<string, List<string>>
                foreach (string player in options.JobOwnersExcluded.Keys)
                {
                    if (cleanedPlayerList.Contains(player))
                    {
                        for (int i = 0; i < cleanedPlayerList.Length; i++)
                        {
                            foreach (string job in options.JobOwnersExcluded[player])
                            {
                                bool jobFound = options.JobOwnersExcluded[player].Any(job => cleanedJobList[i].IndexOf(job) >= 0);

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
