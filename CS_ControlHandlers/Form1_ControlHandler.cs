using Boss_Tracker.CS_States;
using Boss_Tracker.CS_Utility;
using Boss_Tracker.Properties;

namespace Boss_Tracker.CS_ControlHandlers
{
    public class Form1_ControlHandler
    {
        private readonly FilterState _filterState;
        private readonly SaveToFile _saveToFile;
        private readonly string _filePathBTT = "";

        public Form1_ControlHandler(FilterState filterState, string filePathBTT) 
        {
            _filterState = filterState;
            _saveToFile = new SaveToFile(); 
            _filePathBTT = filePathBTT;
        }

        // btn colors
        Color btnColorDefault = SystemColors.ControlLightLight;
        Color btnColorGreen = Color.LightGreen;
        Color btnColorRed = Color.IndianRed;

        // panel colors
        Color panelColor = Color.FromArgb(224, 224, 224); // gray
        Color labelColor = Color.FromArgb(244, 244, 244); // light-gray

        // font settings
        private Font fontStyleBold = new Font("Segoe UI", 9, FontStyle.Bold);
        private Font fontStyle = new Font("Segoe UI", 9);

        // player toggle buttons
        public Button CreatePlayerToggleButton(Panel panel, string player, int offset)
        {
            int buttonWidth = (int)(panel.Width / 1.25f);

            Button playertoggleButton = new Button
            {
                Text = player,
                Tag = panel,
                Width = buttonWidth,
                Location = new Point((panel.Width - buttonWidth) / 2, offset * 22)
            };

            playertoggleButton.MouseDown += playertoggleButton_MouseDown;

            return playertoggleButton;
        }

        private void playertoggleButton_MouseDown(object? sender, MouseEventArgs e)
        {
            List<string> activePlayers = _filterState.ActivePlayers;
            List<string> excludedPlayers = _filterState.ExcludedPlayers;

            // check to make sure btn is not broken
            if (sender is not Button btn)
            {
                MessageBox.Show("Error: Erroneous button detected (null). Please restart the program");
                return;
            }
            else
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (excludedPlayers.Contains(btn.Text))
                    {
                        Console.WriteLine("REMOVING PLAYER FROM EXCLUSIONS " + btn.Text);
                        excludedPlayers.Remove(btn.Text);
                    }

                    if (!activePlayers.Contains(btn.Text))
                    {
                        activePlayers.Add(btn.Text);
                        btn.BackColor = btnColorGreen;
                    }
                    else
                    {
                        activePlayers.Remove(btn.Text);
                        btn.BackColor = btnColorDefault;
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    if (activePlayers.Contains(btn.Text))
                    {
                        Console.WriteLine("REMOVING PLAYER FROM ACTIVE " + btn.Text);
                        activePlayers.Remove(btn.Text);
                    }

                    if (!excludedPlayers.Contains(btn.Text))
                    {
                        excludedPlayers.Add(btn.Text);
                        btn.BackColor = btnColorRed;
                        Console.WriteLine(excludedPlayers.Count);
                    }
                    else
                    {
                        excludedPlayers.Remove(btn.Text);
                        btn.BackColor = btnColorDefault;
                    }
                }
            }
        }

        // job toggle buttons
        public Button CreateJobToggleButton(Panel panel, string player, int offset)
        {
            int buttonWidth = (int)(panel.Width / 1.25f);

            Button jobtoggleButton = new Button
            {
                Text = player,
                Tag = panel,
                Width = buttonWidth,
                Location = new Point((panel.Width - buttonWidth) / 2, offset * 22)
            };

            jobtoggleButton.MouseDown += jobtoggleButton_MouseDown;

            return jobtoggleButton;
        }

