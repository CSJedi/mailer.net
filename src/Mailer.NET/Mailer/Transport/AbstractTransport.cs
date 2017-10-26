using System.Net;
using System.Threading.Tasks;
using Mailer.NET.Mailer.Response;

namespace Mailer.NET.Mailer.Transport
{
    public abstract class AbstractTransport
    {
        public WebProxy Proxy { get; set; }
        public abstract EmailResponse SendEmail(Email email);
        public abstract Task<EmailResponse> SendEmailAsync(Email email);
    }
}
