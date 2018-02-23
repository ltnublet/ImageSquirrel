using System;
using System.Collections.Generic;

namespace ImageSquirrel.DataSources.External
{
    public interface IConfigurationRequirement
    {
        IEnumerable<IConfigurationRequirement> DependsOn { get; }

        string Description { get; }

        IEnumerable<IConfigurationRequirement> ExclusiveWith { get; }

        bool IsCollection { get; }

        bool IsOptional { get; }

        ConfigurationRequirementType OfType { get; }

        string Name { get; }

        Exception Validate(object instance);
    }
}
