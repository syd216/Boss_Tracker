using Boss_Tracker.CS_Contexts;
using Boss_Tracker.CS_States;
using Boss_Tracker.CS_Utility;
using Boss_Tracker.Properties;

namespace Boss_Tracker.CS_ControlHandlers
{
    public class Form1_BossCrystalHandler
    {
        // string helper
        private readonly StringUtils _stringUtils = new StringUtils();
        private readonly BossCrystal_Prices _bossCrystal_Prices;

        // panel colors
        Color panelColor = Color.FromArgb(224, 224, 224); // gray
        Color labelColor = Color.FromArgb(244, 244, 244); // light-gray

        // font settings
        private Font fontStyleBold = new Font("Segoe UI", 9, FontStyle.Bold);
        private Font fontStyle = new Font("Segoe UI", 9);

        public Form1_BossCrystalHandler(BossCrystal_Prices BC_P)
        {
            _bossCrystal_Prices = BC_P;
        }

        // boss panels
        public Panel CreateBossCrystalPanel(string bossName, string players, string jobs, string partyType)
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

            string? foundKeyword = keywords.FirstOrDefault(k => bossName.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0);

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

            int labelPlayerAmount = 0;
            if (splitPlayers.Length - 1 > 0) { labelPlayerAmount = players.Split().Length - 1; }
            else { labelPlayerAmount = 1; }

            Label bossLabel = new Label
            {
                Name = "bossLabel",
                Text = $"{boss} ({difficulty}) | {partyType} | [{labelPlayerAmount}]",
                Width = 300, // set width dependant on if player count over 3
                Height = 25,
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = labelColor,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5),
                Location = new Point(xOffset, 0),
                Font = fontStyleBold
            };

            Label mesoPartyLabel = new Label
            {
                Name = "mesoPartyLabel",
                Width = 153,
                Height = 25,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = labelColor,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5),
                Font = fontStyleBold
            };
            mesoPartyLabel.Location = new Point(panel.Right - mesoPartyLabel.Width, 0);

            PictureBox mesoPartyPictureBox = new PictureBox
            {
                Width = 25,
                Height = 25,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5),
                BackgroundImage = Resources.ResourceManager.GetObject("MesoGold") as Image,
                BackgroundImageLayout = ImageLayout.Stretch,
            };
            mesoPartyPictureBox.Location = new Point(mesoPartyLabel.Left + 1 - mesoPartyPictureBox.Width, mesoPartyLabel.Location.Y);

            Label mesoLabel = new Label
            {
                Name = "mesoPartyLabel",
                Width = 153,
                Height = 25,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = labelColor,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5),
                Font = fontStyleBold
            };
            mesoLabel.Location = new Point(mesoPartyPictureBox.Left + 1 - mesoLabel.Width, 0);

            PictureBox mesoPictureBox = new PictureBox
            {
                Width = 25,
                Height = 25,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5),
                BackgroundImage = Resources.ResourceManager.GetObject("MesoGrey") as Image,
                BackgroundImageLayout = ImageLayout.Stretch,
            };
            mesoPictureBox.Location = new Point(mesoLabel.Left + 1 - mesoPictureBox.Width, mesoLabel.Location.Y);

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
                    Font = fontStyle
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
                    Font = fontStyle
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
                    Name = "visualPlayerLabel",
                    Text = players,
                    Width = bossLabel.Width,
                    Height = 25,
                    BackColor = labelColor,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(5),
                    Location = new Point(xOffset, 25),
                    Font = fontStyle
                };

                Label visualJobsLabel = new Label
                {
                    Name = "visualJobLabel",
                    Text = jobs,
                    Width = bossLabel.Width,
                    Height = 25,
                    BackColor = labelColor,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(5),
                    Location = new Point(xOffset, 50),
                    Font = fontStyle
                };

                panel.Controls.Add(visualPlayerLabel);
                panel.Controls.Add(visualJobsLabel);
            }

            panel.Controls.Add(bossPictureBox);
            panel.Controls.Add(bossLabel);
            panel.Controls.Add(mesoPictureBox);
            panel.Controls.Add(mesoPartyPictureBox);
            panel.Controls.Add(mesoLabel);
            panel.Controls.Add(mesoPartyLabel);

            // we pass bossName instead of just the cut down boss variable as the table uses both the full boss name with difficulty
            long[] mesoAmounts = CalculateMeso(mesoLabel, players, bossName);
            // also update mesoLabel with comma styling
            mesoLabel.Text = mesoAmounts[0].ToString("N0");
            mesoPartyLabel.Text = mesoAmounts[1].ToString("N0");

            // construct player & job owner
            Dictionary<String, String> playerjobPairs = new Dictionary<String, String>();
            String[] trimmedPlayers = _stringUtils.TrimStringArrayEnd(players);
            String[] trimmedJobs = _stringUtils.TrimStringArrayEnd(jobs);

            for (int i = 0; i < trimmedPlayers.Length; i++)
            {
                playerjobPairs.Add(trimmedPlayers[i], trimmedJobs[i]);
            }

            BossCrystalContext bcc = new BossCrystalContext
            {
                BossName = boss,
                BossPanelPlayerJobPairs = playerjobPairs,
                Meso = mesoAmounts[1], // only need to pass final meso amount here
            };

            // add the modified context helper to the tag for future retrieval by the sorter
            panel.Tag = bcc;

            return panel;
        }

        // using the dictionary containing the meso amount for all bosses, calculate for context and text labels;
        private long[] CalculateMeso(Label mesoLabel, String players, String bossName)
        {
            // make sure no " " exists in string and get length
            int playersTrimmedLength = _stringUtils.TrimStringArrayEnd(players).Length;
            long[] finalMesos = new long[2]; // size of 2, [0] = solo meso, [1] = party meso

            if (_bossCrystal_Prices.BossCrystalPricesDict.ContainsKey(bossName))
            {
                // index 0 contains the meso amount that the boss has for 1 player
                // index 1 contains the meso amount after the amount of players is divided against it
                finalMesos[0] = _bossCrystal_Prices.BossCrystalPricesDict[bossName];
                finalMesos[1] = _bossCrystal_Prices.BossCrystalPricesDict[bossName] / playersTrimmedLength;
            }
            
            return finalMesos;
        }
    }
}
