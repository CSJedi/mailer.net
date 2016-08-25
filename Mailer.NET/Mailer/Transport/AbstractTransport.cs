using System.Net;

namespace Mailer.NET.Mailer.Transport
{
    public abstract class AbstractTransport
    {
        public WebProxy Proxy { get; set; }
        public abstract bool SendEmail(Email email);
    }
}
