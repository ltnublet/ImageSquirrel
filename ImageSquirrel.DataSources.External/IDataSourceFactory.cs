using System.Collections.Generic;

namespace ImageSquirrel.DataSources.External
{
    public interface IDataSourceFactory
    {
        IReadOnlyList<IConfigurationRequirement> Requirements { get; }

        IDataSource MakeDataSource(IReadOnlyDictionary<IConfigurationRequirement, object> bindings);
    }
}
