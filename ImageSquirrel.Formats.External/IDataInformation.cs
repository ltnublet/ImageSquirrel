using System;
using System.IO;

namespace ImageSquirrel.Formats.External
{
    /// <summary>
    /// Represents information about data contained within a data source.
    /// </summary>
    public interface IDataInformation
    {
        /// <summary>
        /// The time the underlying data was created.
        /// </summary>
        DateTime Created { get; }

        /// <summary>
        /// The time the underlying data was last modified.
        /// </summary>
        DateTime LastModified { get; }

        /// <summary>
        /// The name of the underlying data.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Tries to create a <see cref="Stream"/> by which to access the underlying data's content.
        /// </summary>
        /// <param name="stream">
        /// The resulting <see cref="Stream"/>.
        /// </param>
        /// <param name="failureReason">
        /// An <see cref="Exception"/> describing the reason the <see cref="Stream"/> could not be created, if any.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="stream"/> was successfully created; <see langword="false"/>
        /// otherwise.
        /// </returns>
        bool TryOpen(out Stream stream, out Exception failureReason);
    }
}
