using Boss_Tracker.CS_ControlHandlers;

namespace Boss_Tracker.CS_Services
{
    public class AppServices
    {
        public CSVLoader csvLoader { get; }
        public CSVDownloader csvDownloader { get; } = new();
        public FilterHandler filterHandler { get; } = new();
        public Form1_ControlHandler form1_CotnrolHandler { get; }

        public AppServices(string bttPath, string charPath, Form1 form)
        {
            csvLoader = new CSVLoader(bttPath, charPath);
            form1_CotnrolHandler = new Form1_ControlHandler(form, bttPath);
        }
    }
}
