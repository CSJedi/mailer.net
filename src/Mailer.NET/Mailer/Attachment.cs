using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.NET.Mailer
{
    /// <summary>
    /// Class to represent attachments on an Email.
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// Represent a full location of an file.
        /// </summary>
        public string File { get; set; }
        /// <summary>
        /// Represent an identifier to a inline attachment, example: src="cid:mycontentid"
        /// </summary>
        public string ContentId { get; set; }
        /// <summary>
        /// MimeType represent a type of an attachment, for exemple: MediaTypes.Image.Jpeg, MediaTypeNames.Application.Pdf
        /// </summary>
        public string MimeType { get; set; }

        public Attachment()
        {
            MimeType = MediaTypeNames.Application.Octet;
        }
    }
}
