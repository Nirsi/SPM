using System;
using System.Linq;
using SPM.Api;
using SPM.PluginManagement;

namespace SPM
{
    class Program
    {
        static void Main(string[] args)
        {
            PluginIO.PrepareDirectories();
            
            Router.ProcessInput(args);
            Console.ReadLine();
        }
    }
}