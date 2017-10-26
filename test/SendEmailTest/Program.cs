using Mailer.NET.Mailer;

namespace SendEmailTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Email email = new Email(EmailContentType.Html)
            {
                From = new Contact {Name = "sender", Email = "email"}
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
