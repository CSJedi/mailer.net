using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var response = GenerateDebugEmailResponse();
            return response;
        }

        public override async Task<EmailResponse> SendEmailAsync(Email email)
        {
            var response = await Task.FromResult(GenerateDebugEmailResponse());
            return response;
        }

        private EmailResponse GenerateDebugEmailResponse()
        {
            return new EmailResponse
            {
                Success = Success,
                Message = Success ? "Email successfully sent" : "Undefined Error"
            };
        }
    }
}
