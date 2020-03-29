using System.IO;
using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace SPM.PluginManagement
{
    /// <summary>
    /// Operating Plugin Json DB
    /// </summary>
    public class PluginDb
    {
        private const string SpmBase = "./spm";
        private const string PluginBase = "/store";

        public static void PrepareDirectories()
        {
            if (!Directory.Exists(SpmBase))
            {
                Directory.CreateDirectory(SpmBase);
            }

            if (!Directory.Exists($"{SpmBase}/{PluginBase}"))
            {
                Directory.CreateDirectory($"{SpmBase}/{PluginBase}");
            }
            
        }
        public void WriteTest(PluginRecord[] pluginRecord)
        {
            var serOptions = new JsonSerializerOptions {WriteIndented = true};

            File.WriteAllText($"{SpmBase}/plugins.json",JsonSerializer.Serialize(pluginRecord, serOptions));
        }

        public static void WriteToJson()
        {
            
        }

        public static PluginRecord[] ReadFromJson()
        {
            var pluginRecords = JsonSerializer.Deserialize<PluginRecord[]>(File.ReadAllText($"{SpmBase}/plugins.json"));
            return pluginRecords;
        }
    }
}