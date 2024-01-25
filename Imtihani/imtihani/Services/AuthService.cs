//using IbtecarBusiness.Security;
//using IbtecarBusiness.Security.Interfaces;
//using DataContracts = IbtecarBusiness.Security.DataContracts;
//using System.Threading.Tasks;
//using IbtecarBusiness.Security.Services;
//using Imtihani.Models;
//using IbtecarFramework.Exceptions;
//using Imtihani.Interfaces;

//namespace Imtihani.Services
//{
//    public class AuthService : IAuthService
//    {
//        protected SecurityContext _context;
//        protected UserService _userService;
//        public AuthService(SecurityContext context)
//        {
//            _context = context;
//        }

//        public async Task<IAuthResponse> LoginAsync(string email, string password)
//        {
//            var user = await _userService.GetAsync(email);
//            if (user == null)
//                return null;

//            if (user.PasswordHash != password)
//                return new AuthResponse("Invalid Password", new string[] { });
//            return new AuthResponse( user);
//        }

//        //public async Task<IAuthResponse> RefreshTokenAsync(IRefreshRequest request)
//        //{
//        //    throw new System.NotImplementedException();
//        //}

//        public async Task<IAuthResponse> RegisterAsync(IRegisterRequest request)
//        {
//            throw new System.NotImplementedException();
//        }
//    }
//}

#region Assembly IbtecarFramework.Apis, Version=1.6.2.5, Culture=neutral, PublicKeyToken=null
// C:\Users\QUSAI AL-TAMIMI\.nuget\packages\ibtecarframework.apis\1.6.25\lib\net6.0\IbtecarFramework.Apis.dll
// Decompiled with ICSharpCode.Decompiler 7.1.0.6543
#endregion

//using System;
//using System.Collections.Generic;
//using System.Net.Mail;
//using SmtpClient = MailKit.Net.Smtp.SmtpClient;
//using MimeKit;
//using Imtihani.Models;
//using Microsoft.Extensions.Configuration;

//namespace IbtecarFramework.Apis.Services
//{
//    EmailSettings _emailSettings = new EmailSettings();
//     public class EmailService(IConfiguration config, EmailService emailService)
//    {
       
//        public string SendEmail(string from, string to, string subject, string body, List<Attachment> attachments, Dictionary<string, string> replacements, bool sendConfirmation = false, string bcc = "")
//        {
//            MailMessage mailMessage = new MailMessage();
//            try
//            {
//                mailMessage.From = new MailAddress(from);
//                string[] array = to.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
//                foreach (string address in array)
//                {
//                    mailMessage.To.Add(new MailAddress(address));
//                }

//                if (sendConfirmation)
//                {
//                    mailMessage.Bcc.Add(new MailAddress("mrjust@gmail.com"));
//                    mailMessage.Bcc.Add(new MailAddress("h.abu.odeh@gmail.com"));
//                }

//                if (!string.IsNullOrEmpty(bcc))
//                {
//                    array = bcc.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
//                    foreach (string addresses in array)
//                    {
//                        mailMessage.Bcc.Add(addresses);
//                    }
//                }

//                mailMessage.IsBodyHtml = true;
//                if (replacements != null)
//                {
//                    foreach (string key in replacements.Keys)
//                    {
//                        body = body.Replace(key, replacements[key]);
//                        subject = subject.Replace(key, replacements[key]);
//                    }
//                }

//                mailMessage.Subject = subject;
//                mailMessage.Body = body;
//                if (attachments != null)
//                {
//                    foreach (Attachment attachment in attachments)
//                    {
//                        mailMessage.Attachments.Add(attachment);
//                    }
//                }
//                using (var client = new SmtpClient())
//                {
//                    try
//                    {
//                        client.Connect(_emailConfig.SmtpServerPrice, _emailConfig.PortPrice, false);
//                        client.AuthenticationMechanisms.Remove("XOAUTH2");
//                        client.Authenticate(_emailConfig.UserNamePrice, _emailConfig.PasswordPrice);
//                        client.Send(emailMessage);
//                    }
//                    catch
//                    {
//                        //log an error message or throw an exception or both.
//                        throw;
//                    }
//                    finally
//                    {
//                        client.Disconnect(true);
//                        client.Dispose();
//                    }
//                }

//                SmtpClient.Send(mailMessage);
//                return "";
//            }
//            catch (Exception ex)
//            {
//                return ex.Message;
//            }
//            finally
//            {
//                mailMessage.Dispose();
//            }
//        }
//    }
 