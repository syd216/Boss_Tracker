namespace Boss_Tracker.CS_Utility
{
    public class StringUtils
    {
        public string[] TrimStringArrayEnd(string t)
        {
            string[] cleanedplayerLBLText = t
                .Split(" ")
                .Select(name => name.Trim())
                .Where(name => !string.IsNullOrEmpty(name))
                .ToArray();

            return cleanedplayerLBLText;
        }
    }
}
