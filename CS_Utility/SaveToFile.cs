using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
