using Boss_Tracker.CS_Filter;
using System.Text.Json;

namespace Boss_Tracker.CS_Utility
{
    public class CSVLoader
    {
        string filePathTracker = "";
        string filePathCharacters = "";

        // set file paths from config file
        public CSVLoader(string fPT, string fPC)
        {
            filePathTracker = fPT;
            filePathCharacters = fPC;
        }

        // IENumerable string return allows the receiving LINQ to process the data one-by-one
        // rather than having to return an entire list/array all at once
        private IEnumerable<string[]> ReadCSVLinesTracker()
        {
            // Boss [0], PartyID [1], partyType [2], CharID [3], owner [4], Class [5], partySize [6], Cleared [7], Notes [8]

            using (FileStream fs = new FileStream(filePathTracker, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    // skip two filler rows (blank cells then header cells)
                    sr.ReadLine();
                    sr.ReadLine();

                    string? line;
                    while ((line = sr.ReadLine()) != null) { yield return line.Split(','); }
                }
            }
        }

        // return regular party groups only
        public List<string[]> LoadBosses()
        {
            // the LINQ here receives each value as it returns from ReadCSVLines()
            // and creates a list of the values after filtering out the solo bosses
            // the "values" variable here has no important meaning other than being a reference for the current CSV line
            return ReadCSVLinesTracker()
                .Where(values => values[2] != "Solo")
                .ToList();
        }

        // return solo bosses only
        public List<string[]> LoadBossesSolo()
        {
            return ReadCSVLinesTracker()
                .Where(values => values[2] == "Solo")
                .ToList();
        }

        // return uniquely appearing player names
        public List<string> LoadPlayerNames()
        {
            List<string> names = ReadCSVLinesTracker().Select(values => values[4]).OrderBy(n => n).ToList();
            List<string> uniqueNames = new List<string>();
            
            foreach (string n in names) { if (!uniqueNames.Contains(n)) { uniqueNames.Add(n); } }

            return uniqueNames;
        }

        public List<string> LoadPlayerJobs()
        {
            List<string> jobs = ReadCSVLinesTracker().Select(values => values[5]).OrderBy(j => j).ToList();
            List<string> uniqueJobs = new List<string>();

            foreach (string j in jobs) { if (!uniqueJobs.Contains(j)) { uniqueJobs.Add(j); } }

            return uniqueJobs;
        }

        public List<string> LoadBossNames()
        {
            List<string> bosses = ReadCSVLinesTracker().Select(values => values[0]).ToList();
            List<string> cleanedBosses = new List<string>();

            // get each line and find unique bosses
            foreach (string boss in bosses)
            {
                if (!cleanedBosses.Contains(boss.Split(' ')[1])) { cleanedBosses.Add(boss.Split(' ')[1]); }
            }

            return cleanedBosses;
        }

        // player-job association
        private List<string[]> ReadCSVLinesCharacters()
        {
            // CharID [0], Owner [1], Class [2], Type [3]
            using (FileStream fs = new FileStream(filePathCharacters, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    // skip first filler row (header cells)
                    sr.ReadLine();

                    List<string[]> lines = new List<string[]>();
                    string? curLine;
                    while ((curLine = sr.ReadLine()) != null) { lines.Add(curLine.Split(',')); }

                    return lines;
                }
            }
        }

        // return a list of players with a corresponding list of the jobs they play
        public Dictionary<string, List<string>> LoadPlayerJobOwners()
        {
            Dictionary<string, List<string>> jobOwnersDict = new Dictionary<string, List<string>>();
            List<string[]> lines = ReadCSVLinesCharacters();

            foreach (string[] line in lines)
            {
                if (jobOwnersDict.ContainsKey(line[1]))
                {
                    jobOwnersDict[line[1]].Add(line[2]);
                }
                else
                {
                    List<string> newList = new List<string>() { line[2] };

                    jobOwnersDict.Add(line[1], newList);
                }
            }

            return jobOwnersDict;
        }
    }
}
