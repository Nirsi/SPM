namespace SPM.Api.Responses.Auxiliary
{
    public class Review
    {
        public Author author { get; set; }
        public Rating rating { get; set; }
        public string message { get; set; }
        public string responseMessage { get; set; }
        public string version { get; set; }
        public long date { get; set; }
    }
}