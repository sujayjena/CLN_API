using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using Microsoft.AspNetCore.Http;

namespace CLN.Application.Helpers
{
    public interface IEmailHelper
    {
        Task<bool> SendEmail(string emailSubject, string emailContent, string receiverEmail, List<Attachment> files = null);
    }

    public class EmailHelper : IEmailHelper
    {
        private readonly IEmailConfigRepository _emailConfigRepository;

        public EmailHelper(IEmailConfigRepository emailConfigRepository)
        {
            _emailConfigRepository = emailConfigRepository;
        }

        public async Task<bool> SendEmail(string emailSubject, string emailContent, string receiverEmail, List<Attachment> files = null)
        {
            bool result = false;

            try
            {
                string sSmtpServerName = string.Empty;
                string sSmtpServer = string.Empty;
                string sSmtpUsername = string.Empty;
                string sSmtpPassword = string.Empty;
                bool? bSmtpUseDefaultCredentials = false;
                bool? bSmtpEnableSSL = false;
                int iSmtpPort = 0;
                int? iSmtpTimeout = 0;
                string sFromAddress = string.Empty;
                string sEmailSenderName = string.Empty;
                string sEmailSenderCompanyLogo = string.Empty;
                bool? bIsActive = false;

                #region Email Config

                var vEmailConfig_Search = new EmailConfig_Search() { IsActive = true };
                var vEmailConfigObj = _emailConfigRepository.GetEmailConfigList(vEmailConfig_Search).Result.ToList().FirstOrDefault();
                if (vEmailConfigObj != null)
                {
                    sSmtpServerName = vEmailConfigObj.SmtpServerName;
                    sSmtpServer = vEmailConfigObj.SmtpServer;
                    sSmtpUsername = vEmailConfigObj.SmtpUsername;
                    sSmtpPassword = vEmailConfigObj.SmtpPassword;
                    bSmtpUseDefaultCredentials = vEmailConfigObj.SmtpUseDefaultCredentials;
                    bSmtpEnableSSL = vEmailConfigObj.SmtpEnableSSL;
                    iSmtpTimeout = vEmailConfigObj.SmtpTimeout;
                    sFromAddress = vEmailConfigObj.FromAddress;
                    sEmailSenderName = vEmailConfigObj.EmailSenderName;
                    sEmailSenderCompanyLogo = vEmailConfigObj.EmailSenderCompanyLogo;
                    bIsActive = vEmailConfigObj.IsActive;
                }

                #endregion

                if (vEmailConfigObj != null)
                {
                    await Task.Run(() =>
                    {
                        using (MailMessage mail = new MailMessage())
                        {
                            if (!string.IsNullOrWhiteSpace(receiverEmail))
                            {
                                mail.From = new MailAddress(sFromAddress);
                                mail.To.Add(receiverEmail);
                                mail.Subject = emailSubject;
                                mail.Body = emailContent;
                                mail.IsBodyHtml = true;

                                if (files != null)
                                {
                                    for (int f = 0; f < files.Count; f++)
                                    {
                                        mail.Attachments.Add(new Attachment(files[f].ContentStream, files[f].Name));
                                    }
                                }

                                using (SmtpClient smtp = new SmtpClient(sSmtpServer, iSmtpPort))
                                {
                                    smtp.Credentials = new NetworkCredential(sSmtpUsername, sSmtpPassword);
                                    smtp.EnableSsl = Convert.ToBoolean(bSmtpEnableSSL);

                                    //smtp.SendAsync(mail, "EmailAlert");
                                    try
                                    {
                                        smtp.Send(mail);

                                        result = true;
                                    }
                                    catch (Exception ex)
                                    {
                                        result = false;
                                    }
                                }
                            }
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

    }
}
