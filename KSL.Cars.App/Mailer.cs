using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace KSL.Cars.App
{
    class Mailer
    {
        private SmtpClient smtp;

        public string FromEmail { get; set; }
        private string Username { get; set; }
        private string Password { get; set; }
        public bool UseSSL { get; set; }

        public Mailer(string fromEmailIn, string smtpHost = "smtp.gmail.com", int portIn = 465)
        {
            smtp = new SmtpClient(smtpHost, portIn);
            FromEmail = fromEmailIn;
        }

        public Mailer(string usernameIn, string passwordIn, string fromEmailIn, string smtpHost = "smtp.gmail.com", int portIn = 465, bool useSSL = true)
        {
            smtp = new SmtpClient(smtpHost, portIn);
            Password = Encryption.Encrypt(passwordIn);
            Username = usernameIn;
            FromEmail = fromEmailIn;
            UseSSL = useSSL;
        }

        /// <summary>
        /// Stores credentials for later use.
        /// </summary>
        /// <param name="usernameIn">Username for the email account</param>
        /// <param name="passwordIn">Plaintext password for email account</param>
        public void StoreCredentials(string usernameIn, string passwordIn)
        {
            Password = Encryption.Encrypt(passwordIn);
            Username = usernameIn;
        }

        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="destEmail"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public void SendMail(string destEmail, string subject, string body)
        {
            MailMessage mail = new MailMessage(FromEmail, destEmail, subject, body);

            mail.IsBodyHtml = true;

            smtp.EnableSsl = UseSSL;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(FromEmail, Encryption.Decrypt(Password));
            
            try { smtp.Send(mail); }
            catch (Exception ex) { EventLogger.LogEvent("Error sending email: " + ex.Message); }
        }
    }
}


