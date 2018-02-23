using System;
using System.Security;

namespace ImageSquirrel.DataSources.External
{
    public sealed class ConfigurationRequirementType
    {
        public static ConfigurationRequirementType String { get; } =
            new ConfigurationRequirementType(typeof(string));

        public static ConfigurationRequirementType Int32 { get; } =
            new ConfigurationRequirementType(typeof(int));

        public static ConfigurationRequirementType Int64 { get; } =
            new ConfigurationRequirementType(typeof(long));

        public static ConfigurationRequirementType Bool { get; } =
            new ConfigurationRequirementType(typeof(bool));

        public static ConfigurationRequirementType Path { get; } =
            new ConfigurationRequirementType(typeof(FilePath));

        public static ConfigurationRequirementType Uri { get; } =
            new ConfigurationRequirementType(typeof(Uri));

        public static ConfigurationRequirementType SecureString { get; } =
            new ConfigurationRequirementType(typeof(SecureString));

        public ConfigurationRequirementType(Type type)
        {
            this.Type = type;
        }

        public Type Type { get; private set; }
    }
}
