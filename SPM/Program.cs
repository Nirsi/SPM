using System;
using System.Linq;
using System.CommandLine;
using System.CommandLine.Help;
using System.CommandLine.Invocation;
using System.Diagnostics;
using System.IO;
using SPM.Api;
using SPM.PluginManagement;

namespace SPM
{
    class Program
    {
        static int Main(string[] args)
        {
            var rootCommand = new RootCommand
            {
                //options with parameters
                new Option<string>(
                    "search",
                    getDefaultValue: () => "none",
                    description: "Search for plugin in Spigot repository"),
                new Option<int>(
                    "install",
                    getDefaultValue: () => 0,
                    description: "Install plugin from Spigot repository specified by ID of plugin"),
                new Option<int>(
                    "remove",
                    getDefaultValue: () => 0,
                    description: "Remove installed plugin"),
                
                //Commands without parameter
                new Command("list", "List all installed plugins")
                {
                    Handler = CommandHandler.Create(Utils.ListInstalledPlugins),
                },
                
                new Command("getserver", "Download PaperMC server for minecraft 1.15.2 for testing")
                {
                    Handler = CommandHandler.Create(Utils.DownloadServer)
                }
            };
            
            rootCommand.Description = "Spigot plugin manager";
            
            rootCommand.Handler = CommandHandler.Create<string, int, int, string, string>((search, install, remove, list, getserver) => 
                {
                    if (search != "none")
                    {
                        Utils.ListSearchedPlugins(search);
                    }

                    if (install != 0)
                    {
                        Utils.InstallPlugin(install);
                    }

                    if (remove != 0)
                    {
                        Utils.UninstallPlugin(remove);
                    }
                });
            
            return rootCommand.InvokeAsync(args).Result;
            
            //keeping it for a history lesson.
            PluginIO.PrepareDirectories();
            Router.ProcessInput(args);
        }
    }
}