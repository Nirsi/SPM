using System.IO;
using System.Text.Json;
using System.Linq;

namespace SPM.PluginManagement
{
    /// <summary>
    /// Operating Plugin Json DB
    /// </summary>
    public class PluginDb
    {
        
        
        public static void WriteToJson(PluginRecord[] pluginRecords)
        {

            var allPluginRecords = ReadFromJson().Concat(pluginRecords).ToArray();
            
            var serializerOptions = new JsonSerializerOptions {WriteIndented = true};
            File.WriteAllText($"{PluginIO.SpmBase}/plugins.json",JsonSerializer.Serialize(allPluginRecords, serializerOptions));
        }

        public static void RemoveFromJson(int resourceId)
        {
            var pluginRecords = JsonSerializer.Deserialize<PluginRecord[]>(File.ReadAllText($"{PluginIO.SpmBase}/plugins.json"));
            WriteToJson(pluginRecords.Where(t => t.id != resourceId).ToArray());
        }

        public static PluginRecord[] ReadFromJson()
        {
            var pluginRecords = JsonSerializer.Deserialize<PluginRecord[]>(File.ReadAllText($"{PluginIO.SpmBase}/plugins.json"));
            return pluginRecords;
        }
    }
}