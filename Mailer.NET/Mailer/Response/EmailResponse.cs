using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.NET.Mailer.Response
{
    /// <summary>
    /// Corresponds to a class responsible to represent a return of send email, on success or fail.
    /// </summary>
    public class EmailResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
