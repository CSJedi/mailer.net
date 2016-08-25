using NVelocity;
using NVelocity.App;
using System;
using System.IO;
using System.Text;

namespace Mailer.NET.Mailer.Rendering
{
    class EmailRender
    {
        public static string RenderEmail(Email email)
        {
            if (
                !File.Exists(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(),
                    "Email\\Template\\" + email.Template + ".html")))
            {
                throw new InvalidOperationException("Cannot found template " + "'Email\\Template\\" + email.Template + "'.html");
            }

            Velocity.Init();

            var velocityContext = new VelocityContext();
            foreach (var templateVar in email.TemplateVars)
            {
                velocityContext.Put(templateVar.Name, templateVar.Data);
            }

            string template = string.Join(Environment.NewLine, File.ReadAllLines(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(),
                    "Email\\Template\\" + email.Template + ".html")));

            var sb = new StringBuilder();
            Velocity.Evaluate(
                velocityContext,
                new StringWriter(sb),
                "",
                new StringReader(template));
            return sb.ToString();
        }
    }
}
