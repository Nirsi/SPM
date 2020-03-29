using System;
using SPM.Api;

namespace SPM.PluginManagement
{
    public class Utils
    {
        private static readonly Calls Calls = new Calls();
        public static void ListSearchedPlugins(string searchedName)
        {
            Console.WriteLine($@"Looking for plugin ""{searchedName}""");

            var collection = Calls.GetResourcesByName(searchedName);
            foreach (var response in collection)
            {
                Console.WriteLine($@"{response.name} ({response.id})"+
                                  $"");
            }
        }

        public static void ListInstalledPlugins()
        {
            Console.WriteLine("Installed plugins\n");
            foreach (var pluginRecord in PluginDb.ReadFromJson())
            {
                Console.WriteLine($"{pluginRecord.name} ({pluginRecord.id}) version: {pluginRecord.version}");
            }
        }
    }
}