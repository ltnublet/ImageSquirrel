using ImageSquirrel.Formats.External;

namespace ImageSquirrel.DataSources.External
{
    /// <summary>
    /// Represents a change in an <see cref="IDataSource"/>.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Naming",
        "CA1711:IdentifiersShouldNotHaveIncorrectSuffix",
        Justification = "Implementors of this interface are expected to be used in place of a concrete EventArgs implementation.")]
    public interface IDataSourceChangeEventArgs
    {
        /// <summary>
        /// The type of the change.
        /// </summary>
        DataSourceChangeEventType EventType { get; }

        /// <summary>
        /// The <see cref="IDataInformation"/> associated with this change.
        /// </summary>
        /// <returns>
        /// An <see cref="IDataInformation"/> associated with this change.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1024:UsePropertiesWhereAppropriate",
            Justification = "We assume the underlying implementation could do work; thus, a property is not appropriate.")]
        IDataInformation GetImageInformation();
    }
}
