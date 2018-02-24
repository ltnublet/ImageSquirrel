using System.Collections.Generic;

namespace ImageSquirrel.DataSources.External
{
    /// <summary>
    /// Represents a factory which produces <see cref="IDataSource"/>s from <see cref="IConfigurationRequirement"/>s.
    /// </summary>
    public interface IDataSourceFactory
    {
        /// <summary>
        /// The <see cref="IConfigurationRequirement"/>s accepted by this factory.
        /// </summary>
        IReadOnlyList<IConfigurationRequirement> Requirements { get; }

        /// <summary>
        /// Produces an <see cref="IDataSource"/> instance using the supplied <paramref name="bindings"/>.
        /// </summary>
        /// <param name="bindings">
        /// A mapping of <see cref="IConfigurationRequirement"/> to <see cref="object"/>; used by the factory to
        /// produce the <see cref="IDataSource"/>.
        /// </param>
        /// <returns>
        /// An <see cref="IDataSource"/> produced from the supplied <paramref name="bindings"/>.
        /// </returns>
        IDataSource MakeDataSource(IReadOnlyDictionary<IConfigurationRequirement, object> bindings);
    }
}
