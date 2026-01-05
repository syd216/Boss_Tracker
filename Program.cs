using Boss_Tracker.CS_Utility;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace Boss_Tracker
{
    internal static class Program
    {
        static readonly CSVDownloader _csvDownloader = new CSVDownloader();

        // names for the file
        static readonly string bossTradeTrackerName = "BossTradeTracker.csv";
        static readonly string charactersName = "Characters.csv";
        static readonly string configName = "config.json";

        // key value names
        static readonly string configBTTKey = "bossTradeTrackerPath";
        static readonly string configCharactersKey = "charactersPath";

        static string configFile = "";
        static Dictionary<string, string>? configDict;

        /*[DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();*/

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            //AllocConsole();

            bool canRun = true;
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // get file locations (full path including file)
            configFile = Path.Combine(baseDirectory, configName);

            if (!File.Exists(configFile))
            {
                // create file then close for immediate write usage
                FileStream file = File.Create(configFile);
                file.Close();

                Dictionary<string, string> jsonDefaultDict = new Dictionary<string, string>
                {
                    { configBTTKey,"SheetsURL" },
                    { configCharactersKey,"SheetsURL" }
                };

                // more human readable
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(jsonDefaultDict, options);

                File.WriteAllText(configFile, jsonString);

                MessageBox.Show($"Config file created at: {configFile}\nPlease edit the config.json with the correct URLs for the CSV files", "Notice");

                canRun = false;
            }
            else // try to read the lines of each key. If they are invalid canRun = false
            {
                try
                {
                    // read all lines in json and use JsonSerializer to parse the file
                    string rawConfigJSON = File.ReadAllText(configFile);
                    configDict = JsonSerializer.Deserialize<Dictionary<string, string>>(rawConfigJSON);

                    if (configDict != null | configDict.Keys.Count > 1)
                    {
                        if (configDict[configBTTKey] == "")
                        {
                            canRun = false;
                            MessageBox.Show($"{configBTTKey} is empty. Please edit the config.json", "Notice");
                        }

                        if (configDict[configCharactersKey] == "")
                        {
                            canRun = false;
                            MessageBox.Show($"{configCharactersKey} is empty. Please edit the config.json", "Notice");
                        }
                    }
                    else { MessageBox.Show("Did not reach expected element amounts in dictionary. Please fix/delete the config.json to reset this", "Notice"); }
                }
                catch (Exception e) { MessageBox.Show($"Error loading json file: {e.Message}\nYell at Derpy"); canRun = false; }
            }

            // finally, if canRun is still true, download the correct files
            if (canRun)
            {
                // base directory + final name of downloaded file
                // will also be used to pass into Form1 (if successful)
                string BTTPath = Path.Combine(baseDirectory, bossTradeTrackerName);
                string CharactersPath = Path.Combine(baseDirectory, charactersName);

                try
                {
                    if (!File.Exists(BTTPath))
                    {
                        await _csvDownloader.Download(configDict[configBTTKey], "BossTradeTracker.csv");
                    }

                    if (!File.Exists(CharactersPath))
                    {
                        await _csvDownloader.Download(configDict[configCharactersKey], "Characters.csv");
                    }
                }
                catch (Exception e) { MessageBox.Show($"Download failed: {e}", "Notice"); Application.Exit(); }

                ApplicationConfiguration.Initialize();
                Application.Run(new Form1(BTTPath, CharactersPath, configDict));
            }
            else { Application.Exit(); }
        }
    }
}