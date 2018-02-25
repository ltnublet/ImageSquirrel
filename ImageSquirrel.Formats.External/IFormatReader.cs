namespace ImageSquirrel.Formats.External
{
    /// <summary>
    /// Represents a reader which can create an <see cref="IImage"/> from an <see cref="IDataInformation"/>.
    /// </summary>
    public interface IFormatReader
    {
        /// <summary>
        /// Creates an <see cref="IImage"/> from the supplied <see cref="IDataInformation"/>.
        /// </summary>
        /// <param name="data">
        /// The <see cref="IDataInformation"/> from which to create the <see cref="IImage"/>.
        /// </param>
        /// <returns>
        /// An <see cref="IImage"/> created from the supplied <paramref name="data"/>.
        /// </returns>
        IImage Read(IDataInformation data);
    }
}
