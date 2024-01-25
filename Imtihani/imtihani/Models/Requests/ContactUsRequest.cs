using System.ComponentModel.DataAnnotations;

namespace Imtihani.Models.Requests
{
    public class ContactUsRequest
    {
        public string FullName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        public string Description { get; set; }
    }
}
