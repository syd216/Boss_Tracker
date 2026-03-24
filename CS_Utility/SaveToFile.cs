namespace Boss_Tracker.CS_Utility
{
    public class SaveToFile()
    {
        public void WriteAllToTracker(string partyID, string clearState, string filePathTracker)
        {
            List<string> lines = new List<string>();
            List<string> finalLines = new List<string>();

            // Boss [0], PartyID [1], partyType [2], CharID [3], owner [4], Class [5], partySize [6], Cleared [7], Notes [8]
            using (FileStream fs = new FileStream(filePathTracker, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    // we are not skipping the first two lines as we want to rewrite the entire file with the new data
                    // fill the lines list above with each line from the csv

                    string currentLine;
                    while ((currentLine = sr.ReadLine()) != null) { lines.Add(currentLine); }
                }
            }

            foreach (string line in lines)
            {
                string[] splitLine = line.Split(',');

                if (splitLine[1] == partyID)
                {
                    //Console.WriteLine($"Found new line to clear: {line}");
                    splitLine[7] = clearState;

                    string joinedAlteredLine = string.Join(',', splitLine);
                    finalLines.Add(joinedAlteredLine);
                }
                else
                {
                    // if the partyID does not match with the partyID of the boss that cleared was pressed, pass into finalLines without change
                    finalLines.Add(line);
                }
            }

            File.WriteAllLines(filePathTracker, finalLines);
        }

        // create and write to settings if a checkbox is used
        public void WriteToSettings(int index, char state)
        {
            String settingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings");
            String toFile = "";

            if (File.Exists(settingsPath))
            {
                String oldSettings = File.ReadAllText(settingsPath);

                char[] newSettings = oldSettings.ToCharArray();
                newSettings[index] = state;

                toFile = new String(newSettings);
            }
            else
            {
                String initSettings = "0001";

                char[] newSettings = initSettings.ToCharArray();
                newSettings[index] = state;

                toFile = new String(newSettings);
            }

            File.WriteAllText(settingsPath, toFile);
        }
    }
}
