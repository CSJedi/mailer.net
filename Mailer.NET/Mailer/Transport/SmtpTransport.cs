using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Mailer.NET.Mailer.Response;

namespace Mailer.NET.Mailer.Transport
{
    public class SmtpTransport:AbstractTransport
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public bool Ssl { get; set; }

        public SmtpTransport()
        {
            
        }
        public SmtpTransport(string host, int port, string user, string password, bool ssl)
        {
            Host = host;
            Port = port;
            User = user;
            Password = password;
            Ssl = ssl;
        }

        public override EmailResponse SendEmail(Email email)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(email.From.Email, email.From.Name);

                if (email.To != null)
                {
                    email.To.ForEach(delegate (Contact address)
                    {
                        mail.To.Add(new MailAddress(address.Email, address.Name));
                    });
                }
                    
                if(email.Bco != null)
                {
                    email.Bco.ForEach(delegate (Contact address)
                    {
                        mail.To.Add(new MailAddress(address.Email, address.Name));
                    });
                }

                if (email.Cco != null)
                {
                    email.Cco.ForEach(delegate (Contact address)
                    {
                        mail.To.Add(new MailAddress(address.Email, address.Name));
                    });
                }

                var avHtml = AlternateView.CreateAlternateViewFromString(email.Message, null, MediaTypeNames.Text.Html);

                if (email.Attachments != null)
                {
                    foreach (var attachment in email.Attachments)
                    {
                        
                        if (!string.IsNullOrEmpty(attachment.ContentId))
                        {
                            var inline = new LinkedResource(attachment.File, attachment.MimeType)
                            {
                                ContentId = attachment.ContentId
                            };
                            avHtml.LinkedResources.Add(inline);
                        }
                        else
                        {
                            var anexo = new System.Net.Mail.Attachment(attachment.File, attachment.MimeType);
                            mail.Attachments.Add(anexo);
                        }
                    }

                    mail.AlternateViews.Add(avHtml);
                }

                mail.IsBodyHtml = email.Type == EmailContentType.Html;
                mail.Subject = email.Subject;
                mail.Body = email.Message;

                if (email.HasReadNotification)
                {
                    mail.Headers.Add("Disposition-Notification-To", email.From.Email);
                }

                var objSmtp = new System.Net.Mail.SmtpClient
                {
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(User, Password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Host = Host,
                    Port = Port,
                    EnableSsl = Ssl
                };

                EmailResponse emailResponse = new EmailResponse();

                try
                {
                    objSmtp.Send(mail);
                    emailResponse.Success = true;
                    emailResponse.Message = "Email successfully sent";
                }
                catch (Exception erro)
                {
                    emailResponse.Success = false;
                    emailResponse.Message = erro.Message;
                }
                return emailResponse;
            }
        }
    }
}
