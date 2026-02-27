using System.Reflection.Metadata.Ecma335;

namespace Boss_Tracker.CS_Contexts
{
    public class BossCrystalContext
    {
        public required String BossName { set; get; }
        public required Dictionary<String, String> BossPanelPlayerJobPairs { set; get; }
        public required long Meso { set; get; } = 0;
    }
}
