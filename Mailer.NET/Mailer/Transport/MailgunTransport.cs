using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace Mailer.NET.Mailer.Transport
{
    public class MailgunTransport : AbstractTransport
    {
        public string Domain { get; set; }
        public string Apikey { get; set; }

        public override bool SendEmail(Email email)
        {
            IRestResponse response = SendMailgunMessage(email);
            return response.StatusCode == HttpStatusCode.Accepted || response.StatusCode == HttpStatusCode.OK;
        }

        private IRestResponse SendMailgunMessage(Email email)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
                new HttpBasicAuthenticator("api",
                    Apikey);
            client.Proxy = Proxy;
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                Domain, ParameterType.UrlSegment);
            request.Resource = Domain + "/messages";
            request.AddParameter("from", email.From.ToString());

            if (email.To != null)
            {
                foreach (var contato in email.To)
                {
                    request.AddParameter("to", contato.Email);
                }
            }

            if (email.Cco != null)
            {
                foreach (var contato in email.Cco)
                {
                    request.AddParameter("cc", contato.Email);
                }
            }

            if (email.Bco != null)
            {
                foreach (var contato in email.Bco)
                {
                    request.AddParameter("bcc", contato.Email);
                }
            }

            if (email.Attachments != null)
            {
                foreach (var attachment in email.Attachments)
                {
                    request.AddFile("attachment", attachment.File);
                }
            }

            request.AddParameter("subject", email.Subject);
            if (email.Type == EmailContentType.Html)
            {
                request.AddParameter("html", email.Message);
            }
            else
            {
                request.AddParameter("text", "Testing some Mailgun awesomness!");
            }
            request.Method = Method.POST;
            return client.Execute(request);
        }
    }
}
