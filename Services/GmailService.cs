using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Extensions.Services
{
    /// <summary>
    /// A collection of methods to iteract with Gmail
    /// </summary>
    public static class GmailService
    {
        /// <summary>
        /// Sends a list of email requests through gmail
        /// </summary>
        public static void Send(List<SendEmailRequest> requests) => requests.ForEach(r => Send(r));

        /// <summary>
        /// Sends an email request through gmail
        /// </summary>
        public static void Send(SendEmailRequest request)
        {
            var creds = new NetworkCredential(request.From, request.Password);
            var msg = new MailMessage()
            {
                From = new MailAddress(request.From),
                Subject = request.Subject,
                Body = request.Body,
                IsBodyHtml = request.IsHtml
            };

            request.To.ForEach(to => msg.To.Add(to));
            request.Cc.ForEach(cc => msg.CC.Add(cc));
            request.Bcc.ForEach(bcc => msg.Bcc.Add(bcc));

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = creds
            };

            client.Send(msg);
        }
    }

    /// <summary>
    /// A strongly typed option to send an email
    /// </summary>
    public class SendEmailRequest
    {
        public string From { get; set; }

        public string Password { get; set; }

        public List<string> To { get; set; }

        public List<string> Cc { get; set; }

        public List<string> Bcc { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsHtml { get; set; }

        public SendEmailRequest()
        {
            To = new List<string>();
            Cc = new List<string>();
            Bcc = new List<string>();
        }
    }
}
