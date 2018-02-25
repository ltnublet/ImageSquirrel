using System;
using System.IO;
using ImageSquirrel.Formats.External;

namespace ImageSquirrel.DataSources.ReferenceImplementation
{
    /// <summary>
    /// Reference implementation of <see cref="IDataInformation"/>.
    /// </summary>
    public class DataInformation : IDataInformation
    {
        private Func<Stream> streamCreator;
        private Func<DateTime> lastModified;

        /// <summary>
        /// Instantiates a new <see cref="DataInformation"/> instance.
        /// </summary>
        /// <param name="name">
        /// The name of the <see cref="DataInformation"/>.
        /// </param>
        /// <param name="created">
        /// The time the underlying data was created.
        /// </param>
        /// <param name="lastModified">
        /// A callback to access the time the underlying data was last modified.
        /// </param>
        /// <param name="streamCreator">
        /// A callback to access a <see cref="Stream"/> of the underlying data's content.
        /// </param>
        public DataInformation(
            string name,
            DateTime created,
            Func<DateTime> lastModified,
            Func<Stream> streamCreator)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Created = created;
            this.lastModified = lastModified ?? throw new ArgumentNullException(nameof(lastModified));
            this.streamCreator = streamCreator ?? throw new ArgumentNullException(nameof(streamCreator));
        }

        /// <inheritdoc />
        public DateTime Created { get; private set; }

        /// <inheritdoc />
        public DateTime LastModified => this.lastModified.Invoke();

        /// <inheritdoc />
        public string Name { get; private set; }

        /// <inheritdoc />
        public bool TryOpen(out Stream stream, out Exception failureReason)
        {
            try
            {
                stream = this.streamCreator.Invoke();
                failureReason = null;

                return true;
            }
            catch (Exception e)
            {
                stream = null;
                failureReason = e;

                return false;
            }
        }
    }
}
