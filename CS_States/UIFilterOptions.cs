namespace Boss_Tracker.CS_States
{
    public class UIFilterOptions
    {
        public required string Mode { get; set; }

        // required UI elements
        public required string FilterBossTextBoxText { get; set; }
        public required CheckBox SoloCheckBox { get; set; }
        public required CheckBox ExcludeSoloCheckBox { get; set; }
        public required CheckBox ExcludeClearsButton { get; set; }
        public required TextBox ElementAmountLabel { get; set; }
    }
}
