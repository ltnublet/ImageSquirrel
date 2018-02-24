using System;
using System.Collections.Generic;
using ImageSquirrel.Formats.External;

namespace ImageSquirrel.DataSources.External
{
    /// <summary>
    /// Represents a data source containing <see cref="IDataInformation"/>s.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Naming",
        "CA1710:IdentifiersShouldHaveCorrectSuffix",
        Justification = "Implements additional behavior beyond being a simple collection.")]
    public interface IDataSource : IEnumerable<IDataInformation>, IDisposable
    {
        /// <summary>
        /// Indicates that the underlying data source has had a change applied to it.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1009:DeclareEventHandlersCorrectly",
            Justification = "IDataSourceChangeEventArgs is an interface by design, and thus cannot implement the EventArgs class.")]
        event EventHandler<IDataSourceChangeEventArgs> OnChange;

        /// <summary>
        /// The number of <see cref="IDataInformation"/>s contained within this <see cref="IDataSource"/>.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// The name of this <see cref="IDataSource"/>.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Returns a collection of <see cref="IDataSource"/>s contained within this instance.
        /// </summary>
        /// <returns>
        /// An <see cref="IReadOnlyList{T}"/> of <see cref="Type"/> <see cref="IDataSource"/> whose contents are the
        /// <see cref="IDataSource"/>s contained within this instance.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1024:UsePropertiesWhereAppropriate",
            Justification = "We assume the underlying implementation could do work; thus, a property is not appropriate.")]
        IReadOnlyList<IDataSource> GetSubDataSources();
    }
}
