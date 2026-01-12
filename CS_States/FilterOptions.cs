namespace Boss_Tracker.CS_States
{
    public class FilterOptions
    {
        public required string Mode { get; set; }

        // simple job filter variables
        public required List<string> ActivePlayers { get; set; }
        public required List<string> ActiveJobs { get; set; }
        public required List<string> ExcludedPlayers { get; set; }
        public required List<string> ExcludedJobs { get; set; }

        // advanced job owner filter variables
        public required Dictionary<string, List<string>> JobOwnersActive {  get; set; }
        public required Dictionary<string, List<string>> JobOwnersExcluded {  get; set; }

        // required UI elements
        public required string FilterBossTextBoxText { get; set; }
        public required CheckBox SoloCheckBox { get; set; }
        public required CheckBox ExcludeSoloCheckBox { get; set; }
        public required CheckBox ExcludeClearsButton { get; set; }
        public required Label ElementAmountLabel { get; set; }
    }
}
