using System;
using System.Net;
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

        /// <summary>
        /// Non-production functionality.
        /// Downloads PaperMC server for development testing
        /// </summary>
        public static void DownloadServer()
        {
            (new WebClient()).DownloadFile(@"https://papermc.io/api/v1/paper/1.15.2/latest/download","TestServer.jar");
        }
    }
}