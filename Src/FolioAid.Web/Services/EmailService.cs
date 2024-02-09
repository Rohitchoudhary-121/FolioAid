using Application.Interface;
using System.Net.Mail;
using System.Net;
using Domain.Common;
using Microsoft.Extensions.Hosting;
using Azure.Core;
using static System.Net.WebRequestMethods;

namespace FolioAid.Services
{
    public class EmailService : IEmailTemplate
    {
        private readonly EmailSMTPConfiguration _EmailSMTPConfiguration;
        private readonly IWebHostEnvironment _hostEnvironment;
        public EmailService(EmailSMTPConfiguration EmailSMTPConfiguration, IWebHostEnvironment hostEnvironment)
        {
            _EmailSMTPConfiguration = EmailSMTPConfiguration;
            _hostEnvironment = hostEnvironment;

        }



        public void SendActivationLink(string recipientEmail, string activationToken)
        {
            var path = $"{_EmailSMTPConfiguration.BaseUrl}/#/activate={activationToken}";
            var fullName = recipientEmail;
            var userName = fullName.Substring(0, fullName.IndexOf('@'));
            string FilePath = Path.Combine(_hostEnvironment.WebRootPath, "RegitserUserTemplate.html");
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[activationstring]", path);
            MailText = MailText.Replace("[userName]", userName);

            SendEmail(recipientEmail, Constant.ActivationLinkSubject, MailText);
        }
        public void SendEmail(string recipientEmail, string subject, string body)
        {

            // Set up SMTP client
            var smtpClient = GetClient();
            // Create email message
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_EmailSMTPConfiguration.FromEmail),
                Subject = subject,
                Body = body
            };
            mailMessage.To.Add(recipientEmail);
            mailMessage.IsBodyHtml = true;
            // Send the email
            try
            {
                smtpClient.Send(mailMessage);
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
                // Handle and log the exception
            }
        }


        private SmtpClient GetClient()
        {
            // Set up SMTP client
            var smtpClient = new SmtpClient(_EmailSMTPConfiguration.Host, _EmailSMTPConfiguration.Port)
            {
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(_EmailSMTPConfiguration.NetworkEmail, _EmailSMTPConfiguration.NetworkPassword)
            };
            return smtpClient;
        }
        void IEmailTemplate.SendResetPasswordLink(string recipientEmail, string token)
        {
            var body = $"Dear User,\n\nPlease Create  New Password your account by clicking the following link: " +

                       $"{_EmailSMTPConfiguration.BaseUrl}/resetPassword?Email={recipientEmail}&token={token}\n\n" +
                        $"Thank you,\nYour Application";
            var path = $"{_EmailSMTPConfiguration.BaseUrl}/resetpassword?email={recipientEmail}&token={token}";
            string FilePath = Path.Combine(_hostEnvironment.WebRootPath, "Template/template.html");
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[recipientEmail & token]", path);
            SendEmail(recipientEmail, Constant.PasswordResetLinkSubject, MailText);





        }
    }
}




