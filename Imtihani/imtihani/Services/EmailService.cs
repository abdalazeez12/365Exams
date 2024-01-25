using Imtihani.Models;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using MimeKit;
using System.IO;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace Imtihani.Services
{
    public class EmailService : IEmailService
    {
        private EmailConfiguration _emailConfig;

        public EmailService(EmailConfiguration emailConfig) {
            _emailConfig = emailConfig;

        }
    
        public void SendEmail(string body, string to = "admin@365exams.com", string subject = "Contact Us")
        {
            var SiteURL = Environment.GetEnvironmentVariable("WebsiteUrl");
            var message = new Message(new string[] { to }, subject, body, null);
            MimeMessage emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("365Exams", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            //emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            //emailMessage.HtmlBody = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;
            bodyBuilder.TextBody = body;
            //bodyBuilder.Attachments.Add(env.WebRootPath + "\\file.png");
            byte[] fileBytes;
            if (message.Attachment != null)
            {
                using (var ms = new MemoryStream())
                {
                    message.Attachment.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }
                bodyBuilder.Attachments.Add(message.Attachment.FileName, fileBytes, ContentType.Parse(message.Attachment.ContentType));
            }
            emailMessage.Body = bodyBuilder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                    client.Send(emailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
