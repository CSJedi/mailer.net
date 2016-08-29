using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Mailer.NET.Mailer.Internal.ConfigFile
{
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class DefaultTransportElement : ConfigurationElement
    {
        private const string TypeKey = "type";
        private const string ParametersKey = "parameters";

        [ConfigurationProperty(TypeKey, IsRequired = true)]
        public string TransportTypeName
        {
            get { return (string)this[TypeKey]; }
            set { this[TypeKey] = value; }
        }

        [ConfigurationProperty(ParametersKey)]
        public ParameterCollection Parameters
        {
            get { return (ParameterCollection)base[ParametersKey]; }
        }

        public Type GetTransportType()
        {
            return Type.GetType(TransportTypeName, throwOnError: true);
        }
    }
}
