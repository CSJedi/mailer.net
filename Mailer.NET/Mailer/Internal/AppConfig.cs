using Mailer.NET.Mailer.Internal.ConfigFile;
using Mailer.NET.Mailer.Transport;
using Mailer.NET.Properties;
using System;
using System.Configuration;

namespace Mailer.NET.Mailer.Internal
{
    internal class AppConfig
    {
        public const string MailerSectionName = "mailernet";

        private static readonly AppConfig _defaultInstance = new AppConfig();
        private readonly MailerNetSection _mailerNetSettings;

        private readonly Lazy<AbstractTransport> _defaultTransport;
        private readonly Lazy<AbstractTransport> _defaultDefaultTransport =
    new Lazy<AbstractTransport>(() => null, isThreadSafe: true);

        // <summary>
        // Initializes a new instance of AppConfig based on supplied configuration
        // </summary>
        // <param name="configuration"> Configuration to load settings from </param>
        public AppConfig(Configuration configuration)
            : this(
                (MailerNetSection)configuration.GetSection(MailerSectionName))
        {

        }

        private AppConfig()
            : this(
                (MailerNetSection)ConfigurationManager.GetSection(MailerSectionName))
        {
        }

        internal AppConfig(
            MailerNetSection mailerNetSettings)
        {
            _mailerNetSettings = mailerNetSettings ?? new MailerNetSection();

            if (_mailerNetSettings.DefaultTransport.ElementInformation.IsPresent)
            {
                _defaultTransport = new Lazy<AbstractTransport>(
                    () =>
                    {
                        var setting = _mailerNetSettings.DefaultTransport;

                        try
                        {
                            var type = setting.GetTransportType();
                            var args = setting.Parameters.GetTypedParameterValues();
                            return (AbstractTransport)Activator.CreateInstance(type, args);
                        }
                        catch (Exception ex)
                        {
                            throw new InvalidOperationException(
                                String.Format(Resources.SetTransportFromConfigFailed, setting.TransportTypeName), ex);
                        }
                    }, isThreadSafe: true);
            }
            else
            {
                _defaultTransport = _defaultDefaultTransport;
            }
        }

        // <summary>
        // Gets a singleton instance of configuration based on the <see cref="ConfigurationManager" /> for the AppDomain
        // </summary>
        public static AppConfig DefaultInstance
        {
            get { return _defaultInstance; }
        }

        // <summary>
        // Gets the default connection factory based on the configuration
        // </summary>
        public virtual AbstractTransport TryGetDefaultTransport()
        {
            return _defaultTransport.Value;
        }
    }
}
