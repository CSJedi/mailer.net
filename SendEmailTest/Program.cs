using System.Net;
using Mailer.NET.Mailer;
using Mailer.NET.Mailer.Transport;

namespace SendEmailTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MailgunTransport transport = new MailgunTransport()
            {
                Domain = "",
                Apikey = ""
            };
            Email email = new Email(transport, EmailContentType.Html)
            {
                From = new Contact() {Name = "sender", Email = "email"}
            };
            email.AddTo("email", "name");
            email.Subject = "subject";
            email.Template = "template";
            email.AddTemplateVar("person", "teste");
            email.AddTemplateVar("number", "123");
            email.Send();
        }
    }
}
