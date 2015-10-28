using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CrossoverTask.Service.Emailing
{
    /// <summary>
    /// Implementation of the IMailer interface with the gmail credentials
    /// </summary>
    public class GmailSmtpClient: IMailer
    {
        private static readonly string GmailSmptHost=ConfigurationSettings.AppSettings["GmailSmptHost"];
        private static readonly string GmailId = ConfigurationSettings.AppSettings["GmailId"];
        private static readonly string GmailPassword = ConfigurationSettings.AppSettings["GmailPass"];
        
        /// <summary>
        /// Sends a message via Gmail smpt host
        /// </summary>
        /// <param name="msg"></param>
        public void SendMail(MailMessage msg)
        {
            using (var gmailClient = new System.Net.Mail.SmtpClient
            {
                Host = GmailSmptHost,
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(GmailId, GmailPassword)
            })
            {
                gmailClient.Send(msg);
            }            
        }        
    }
}
