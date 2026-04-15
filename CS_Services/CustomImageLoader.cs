using System.Text.Json;

namespace Boss_Tracker.CS_Services
{
    public class CustomImageLoader
    {
        // general
        public Dictionary<String, String> generalImagesDict = new Dictionary<String, String>
        {
            { "generalRightPaneBG", "" }
        };

        // tabPage1 Bosses
        public Dictionary<String, String> tabPage1ImagesDict = new Dictionary<String, String>
        {
            { "tabPage1BG", "" },
            { "tabPage1PanelBossCleared", "" },
            { "tabPage1PanelBossUncleared", "" },
            { "tabPage1PanelButtonClear", "" },
            { "tabPage1PanelButtonUnclear", "" }
        };

        // tabPage2 Boss Crystal
        public Dictionary<String, String> tabPage2ImagesDict = new Dictionary<String, String>
        {
            { "tabPage2BG", "" },
            { "tabPage2PanelBossDefault", "" }
        };

        // other
        String fileName = "CustomBGImages.json";

        public void LoadImagePathFromJSON()
        {
            String jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            if (!File.Exists(jsonPath))
            {
                string jsonDefaults = "";
                JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

                Dictionary<String, Dictionary<String, String>> combined = new()
                {
                    { "General", generalImagesDict},
                    { "TabPage1", tabPage1ImagesDict },
                    { "TabPage2", tabPage2ImagesDict }
                };

                jsonDefaults = JsonSerializer.Serialize(combined, options);

                File.WriteAllText(jsonPath, jsonDefaults);
            }    
            else
            {
                string jsonFromFile = File.ReadAllText(jsonPath);

                Dictionary<String, Dictionary<String, String>> combined = 
                    JsonSerializer.Deserialize<Dictionary<String, Dictionary<String, String>>>(jsonFromFile);

                if (combined != null)
                {
                    // ternary conditional operators. Shorthand if statement
                    // ? for boolean check on ContainsKey, if it does exist set to found TabPage key. If not, set to new()
                    generalImagesDict = combined.ContainsKey("General") ? combined["General"] : new Dictionary<String, String>();
                    tabPage1ImagesDict = combined.ContainsKey("TabPage1") ? combined["TabPage1"] : new Dictionary<String, String>();
                    tabPage2ImagesDict = combined.ContainsKey("TabPage2") ? combined["TabPage2"] : new Dictionary<String, String>();
                }
            }
        }

        public String GetTabPage1Images(String key)
        {
            if (tabPage1ImagesDict.ContainsKey(key))
            {
                if (!String.IsNullOrEmpty(tabPage1ImagesDict[key]))
                {
                    if (File.Exists(tabPage1ImagesDict[key]))
                    {
                        return tabPage1ImagesDict[key];
                    }
                }
            }

            // if all cases are false, return empty
            return "";
        }

        public String GetGeneralImages(string key)
        {
            if (generalImagesDict.ContainsKey(key))
            {
                if (!String.IsNullOrEmpty(generalImagesDict[key]))
                {
                    if (File.Exists(generalImagesDict[key]))
                    {
                        return generalImagesDict[key];
                    }
                }
            }

            return "";
        }
    }
}
