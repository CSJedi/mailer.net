using System.Net;
using Mailer.NET.Mailer.Response;

namespace Mailer.NET.Mailer.Transport
{
    public abstract class AbstractTransport
    {
        public WebProxy Proxy { get; set; }
        public abstract EmailResponse SendEmail(Email email);
    }
}
