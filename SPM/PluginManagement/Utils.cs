using System;
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
                var testedVersions = string.Join(',', response.TestedVersions);
                testedVersions = testedVersions.Length == 0 ? "None tested by the author" : testedVersions;
              
                Console.WriteLine($"{response.Name} ({response.Id})\n" + 
                                  $"Supported Minecraft versions: {testedVersions}\n"+
                                  $"Newest plugin version: {response.Version.Id}\n");
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
            Console.WriteLine($"Installing plugin {resourceDetails.Name}");

            //downloading plugin
            if (!PluginIO.DownloadPlugin(resourceDetails.Id))
            {
              return;
            }
            
            //writing record to DB
            PluginDb.WriteToJson(new []
            {
                new PluginRecord(){name = resourceDetails.Name, id = resourceDetails.Id, version = resourceDetails.Version.Id},
            });

            
        } 

        public static void UninstallPlugin(int resourceId)
        {
            //removing record from DB
            PluginDb.RemoveFromJson(resourceId);
            //deleting plugin from /plugins folder
            
        }

        //TODO: Check for versions of all installed plugin and update them.
        public static void UpdatePlugins()
        {
            
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