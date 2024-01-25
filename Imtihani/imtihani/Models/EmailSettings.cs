namespace Imtihani.Models
{
    public class EmailSettings
    {
        public string FromEmail { get; set; }
        public string ContactEmail { get; set; }
        public string CareersEmail { get; set; }
        public string ExceptionsEmail { get; set; }
        public bool SendConfirmation { get; set; }
    }
}
