using System;
using SPM.PluginManagement;

namespace SPM
{
    public static class Router
    {
        public static void ProcessInput(string[] args)
        {
            //if (CheckInput(args)) return;
            switch (args[0])
            {
                case "search":
                    Utils.ListSearchedPlugins(args[1]);
                    break; 
                
                case "install":
                    //Look for plugin and install it
                    Utils.InstallPlugin(int.Parse(args[1]));
                    //temp test
                    //PluginDb.WriteToJson(new []{new PluginRecord() {name = "Test plugin", id = 156, version = 31}, new PluginRecord() {name = "Best plug", id = 151, version = "1", }, new PluginRecord() {name = "Login plugin", id = 51, version = "2"}});
                    break;
                
                case "remove":
                    //Remove plugin
                    Utils.UninstallPlugin(int.Parse(args[1]));
                    break;
                
                case "list":
                    Utils.ListInstalledPlugins();
                    break;
                
                //Development QoL feature, I will probably remove this later down the line.
                case "getserver":
                    Utils.DownloadServer();
                    break;
            }
        }

        //This tries to catch bad input
        private static bool CheckInput(string[] input)
        {
            if (input.Length < 1)
            {
                Console.WriteLine("No input params");
                return true;
            }
            
            if(input.Length < 2)
            {
                Console.WriteLine("missing second parameter");
                return true;
            }

            return false;
        }
    }
}