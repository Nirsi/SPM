using System;
using System.Linq;
using System.Net;
using SPM.Api;

namespace SPM.PluginManagement
{
    public class Utils
    {
        /// <summary>
        /// Write to console plugins that matches input. 
        /// </summary>
        /// <param name="searchedName"></param>
        public static void ListSearchedPlugins(string searchedName)
        {
            Console.WriteLine($"Looking for plugin \"{searchedName}\"\n");

            var collection = Calls.GetResourcesByName(searchedName);
            foreach (var response in collection)
            {                
                var testedVersions = string.Join(',', response.testedVersions);
                testedVersions = testedVersions.Length == 0 ? "None tested by the author" : testedVersions;
              
                Console.WriteLine($"{response.name} ({response.id})\n" + 
                                  $"Supported Minecraft versions: {testedVersions}\n"+
                                  $"Newest plugin version: {response.version.id}\n");
            }
        }
        
        /// <summary>
        /// Reads installed plugins from JSON and writes 'em to console
        /// </summary>
        public static void ListInstalledPlugins()
        {
            Console.WriteLine("Installed plugins\n");
            foreach (var pluginRecord in PluginDb.ReadFromJson())
            {
                Console.WriteLine($"{pluginRecord.name} ({pluginRecord.id}) version: {pluginRecord.version}");
            }
        }

        /// <summary>
        /// Installs plugin specified by resourceId
        /// TODO: Change this to any input and try to find desired plugin by name or directly by ID.
        /// </summary>
        /// <param name="resourceId"></param>
        public static void InstallPlugin(long resourceId)
        {
            var resourceDetails = Calls.GetResourceDetails(resourceId);
            
            //writing record from DB
            PluginDb.WriteToJson(new []{new PluginRecord(){name = resourceDetails.name, id = resourceDetails.id, version = resourceDetails.version.id}, });
            
            //downloading plugin
            //TODO: download plugin and install it.
            PluginIO.DownloadPlugin(resourceDetails.id);
            
        } 

        public static void UninstallPlugin(int resourceId)
        {
            //removing record from DB
            PluginDb.RemoveFromJson(resourceId);
            //deleting plugin from /plugins folder
            
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