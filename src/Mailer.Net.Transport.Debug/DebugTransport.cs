using Mailer.NET.Mailer;
using Mailer.NET.Mailer.Response;
using Mailer.NET.Mailer.Transport;

namespace Mailer.Net.Transport
{
    public class DebugTransport : AbstractTransport
    {
        public bool Success { get; set; }

        public DebugTransport(bool success)
        {
            Success = success;
        }

        public override EmailResponse SendEmail(Email email)
        {
            var response = new EmailResponse
            {
                Success = Success,
                Message = Success ? "Email successfully sent" : "Undefined Error"
            };
            return response;
        }
    }
}
