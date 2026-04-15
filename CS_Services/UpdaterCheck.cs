using System.Diagnostics;
using System.Net.Http.Headers;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace Boss_Tracker.CS_Services
{
    public class UpdaterCheck
    {
        private static string versionUrl = "https://raw.githubusercontent.com/syd216/Boss_Tracker/master/version.txt";
        //private static string latestzipUrl = "https://github.com/syd216/Boss_Tracker/releases/latest/download/BossTracker.zip";

        public void Check()
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, versionUrl);
            request.Headers.CacheControl = new CacheControlHeaderValue
            {
                NoCache = true,
                NoStore = true
            };

            HttpResponseMessage response = client.SendAsync(request).Result;

            try
            {
                Version remoteVersion = new Version(response.Content.ReadAsStringAsync().Result.Trim());
                Version localVersion = new Version(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "version.txt")));

                if (remoteVersion > localVersion)
                {
                    Console.WriteLine($"version mismatch {localVersion} vs {remoteVersion}");
                    var _ = Process.Start("BossTrackerUpdater.exe"); // fire and forget
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("match");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Problem fetching versions. Continuing to run program without checking for an update\n\n{e}", "Notice");
            }
        }
    }
}