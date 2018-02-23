using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ImageSquirrel.DataSources.External;
using ImageSquirrel.DataSources.ReferenceImplementation.Internationalization;

namespace ImageSquirrel.DataSources.ReferenceImplementation
{
    public class ConfigurationRequirement : IConfigurationRequirement
    {
        private Func<object, Exception> validator;

        public ConfigurationRequirement(
            string name,
            string description,
            ConfigurationRequirementType ofType,
            bool isCollection,
            bool isOptional,
            Func<object, Exception> validator,
            IEnumerable<IConfigurationRequirement> dependsOn = null,
            IEnumerable<IConfigurationRequirement> exclusiveWith = null)
        {
            ConfigurationRequirement.ThrowIfBadString(name, nameof(name));
            ConfigurationRequirement.ThrowIfBadString(description, nameof(description));
            this.Name = name;
            this.Description = description;
            this.OfType = ofType;
            this.IsCollection = isCollection;
            this.IsOptional = isOptional;
            this.DependsOn = dependsOn ?? new IConfigurationRequirement[0];
            this.ExclusiveWith = exclusiveWith ?? new IConfigurationRequirement[0];
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public IEnumerable<IConfigurationRequirement> DependsOn { get; private set; }

        public string Description { get; private set; }

        public IEnumerable<IConfigurationRequirement> ExclusiveWith { get; private set; }

        public bool IsCollection { get; private set; }

        public bool IsOptional { get; private set; }

        public string Name { get; private set; }

        public ConfigurationRequirementType OfType { get; private set; }

        public static IConfigurationRequirement String(
            string name,
            string description,
            bool isOptional,
            IEnumerable<IConfigurationRequirement> dependsOn = null,
            IEnumerable<IConfigurationRequirement> exclusiveWith = null)
        {
            return new ConfigurationRequirement(
                name,
                description,
                ConfigurationRequirementType.String,
                false,
                isOptional,
                x =>
                {
                    if (x == null)
                    {
                        return new ArgumentNullException();
                    }
                    else if (!ConfigurationRequirementType.String.Type.IsAssignableFrom(x.GetType()))
                    {
                        return new InvalidCastException(
                            string.Format(
                                en_us.Culture,
                                en_us.Requirement_SuppliedObjectIsOfWrongType,
                                ConfigurationRequirementType.String.Type.ToString()));
                    }

                    return null;
                },
                dependsOn,
                exclusiveWith);
        }

        public static IConfigurationRequirement Path(
            string name,
            string description,
            bool isOptional,
            IEnumerable<IConfigurationRequirement> dependsOn = null,
            IEnumerable<IConfigurationRequirement> exclusiveWith = null)
        {
            return new ConfigurationRequirement(
                name,
                description,
                ConfigurationRequirementType.Path,
                false,
                isOptional,
                x =>
                {
                    if (x == null)
                    {
                        return new ArgumentNullException();
                    }
                    else if (!ConfigurationRequirementType.Path.Type.IsAssignableFrom(x.GetType()))
                    {
                        return new InvalidCastException(
                            string.Format(
                                en_us.Culture,
                                en_us.Requirement_SuppliedObjectIsOfWrongType,
                                ConfigurationRequirementType.Path.Type.ToString()));
                    }

                    return null;
                },
                dependsOn,
                exclusiveWith);
        }

        public static IConfigurationRequirement Long(
            string name,
            string description,
            bool isOptional,
            IEnumerable<IConfigurationRequirement> dependsOn = null,
            IEnumerable<IConfigurationRequirement> exclusiveWith = null)
        {
            return new ConfigurationRequirement(
                name,
                description,
                ConfigurationRequirementType.Int64,
                false,
                isOptional,
                x =>
                {
                    if (x == null)
                    {
                        return new ArgumentNullException();
                    }
                    else if (!ConfigurationRequirementType.Int64.Type.IsAssignableFrom(x.GetType()))
                    {
                        return new InvalidCastException(
                            string.Format(
                                en_us.Culture,
                                en_us.Requirement_SuppliedObjectIsOfWrongType,
                                ConfigurationRequirementType.Int64.Type.ToString()));
                    }

                    return null;
                },
                dependsOn,
                exclusiveWith);
        }

        public Exception Validate(object instance)
        {
            try
            {
                return this.validator.Invoke(instance);
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public override string ToString()
        {
            const string @null = "<null>";

            return string.Format(
                en_us.Culture,
                "{{ \"{0}\": \"{1}\", \"{2}\": \"{3}\", \"{4}\": \"{5}\", \"{6}\": \"{7}\", \"{8}\": \"{9}\", \"{10}\": [{11}], \"{12}\": [{13}] }}",
                nameof(this.Name),
                this.Name,
                nameof(this.OfType),
                this.OfType,
                nameof(this.IsCollection),
                this.IsCollection,
                nameof(this.IsOptional),
                this.IsOptional,
                nameof(this.Description),
                this.Description,
                nameof(this.DependsOn),
                string.Join(", ", this.DependsOn.Select(x => "\"" + (x?.Name ?? @null) + "\"")),
                nameof(this.ExclusiveWith),
                string.Join(", ", this.ExclusiveWith.Select(x => "\"" + (x?.Name ?? @null) + "\"")));
        }

        [DebuggerHidden]
        private static void ThrowIfBadString(string @string, string name)
        {
            if (@string == null)
            {
                throw new ArgumentNullException(name);
            }
            else if (string.IsNullOrWhiteSpace(@string))
            {
                throw new ArgumentException(en_us.Requirement_StringMustBeNonWhitespace, name);
            }
        }
    }
}
