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
        
        
        public static void WriteToJson(PluginRecord[] pluginRecords)
        {
            var serializerOptions = new JsonSerializerOptions {WriteIndented = true};
            File.AppendAllText($"{PluginIO.SpmBase}/plugins.json",JsonSerializer.Serialize(pluginRecords, serializerOptions));
        }

        public static void RemoveFromJson(PluginRecord removedPluginRecord)
        {
            var pluginRecords = JsonSerializer.Deserialize<PluginRecord[]>(File.ReadAllText($"{PluginIO.SpmBase}/plugins.json"));
            WriteToJson(pluginRecords.Where(t => t.id != removedPluginRecord.id).ToArray());
        }

        public static PluginRecord[] ReadFromJson()
        {
            var pluginRecords = JsonSerializer.Deserialize<PluginRecord[]>(File.ReadAllText($"{PluginIO.SpmBase}/plugins.json"));
            return pluginRecords;
        }
    }
}