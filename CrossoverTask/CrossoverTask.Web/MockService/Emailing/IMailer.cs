using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CrossoverTask.Service.Emailing
{    
    interface IMailer
    {
        void SendMail(MailMessage msg);
    }
}
