# About

Mailer.NET is a library to help you to send emails. You can use templates and different transports like Mailgun and Smtp.

# Install Mailer.NET via nuget
```
Install-Package Mailer.NET 
```
# Add ConfigSection of Mailer.Net

```xml
  <configSections>
    <section name="mailernet" type="Mailer.NET.Mailer.Internal.ConfigFile.MailerNetSection, Mailer.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" requirePermission="false" />
  </configSections>
```

# Configure Mailer.NET to use smtp

Put the following snippet on your app.config or web.config

```xml
<mailernet>
    <defaultTransport type="Mailer.NET.Mailer.Transport.SmtpTransport, Mailer.NET">
      <parameters>
        <parameter value="host"/>
        <parameter value="port" type="System.Int32"/>
        <parameter value="user"/>
        <parameter value="password"/>
        <parameter value="useOrNotSsl" type="System.Boolean"/>
      </parameters>
    </defaultTransport>
  </mailernet>
```

# Configure Mailer.NET to use Mailgun

Put the following snippet on your app.config or web.config

```xml
<mailernet>
    <defaultTransport type="Mailer.NET.Mailer.Transport.MailgunTransport, Mailer.NET">
      <parameters>
        <parameter value="damain"/>
        <parameter value="apikey"/>
      </parameters>
    </defaultTransport>
  </mailernet>
  ```
  
# Send email with a simple message

```csharp
Email email = new Email(EmailContentType.Text)
{
	From = new Contact() {Name = "sender", Email = "email_sender"}
};
email.AddTo("dest_email", "dest");
email.Subject = "subject";
email.Message = "simple message"
bool sent = email.Send();
```
  
# Send email with template

**Html template file should be locate in APP_DIR\Email\Template\template.html**

```csharp
Email email = new Email(EmailContentType.Html)
{
	From = new Contact() {Name = "sender", Email = "email_sender"}
};
email.AddTo("dest_email", "dest");
email.Subject = "subject";
email.Template = "template";
email.AddTemplateVar("person", "teste");
email.AddTemplateVar("number", "123");
bool sent = email.Send();
```

## Template file
```html
<html>
<head>
    <meta charset="utf-8" />
</head>
<body>
    <h4>Template Example</h4>
    <p>Hello $person, this is a example template</p>
</body>
</html>
```

**the $person will be replaced to teste**

## Configure transport dinamically

```csharp
MailgunTransport transport = new MailgunTransport("domain", "apikey");
Email email = new Email(transport,EmailContentType.Html)
```

## How to add your custom transport

Create your class extending Mailer.NET.Transport.AbstractTransport and implement SendEmail method. 

```csharp
public class CustomTransport : AbstractTransport
{
    public override EmailResponse SendEmail(Email email)
    {
        //your logic to send email here
        return new EmailResponse("Message", true);
    }
}
```