using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Linq;

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
        public static void WriteToJson(PluginRecord[] pluginRecords)
        {
            var serializerOptions = new JsonSerializerOptions {WriteIndented = true};
            File.WriteAllText($"{SpmBase}/plugins.json",JsonSerializer.Serialize(pluginRecords, serializerOptions));
        }

        public static void RemoveFromJson(PluginRecord removedPluginRecord)
        {
            var pluginRecords = JsonSerializer.Deserialize<PluginRecord[]>(File.ReadAllText($"{SpmBase}/plugins.json"));
            WriteToJson(pluginRecords.Where(t => t.id != removedPluginRecord.id).ToArray());
        }

        public static PluginRecord[] ReadFromJson()
        {
            var pluginRecords = JsonSerializer.Deserialize<PluginRecord[]>(File.ReadAllText($"{SpmBase}/plugins.json"));
            return pluginRecords;
        }
    }
}