using CrossoverTask.Data.Entities;
using CrossoverTask.Service.Pdf;
using CrossoverTask.Service.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace CrossoverTask.Service.Emailing
{
    class ResultsMailer
    {
        #region Constants
        private static readonly string CompanyMail = ConfigurationSettings.AppSettings["CompanyMail"];
        //Idealy this would be fetched from a localisation resource
        private const string NoResultsMsgBody= "Dear Sir/Madam, We couldn't find any results matching your search. \n Yours faithfully,\n John Doe";
        private const string MultipleResultsMsgBodyTemplate = "Dear Sir/Madam,\n We have attached the search results {0}-{1} out of {2} as separate pdf files. \n Yours faithfully,\n John Doe";
        private const string MsgSubject= "Convict search query results";
        #endregion
        private GmailSmtpClient MailClient { get; set; }

        public ResultsMailer(IMailer mailClient = null)
        {
            //Idealy this would be injected
            if (mailClient == null)
            {
                MailClient = new GmailSmtpClient();
            }
        }

        /// <summary>
        /// Sends an email to the specified recipient consisting of pdf summaries for all the convics in lst
        /// One pdf per convict, maximum 10 pdfs per mail. If there are more than 10 results multiple mails are sent.
        /// In the event of an empty list, a mail informing that no results were found is sent.
        /// </summary>
        /// <param name="lst">List of all the convicts. If it is null or empty a mail informing that no convicts matched the search criteria is sent</param>
        /// <param name="mail">The recipients email addres</param>
        public void SendSearchResults(List<Convict> lst,string mail)
        {
            //No results found
            if (lst==null || (lst!=null && lst.Count <= 0))
            {               
                var msg = CreateNoResultsFoundMsg(mail);
                MailClient.SendMail(msg);
                return;
            }

            //Many results found, create one of more mails
            int sentAtachmentsCount = 0;
            int totalAttachmentsCount = lst.Count;
            var currentResultsBatch = new List<Convict>();           
            for (int i = 0; (i < (totalAttachmentsCount / 10) + 1) && lst.Count > 0; i++)
            {
                //Select at most first 10 members
                currentResultsBatch.Clear();
                for (int j = 0; j < 10 &&  lst.Count>0; j++)
                {
                    currentResultsBatch.Add(lst[0]);
                    lst.RemoveAt(0);
                }

                //Create and send msg
                using (var msPool = new DisposableResourceTracker<Stream>())
                {
                    var msg = CreateMessage(sentAtachmentsCount, 
                                            sentAtachmentsCount + currentResultsBatch.Count,
                                            totalAttachmentsCount, 
                                            currentResultsBatch,
                                            msPool,
                                            mail);                    
                   
                    MailClient.SendMail(msg);                   
                }

                //refresh sent results count
                sentAtachmentsCount += currentResultsBatch.Count;                              
            }
        }

        private MailMessage CreateNoResultsFoundMsg(string reciever)
        {
            var msg = new MailMessage(CompanyMail, reciever);
            msg.Subject = MsgSubject;
            msg.Body = NoResultsMsgBody;
            return msg;
        }

        private MailMessage CreateMessage(int startRange,int endRange,int allAtachmentsCount, List<Convict> convicts, DisposableResourceTracker<Stream> msPool,string reciever)
        {
            var pdfGenerator=new ConvictPdfGenerator();
            var msg = new MailMessage(CompanyMail, reciever);

            //Idealy this strings would be fetched from a localization resourse
            msg.Subject = MsgSubject;
            msg.Body = string.Format(MultipleResultsMsgBodyTemplate,
                                                         startRange,
                                                         endRange,
                                                         allAtachmentsCount);
            foreach (var convict in convicts)
            {
                var pdfStream = new MemoryStream();
                msPool.Track(pdfStream);
                pdfGenerator.CreatePdf(convict, pdfStream);
                pdfStream.Position = 0;
                msg.Attachments.Add(new Attachment(pdfStream,string.Format("{0}_{1}.pdf",convict.FirstName,convict.LastName), MediaTypeNames.Application.Pdf));                
            }

            return msg;
        }
        
    }
}
