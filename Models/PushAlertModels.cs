namespace Api.Models
{
    public class MessageBody
    {
        public string subscriber { get; set; }
        public string title { get; set; }
        public string message { get; set; }
        public string url { get; set; }
    }
    
    public class SendToAllMessageBody
    {
        public string title { get; set; }
        public string message { get; set; }
        public string url { get; set; }
    }
}