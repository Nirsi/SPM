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
                    break;
                
                case "remove":
                    //Remove plugin
                    break;
                
                case "list":
                    Utils.ListInstalledPlugins();
                    break;
                
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