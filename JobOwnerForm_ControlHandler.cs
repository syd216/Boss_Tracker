using Boss_Tracker.CS_Filter;
using System.Runtime.InteropServices;

namespace Boss_Tracker
{
    internal class JobOwnerForm_ControlHandler
    {
        // vars here from Form1 (_mainForm)
        Dictionary<string, List<string>> _jobOwnersActive;
        Dictionary<string, List<string>> _jobOwnersExcluded;

        // btn colors
        Color btnColorDefault = SystemColors.ControlLightLight;
        Color btnColorGreen = Color.LightGreen;
        Color btnColorRed = Color.IndianRed;

        public JobOwnerForm_ControlHandler(FilterOptions options) 
        { 
            _jobOwnersActive = options.JobOwnersActive;
            _jobOwnersExcluded = options.JobOwnersExcluded;

            Console.WriteLine("---------------------------");
            foreach (string key in _jobOwnersActive.Keys)
            {
                Console.WriteLine($"Active Key: " + key);
            }

            foreach (string key in _jobOwnersExcluded.Keys)
            {
                Console.WriteLine($"Excluded Key: " + key);
            }
        }

        // off set is the locational offset from the right of the original label
        public Panel CreateJobOwnerSections(int offset, string player, List<string> jobs)
        {
            int locationOffset = 126 * offset;

            Panel panel = new Panel
            {
                Size = new Size(120, 250),
                Location = new Point(12 + locationOffset, 12)
            };

            Label playerLabel = new Label
            {
                Size = new Size(120, 20),
                BackColor = Color.FromArgb(224, 224, 224),
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5),
                Text = player,
                TextAlign = ContentAlignment.MiddleCenter
            };

            panel.Controls.Add(playerLabel);

            foreach (string job in jobs)
            {
                Button jobButton = new Button
                { 
                    Size = new Size(120, 25),
                    Location = new Point(playerLabel.Location.X, 25 + (25 * jobs.IndexOf(job))),
                    Margin = new Padding(5),
                    Tag = playerLabel,
                    Text = job
                };

                jobButton.MouseDown += jobButton_MouseDown;

                panel.Controls.Add(jobButton);
            }

            RecolorJobOwnerButton(panel, player);

            return panel; 
        }

        // of the current panel of the current player that is being taken care of, recolor the buttons 
        // based on jobOwnersActive/Excluded
        void RecolorJobOwnerButton(Panel panel, string player)
        {
            List<Button> buttons = new List<Button>();

            // acquire buttons (each button has job name associated with owner)
            foreach (Control c in panel.Controls)
            {
                if (c is Button) { buttons.Add((Button)c); }
            }

            if (_jobOwnersActive.ContainsKey(player))
            {
                foreach (string job in _jobOwnersActive[player])
                {
                    //Console.WriteLine(job);

                    foreach (Button btn in buttons)
                    {
                        if (btn.Text == job) { btn.BackColor = btnColorGreen; }
                    }
                }
            }

            if (_jobOwnersExcluded.ContainsKey(player))
            {
                foreach (string job in _jobOwnersExcluded[player])
                {
                    foreach (Button btn in buttons)
                    {
                        if (btn.Text == job) { btn.BackColor = btnColorRed; }
                    }
                }
            }
        }

        private void jobButton_MouseDown(object? sender, MouseEventArgs e)
        {
            // check to make sure btn is not broken
            if (sender is not Button btn)
            {
                MessageBox.Show("Error: Erroneous button detected (null). Please restart the program");
                return;
            }
            else if (btn.Tag != null)
            {
                // by setting the tag of the button to the label in the current panel
                // we can easily retrieve the player's name from that label
                string player = ((Label)btn.Tag).Text;
                string job = btn.Text;

                if (e.Button == MouseButtons.Left)
                {
                    if (_jobOwnersExcluded.ContainsKey(player)) 
                    { 
                        if (_jobOwnersExcluded[player].Contains(job)) { _jobOwnersExcluded[player].Remove(job); }
                    }

                    if (_jobOwnersActive.ContainsKey(player))
                    {
                        if (!_jobOwnersActive[player].Contains(job))
                        {
                            _jobOwnersActive[player].Add(job);
                            btn.BackColor = btnColorGreen;
                        }
                        else
                        {
                            _jobOwnersActive[player].Remove(job);
                            btn.BackColor = btnColorDefault;

                            // if the key (player) has no jobs selected remove the key from the current owner list
                            if (_jobOwnersActive[player].Count <= 0) { _jobOwnersActive.Remove(player); }
                        }
                    }
                    else
                    {
                        _jobOwnersActive.Add(player, new List<string>());
                        _jobOwnersActive[player].Add(job);
                        btn.BackColor = btnColorGreen;
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    if (_jobOwnersActive.ContainsKey(player))
                    {
                        if (_jobOwnersActive[player].Contains(job)) { _jobOwnersActive[player].Remove(job); }
                    }

                    if (_jobOwnersExcluded.ContainsKey(player))
                    {
                        if (!_jobOwnersExcluded[player].Contains(job))
                        {
                            _jobOwnersExcluded[player].Add(job);
                            btn.BackColor = btnColorRed;
                        }
                        else
                        {
                            _jobOwnersExcluded[player].Remove(job);
                            btn.BackColor = btnColorDefault;

                            if (_jobOwnersExcluded[player].Count <= 0) { _jobOwnersExcluded.Remove(player); }
                        }
                    }
                    else
                    {
                        _jobOwnersExcluded.Add(player, new List<string>());
                        _jobOwnersExcluded[player].Add(job);
                        btn.BackColor = btnColorRed;
                    }
                }
            }
        }
    }
}
