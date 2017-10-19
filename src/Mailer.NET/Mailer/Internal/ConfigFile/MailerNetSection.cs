using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Mailer.NET.Mailer.Internal.ConfigFile
{
    internal class MailerNetSection : ConfigurationSection
    {
        private const string DefaultTransportKey = "defaultTransport";

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [ConfigurationProperty(DefaultTransportKey)]
        public virtual DefaultTransportElement DefaultTransport
        {
            get { return (DefaultTransportElement)this[DefaultTransportKey]; }
            set { this[DefaultTransportKey] = value;}
        }
    }
}
