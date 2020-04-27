using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using SPM.Api;

namespace SPM.PluginManagement
{
    public class PluginIO
    {
        internal const string SpmBase = "./spm";
        private const string StoreBase = "/store";
        private const string BackupBase = "/backup";
        private const string PluginBase = "./plugins";

        /// <summary>
        /// Creates basic folder structure if one is needed 
        /// </summary>
        public static void PrepareDirectories()
        {
            if (!Directory.Exists(SpmBase))
            {
                Directory.CreateDirectory(SpmBase);
            }

            if (!Directory.Exists($"{SpmBase}/{StoreBase}"))
            {
                Directory.CreateDirectory($"{SpmBase}/{StoreBase}");
            }
            
            if (!Directory.Exists($"{SpmBase}/{BackupBase}"))
            {
                Directory.CreateDirectory($"{SpmBase}/{BackupBase}");
            }
        }

        public static void RemovePlugin()
        {
            
        }

        public static void DownloadPlugin(long resourceId)
        {
            //getting memory stream of downloaded jar file
            /*
             var resourceMemStream = Calls.GetResourceJar(resourceId);
            var file = new FileStream($"{PluginBase}/{resourceName}.jar", FileMode.Create, FileAccess.Write);
            resourceMemStream.WriteTo(file);
            file.Close();
            resourceMemStream.Close();
            */
            
            //https://api.spiget.org/v2/resources/1331/download
            var request = (HttpWebRequest)WebRequest.Create($"https://api.spiget.org/v2/resources/{resourceId}/download");

            HttpWebResponse response = null;
            
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception webException)
            {
                Console.WriteLine(webException.Message);
                Console.ReadLine();
            }

            Console.WriteLine(response.StatusCode);
            if (response.StatusCode == HttpStatusCode.Redirect)
            {
                Console.WriteLine("new URL for plugin download: " + response.Headers["Location"]);
                Console.ReadLine();
            }
            
            
            var originalFileName = response.Headers["Content-Disposition"];
            //Stream streamWithFileBody = response.GetResponseStream();
            //Console.WriteLine($"{originalFileName.Split('"')[1]}");

            var cleaner = new Regex(@"[\\/:*?""<>|]");
            var cleanFileName = cleaner.Replace(originalFileName.Split('"')[1], "");
            
            WebClient webClient = new WebClient();
            webClient.DownloadFile("https://api.spiget.org/v2/resources/1331/download", $"{PluginBase}/{cleanFileName.Split('#')[0]}.jar");
        }
        
        /// <summary>
        /// Creates backup of all installed plugins to ./spm/backup folder
        /// </summary>
        public static void CreateBackup()
        {
            
        }
        
        
    }
}