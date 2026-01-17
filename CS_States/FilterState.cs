namespace Boss_Tracker.CS_States
{
    public class FilterState
    {
        // for simple job filter
        public List<string> ActivePlayers { get; set; } = new();
        public List<string> ActiveJobs { get; set; } = new();
        public List<string> ExcludedPlayers { get; set; } = new();
        public List<string> ExcludedJobs { get; set; } = new();

        // for advanced job owner filter
        public Dictionary<string, List<string>> JobOwnersActive { get; set; } = new();
        public Dictionary<string, List<string>> JobOwnersExcluded { get; set; } = new();
        public Dictionary<string, List<string>> JobOwnersDict { get; set; } = new();
    }
}