        private void jobtoggleButton_MouseDown(object? sender, MouseEventArgs e)
        {
            List<string> activeJobs = _filterState.ActiveJobs;
            List<string> excludedJobs = _filterState.ExcludedJobs;

            if (sender is not Button btn)
            {
                MessageBox.Show("Error: Erroneous button detected (null). Please restart the program");
                return;
            }
            else
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (excludedJobs.Contains(btn.Text))
                    {
                        Console.WriteLine("REMOVING JOB FROM EXCLUSIONS " + btn.Text);
                        excludedJobs.Remove(btn.Text);
                    }

                    if (!activeJobs.Contains(btn.Text))
                    {
                        activeJobs.Add(btn.Text);
                        btn.BackColor = btnColorGreen;
                    }
                    else
                    {
                        activeJobs.Remove(btn.Text);
                        btn.BackColor = btnColorDefault;
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    if (activeJobs.Contains(btn.Text))
                    {
                        Console.WriteLine("REMOVING JOB FROM ACTIVE " + btn.Text);
                        activeJobs.Remove(btn.Text);
                    }

                    if (!excludedJobs.Contains(btn.Text))
                    {
                        excludedJobs.Add(btn.Text);
                        btn.BackColor = btnColorRed;
                        Console.WriteLine(excludedJobs.Count);
                    }
                    else
                    {
                        excludedJobs.Remove(btn.Text);
                        btn.BackColor = btnColorDefault;
                    }
                }
            }
        }

        // boss panels
        public Panel CreateBossPanel(string bossName, string players, string jobs, string partyType, string partyID, string cleared)
        {
            Panel panel = new Panel
            {
                Width = 730,
                Height = 77,
                BackColor = panelColor,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5)
            };

            string boss = bossName;
            string difficulty = "";
            string[] keywords = { "Easy", "Normal", "Hard", "Chaos", "Extreme" };

            string? foundKeyword = keywords.FirstOrDefault(k => boss.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0);

            // split the string of players & jobs that is retrieved from the CSV
            string[] splitPlayers = players.Split(" ");
            string[] splitJobs = jobs.Split(" ");

            if (foundKeyword != null)
            {
                // substring, trim, and retrieve boss difficulty from original csv data
                string trimmed = boss.Substring(foundKeyword.Length).Trim();
                boss = trimmed;
                difficulty = foundKeyword;
            }

            PictureBox bossPictureBox = new PictureBox
            {
                Width = panel.Height,
                Height = panel.Height,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5),
                BackgroundImage = Resources.ResourceManager.GetObject(boss) as Image,
                BackgroundImageLayout = ImageLayout.Stretch,
            };

            int xOffset = bossPictureBox.Width + 1;

            Label bossLabel = new Label
            {
                Name = "bossLabel",
                Text = $"{boss} ({difficulty}) | {partyType} | [{players.Split().Length - 1}]",
                Width = 300, // set width dependant on if player count over 3
                Height = 25, 
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = labelColor,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5),
                Location = new Point(xOffset, 0),
                Font = fontStyleBold
            };

            Label partyIDLabel = new Label
            {
                Name = "partyIDLabel",
                Text = partyID,
                Width = 300,
                Height = 25,
                BackColor = labelColor,
                TextAlign = ContentAlignment.MiddleLeft,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5),
                Location = new Point(xOffset, 75),
                Font = fontStyle,
            };

            partyIDLabel.Hide(); // hide after creating. Only used for debugging

            // if the amount of players is over 3 (-1 for white space inclusion), extend the width of the boss label
            // by a 1/3rd of the size against a multiplier (since each player label is 1/3rd the size of the boss label)
            if (splitPlayers.Length - 1 > 3)
            {
                int widthMultiplier = (splitPlayers.Length - 1) - 3;
                bossLabel.Width += (bossLabel.Width / 3) * widthMultiplier;
                partyIDLabel.Width = bossLabel.Width; // also a long label that matches the boss label width
            }

            // needs to be converted to an array or list later to properly hold player values *********************
            // instead of using a label as a container for variables                          *********************
            // this and jobsLabel will be hidden as they are more code-functional rather than visual
            Label playersLabel = new Label
            {
                Name = "playersLabel",
                Text = players,
                Width = 300,
                Height = 25,
                BackColor = labelColor,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5),
                Location = new Point(xOffset, 25),
                Font = fontStyle,
                Visible = false
            };

            Label jobsLabel = new Label
            {
                Name = "jobsLabel",
                Text = jobs,
                Width = 300,
                Height = 25,
                BackColor = labelColor,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(xOffset, 50),
                Margin = new Padding(5),
                Font = fontStyle,
                Visible = false
            };

            // FOR LOOPS FOR MAKING THE VISUAL LABELS----------------------------------------------------------------
            for (int i = 0; i < splitPlayers.Length - 1; i++) // need to -1 the length because the tail end is empty data: " "
            {
                // create purely visual labels based off the player names
                // using i, the index, as the offset for the name & how spaced each element should be
                Label visualPlayerLabel = new Label
                { 
                    Name = $"visualPlayerLabel{i}", 
                    Text = splitPlayers[i],
                    Width = 100,
                    Height = 25,
                    BackColor = labelColor,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(5),
                    Location = new Point(xOffset + (100 * i), 25), 
                };

                if (splitPlayers.Length - 1 < 3) // less than 3 players
                {
                    visualPlayerLabel.Width = bossLabel.Width / 2; ;
                    visualPlayerLabel.Location = new Point(xOffset + (bossLabel.Width / 2 * i), 25);
                }

                panel.Controls.Add(visualPlayerLabel); 
            }
            
            for (int i = 0; i < splitJobs.Length - 1; i++)
            {
                Label visualJobsLabel = new Label
                {
                    Name = $"visualJobLabel{i}",
                    Text = splitJobs[i],
                    Width = 100,
                    Height = 25,
                    BackColor = labelColor,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(5),
                    Location = new Point(xOffset + (100 * i), 50),
                };

                if (splitPlayers.Length - 1 < 3) // less than 3 players
                {
                    visualJobsLabel.Width = bossLabel.Width / 2;
                    visualJobsLabel.Location = new Point(xOffset + (bossLabel.Width / 2 * i), 50);
                }

                panel.Controls.Add(visualJobsLabel);
            }

            // special case for solos
            if (splitPlayers.Length == 1 && splitJobs.Length == 1)
            {
                Label visualPlayerLabel = new Label
                {
                    Name = $"visualPlayerLabel",
                    Text = players,
                    Width = bossLabel.Width,
                    Height = 25,
                    BackColor = labelColor,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(5),
                    Location = new Point(xOffset, 25),
                };

                Label visualJobsLabel = new Label
                {
                    Name = $"visualJobLabel",
                    Text = jobs,
                    Width = bossLabel.Width,
                    Height = 25,
                    BackColor = labelColor,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(5),
                    Location = new Point(xOffset, 50),
                };

                panel.Controls.Add(visualPlayerLabel);
                panel.Controls.Add(visualJobsLabel);
            }

            Button clearButton = new Button
            {
                Name = "clearButton",
                Width = 65,
                Height = 65,
                BackColor = Color.White,
                Location = new Point(panel.Width - 67, panel.Top),
                Font = fontStyle
            };

            if (cleared == "Y")
            {
                clearButton.Text = "Unclear";
                panel.BackColor = Color.LightGreen;
            }
            else
            {
                clearButton.Text = "Clear";
            }

            clearButton.Tag = panel;
            clearButton.Click += clearButton_Click;

            panel.Controls.Add(bossPictureBox);
            panel.Controls.Add(bossLabel);
            panel.Controls.Add(partyIDLabel);
            panel.Controls.Add(playersLabel);
            panel.Controls.Add(jobsLabel);
            panel.Controls.Add(clearButton);

            return panel;
        }

        // ALSO CALLS SaveToFile.cs TO SAVE THE CLEAR STATE OF THE PARTY
        private void clearButton_Click(object? sender, EventArgs e)
        {
            string partyIDLabelText;

            // check to make sure btn is not broken
            if (sender is not Button btn)
            {
                MessageBox.Show("Error: Erroneous button detected (null). Please restart the program");
                return;
            }

            // type pattern check w/ variable declaration 
            if (btn.Tag is Panel panel) 
            { 
                if (btn.Text == "Clear")
                {
                    panel.BackColor = Color.LightGreen;
                    btn.Text = "Unclear";
                }
                else
                {
                    panel.BackColor = panelColor;
                    btn.Text = "Clear";
                }

                // get relevant panel controls
                foreach (Control c in panel.Controls)
                {
                    if (c is Label)
                    {
                        // look for partyIDLabel and save new clear state to the BossTradeTracker CSV file
                        if (c.Name == "partyIDLabel") 
                        { 
                            partyIDLabelText = c.Text;
                            string clearState = "";

                            // set clearstate to Y or N depending on state of button
                            if (btn.Text == "Clear") { clearState = "N"; }
                            else { clearState = "Y"; }

                            _saveToFile.WriteAllToTracker(partyIDLabelText, clearState, _filePathBTT);
                        }
                    }
                }
            }
        }
    }
}
