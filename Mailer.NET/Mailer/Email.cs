using System;
using System.Collections.Generic;
using Mailer.NET.Mailer.Rendering;
using Mailer.NET.Mailer.Transport;
using Mailer.NET.Mailer.Internal;
using Mailer.NET.Mailer.Response;

namespace Mailer.NET.Mailer
{
    public class Email
    {
        public AbstractTransport Transport { get; set; }
        public String Message { get; set; }
        public String Template { get; set; }
        public List<TemplateVar> TemplateVars { get; set; }
        public List<Contact> To { get; set; }
        public List<Contact> Cco { get; set; }
        public List<Contact> Bco { get; set; }
        public Contact From { get; set; }
        public String Subject { get; set; }
        public EmailContentType Type { get; set; }
        public List<Attachment> Attachments { get; set; }
        public Boolean HasReadNotification { get; set; }

        public Email(AbstractTransport transport, EmailContentType type = EmailContentType.Text)
        {
            Transport = transport;
            Type = type;
        }

        public Email(EmailContentType type = EmailContentType.Text)
        {
            Transport = AppConfig.DefaultInstance.TryGetDefaultTransport();
            Type = type;
        }

        public void AddTo(string email, string name = null)
        {
            if (To == null)
            {
                To = new List<Contact>();
            }
            To.Add(new Contact(){Email = email, Name=name});
        }

        public void AddCco(string email, string name = null)
        {
            if (Cco == null)
            {
                Cco = new List<Contact>();
            }
            Cco.Add(new Contact() { Email = email, Name = name });
        }

        public void AddBco(string email, string name = null)
        {
            if (Bco == null)
            {
                Bco = new List<Contact>();
            }
            Bco.Add(new Contact() { Email = email, Name = name });
        }

        public void AddTemplateVar(string name, object data)
        {
            if (TemplateVars == null)
            {
                TemplateVars = new List<TemplateVar>();
            }
            TemplateVars.Add(new TemplateVar(){Name = name, Data = data});
        }

        public void AddAttachment(Attachment file)
        {
            if (Attachments == null)
            {
                Attachments = new List<Attachment>();
            }
            Attachments.Add(file);
        }

        public EmailResponse Send()
        {
            if (From == null)
            {
                throw new InvalidOperationException("The From is not defined!");
            }

            if (To == null && Bco == null && Cco == null)
            {
                throw new InvalidOperationException("You need specify one destination on to, cc or bcc.");
            }

            if (Transport == null)
            {
                throw new InvalidOperationException("Transport is not defined!");
            }

            if (!String.IsNullOrEmpty(Template))
            {
                Message = EmailRender.RenderEmail(this);
            }

            return Transport.SendEmail(this);
        }
    }
}
