using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ImageSquirrel.DataSources.External;
using ImageSquirrel.DataSources.ReferenceImplementation.Internationalization;

namespace ImageSquirrel.DataSources.ReferenceImplementation
{
    /// <summary>
    /// Represents an <see cref="IConfigurationRequirement"/> implementation.
    /// </summary>
    public class ConfigurationRequirement : IConfigurationRequirement
    {
        private Func<object, Exception> validator;

        /// <summary>
        /// Instantiates a new <see cref="ConfigurationRequirement"/> instance using the supplied parameters.
        /// </summary>
        /// <param name="name">
        /// The name of the <see cref="ConfigurationRequirement"/>.
        /// </param>
        /// <param name="description">
        /// The description of the <see cref="ConfigurationRequirement"/>.
        /// </param>
        /// <param name="ofType">
        /// The <see cref="ConfigurationRequirementType"/> of the <see cref="ConfigurationRequirement"/>.
        /// </param>
        /// <param name="isCollection">
        /// Indicates whether this <see cref="ConfigurationRequirement"/> is for a single instance of the type
        /// indicated by <paramref name="ofType"/>, or a collection.
        /// </param>
        /// <param name="isOptional">
        /// Indicates whether this <see cref="ConfigurationRequirement"/> is optional.
        /// </param>
        /// <param name="validator">
        /// Validates <see cref="object"/>s to determine if they satisfy the <see cref="ConfigurationRequirement"/>.
        /// </param>
        /// <param name="dependsOn">
        /// A collection of <see cref="IConfigurationRequirement"/>s which this <see cref="ConfigurationRequirement"/>
        /// must be supplied alongside.
        /// </param>
        /// <param name="exclusiveWith">
        /// A collection of <see cref="IConfigurationRequirement"/>s which this <see cref="ConfigurationRequirement"/>
        /// must not be supplied alongside with.
        /// </param>
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

        /// <inheritdoc />
        public IEnumerable<IConfigurationRequirement> DependsOn { get; private set; }

        /// <inheritdoc />
        public string Description { get; private set; }

        /// <inheritdoc />
        public IEnumerable<IConfigurationRequirement> ExclusiveWith { get; private set; }

        /// <inheritdoc />
        public bool IsCollection { get; private set; }

        /// <inheritdoc />
        public bool IsOptional { get; private set; }

        /// <inheritdoc />
        public string Name { get; private set; }

        /// <inheritdoc />
        public ConfigurationRequirementType OfType { get; private set; }

        /// <summary>
        /// Creates a new <see cref="ConfigurationRequirement"/> of type
        /// <see cref="ConfigurationRequirementType.String"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the <see cref="ConfigurationRequirement"/>.
        /// </param>
        /// <param name="description">
        /// The description of the <see cref="ConfigurationRequirement"/>.
        /// </param>
        /// <param name="isOptional">
        /// Indicates whether the <see cref="ConfigurationRequirement"/> is optional.
        /// </param>
        /// <param name="dependsOn">
        /// A collection of <see cref="IConfigurationRequirement"/>s which this <see cref="ConfigurationRequirement"/>
        /// must be supplied alongside.
        /// </param>
        /// <param name="exclusiveWith">
        /// A collection of <see cref="IConfigurationRequirement"/>s which this <see cref="ConfigurationRequirement"/>
        /// must not be supplied alongside with.
        /// </param>
        /// <returns>
        /// A <see cref="ConfigurationRequirement"/> of type <see cref="ConfigurationRequirementType.String"/> with the
        /// supplied properties.
        /// </returns>
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

        /// <summary>
        /// Creates a new <see cref="ConfigurationRequirement"/> of type
        /// <see cref="ConfigurationRequirementType.Path"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the <see cref="ConfigurationRequirement"/>.
        /// </param>
        /// <param name="description">
        /// The description of the <see cref="ConfigurationRequirement"/>.
        /// </param>
        /// <param name="isOptional">
        /// Indicates whether the <see cref="ConfigurationRequirement"/> is optional.
        /// </param>
        /// <param name="dependsOn">
        /// A collection of <see cref="IConfigurationRequirement"/>s which this <see cref="ConfigurationRequirement"/>
        /// must be supplied alongside.
        /// </param>
        /// <param name="exclusiveWith">
        /// A collection of <see cref="IConfigurationRequirement"/>s which this <see cref="ConfigurationRequirement"/>
        /// must not be supplied alongside with.
        /// </param>
        /// <returns>
        /// A <see cref="ConfigurationRequirement"/> of type <see cref="ConfigurationRequirementType.Path"/> with the
        /// supplied properties.
        /// </returns>
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

        /// <summary>
        /// Creates a new <see cref="ConfigurationRequirement"/> of type
        /// <see cref="ConfigurationRequirementType.Int64"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the <see cref="ConfigurationRequirement"/>.
        /// </param>
        /// <param name="description">
        /// The description of the <see cref="ConfigurationRequirement"/>.
        /// </param>
        /// <param name="isOptional">
        /// Indicates whether the <see cref="ConfigurationRequirement"/> is optional.
        /// </param>
        /// <param name="dependsOn">
        /// A collection of <see cref="IConfigurationRequirement"/>s which this <see cref="ConfigurationRequirement"/>
        /// must be supplied alongside.
        /// </param>
        /// <param name="exclusiveWith">
        /// A collection of <see cref="IConfigurationRequirement"/>s which this <see cref="ConfigurationRequirement"/>
        /// must not be supplied alongside with.
        /// </param>
        /// <returns>
        /// A <see cref="ConfigurationRequirement"/> of type <see cref="ConfigurationRequirementType.Int64"/> with the
        /// supplied properties.
        /// </returns>
        public static IConfigurationRequirement Int64(
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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
