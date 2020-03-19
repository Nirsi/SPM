using System;

namespace SPM.Api.Responses.Auxiliary
{
    public class File
    {
        public string type { get; set; }
        public double size { get; set; }
        public string sizeUnit { get; set; }
        public string url { get; set; }
    }
}