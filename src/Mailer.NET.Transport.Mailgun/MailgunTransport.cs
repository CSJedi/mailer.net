using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Mailer.NET.Mailer.Response;
using Mailer.NET.Mailer.Transport;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;

namespace Mailer.NET.Transport
{
    public class MailgunTransport : AbstractTransport
    {
        public string Domain { get; set; }
        public string Apikey { get; set; }

        public MailgunTransport(string domain, string apiKey)
        {
            Domain = domain;
            Apikey = apiKey;
        }

        public override EmailResponse SendEmail(Email email)
        {
            var response = SendMailgunMessage(email);
            return ParseMailgunResponse(response);
        }

        public override async Task<EmailResponse> SendEmailAsync(Email email)
        {
            var response = await SendMailgunMessageAsync(email);
            return ParseMailgunResponse(response);
        }

        private IRestResponse SendMailgunMessage(Email email)
        {
            var client = GenerateRestClient();
            var request = GenerateMailgunRequest(email);

            return client.Execute(request);
        }

        private async Task<IRestResponse> SendMailgunMessageAsync(Email email)
        {
            var client = GenerateRestClient();
            var request = GenerateMailgunRequest(email);

            return await client.ExecuteTaskAsync(request);
        }

        private IRestClient GenerateRestClient()
        {
            IRestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
                new HttpBasicAuthenticator("api",
                    Apikey);
            client.Proxy = Proxy;
            return client;
        }

        private IRestRequest GenerateMailgunRequest(Email email)
        {
            IRestRequest request = new RestRequest();
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
                    request.AddFile(string.IsNullOrEmpty(attachment.ContentId) ? "attachment" : "inline", attachment.File);
                }
            }

            if (email.HasReadNotification)
            {
                request.AddParameter("h:Disposition-Notification-To", email.From.Email);
            }

            if (!String.IsNullOrEmpty(email.ReplyTo))
            {
                request.AddParameter("h:Reply-To", email.ReplyTo);
            }

            request.AddParameter("subject", email.Subject);
            request.AddParameter(email.Type == EmailContentType.Html ? "html" : "text", email.Message);
            request.Method = Method.POST;
            return request;
        }

        private EmailResponse ParseMailgunResponse(IRestResponse response)
        {
            var emailResponse = new EmailResponse();
            if (response.StatusCode == HttpStatusCode.Accepted || response.StatusCode == HttpStatusCode.OK)
            {
                emailResponse.Success = true;
                emailResponse.Message = "Email successfully sent";
            }
            else
            {
                emailResponse.Success = false;
                if (response.ErrorMessage != null)
                {
                    emailResponse.Message = response.ErrorMessage;
                }
                else
                {
                    if (response.ContentType == "application/json")
                    {
                        var responseCollection = new JsonDeserializer().Deserialize<Dictionary<string, object>>(response);
                        if (responseCollection.Count > 0)
                        {
                            emailResponse.Message = responseCollection["message"].ToString();
                        }
                        else
                        {
                            emailResponse.Message = "Undefined Error";
                        }
                    }
                    else
                    {
                        emailResponse.Message = response.Content;
                    }
                }

            }

            return emailResponse;
        }
    }
}
