using System;
using System.IO;
using ImageSquirrel.Formats.External;

namespace ImageSquirrel.DataSources.ReferenceImplementation
{
    public class DataInformation : IDataInformation
    {
        private Func<Stream> streamCreator;

        public DataInformation(
            string name,
            DateTime created,
            DateTime lastModified,
            Func<Stream> streamCreator)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Created = created;
            this.LastModified = lastModified;
            this.streamCreator = streamCreator ?? throw new ArgumentNullException(nameof(streamCreator));
        }

        public DateTime Created { get; private set; }

        public DateTime LastModified { get; private set; }

        public string Name { get; private set; }

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
