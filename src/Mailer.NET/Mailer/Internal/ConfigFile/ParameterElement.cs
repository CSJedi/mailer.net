using System;
using System.Configuration;
using System.Globalization;

namespace Mailer.NET.Mailer.Internal.ConfigFile
{
    internal class ParameterElement : ConfigurationElement
    {
        private const string ValueKey = "value";
        private const string TypeKey = "type";

        public ParameterElement(int key)
        {
            Key = key;
        }

        internal int Key { get; private set; }

        [ConfigurationProperty(ValueKey, IsRequired = true)]
        public string ValueString
        {
            get => (string)this[ValueKey];
            set => this[ValueKey] = value;
        }

        [ConfigurationProperty(TypeKey, DefaultValue = "System.String")]
        public string TypeName
        {
            get => (string)this[TypeKey];
            set => this[TypeKey] = value;
        }

        public object GetTypedParameterValue()
        {
            var type = Type.GetType(TypeName, throwOnError: true);

            return Convert.ChangeType(ValueString, type, CultureInfo.InvariantCulture);
        }
    }
}
