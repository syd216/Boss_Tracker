using System;
using System.IO;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Boss_Tracker.CS_Services
{
    public class CSVDownloader
    {
        public async Task Download(string url, string fileName)
        {
            string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            using HttpClient client = new HttpClient();

            try
            {
                byte[] fileBytes = await client.GetByteArrayAsync(url);
                
                await File.WriteAllBytesAsync(outputPath, fileBytes);

                MessageBox.Show($"Successfully downloaded {fileName} file!", "Notice");
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error downloading {fileName} file, closing program...\nPlease check the URL in the config\n{e}", "Notice");
                Application.Exit();
            }
        }
    }
}
