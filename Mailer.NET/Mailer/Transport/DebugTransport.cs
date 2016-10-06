using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mailer.NET.Mailer.Response;

namespace Mailer.NET.Mailer.Transport
{
    public class DebugTransport : AbstractTransport
    {
        public bool Success { get; set; }

        public DebugTransport(bool success)
        {
            Success = success;
        }

        public override Response.EmailResponse SendEmail(Email email)
        {
            var response = new EmailResponse()
            {
                Success = Success,
                Message = (Success ? "Email successfully sent" : "Undefined Error")
            };
            return response;
        }
    }
}
