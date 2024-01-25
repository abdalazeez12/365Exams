using Microsoft.AspNetCore.Http;
using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace Imtihani.Services
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public IFormFile Attachment { get; set; }

        public Message(IEnumerable<string> to, string subject, string content, IFormFile attachment = null)
        {
            To = new List<MailboxAddress>();

            var Addressdata = to.Select(x => new MailboxAddress("", to.First()));
            To.AddRange(Addressdata);


            Subject = subject;
            Content = content;
            Attachment = attachment;
        }
    }
}
