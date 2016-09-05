using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

                if (email.Attachments != null)
                {
                    foreach (var attachment in email.Attachments)
                    {
                        System.Net.Mail.Attachment anexo = new System.Net.Mail.Attachment(attachment.File, System.Net.Mime.MediaTypeNames.Application.Octet);

                        mail.Attachments.Add(anexo);
                    }
                }

                mail.IsBodyHtml = email.Type == EmailContentType.Html;
                mail.Subject = email.Subject;
                mail.Body = email.Message;

                System.Net.Mail.SmtpClient objSmtp = new System.Net.Mail.SmtpClient();

                objSmtp.UseDefaultCredentials = false;
                objSmtp.Credentials = new System.Net.NetworkCredential(User, Password);
                objSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtp.Host = Host;
                objSmtp.Port = Port;
                objSmtp.EnableSsl = Ssl;

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
