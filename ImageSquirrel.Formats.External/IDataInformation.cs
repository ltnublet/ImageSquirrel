using System;
using System.IO;

namespace ImageSquirrel.Formats.External
{
    public interface IDataInformation
    {
        DateTime Created { get; }

        DateTime LastModified { get; }

        string Name { get; }

        bool TryOpen(out Stream stream, out Exception failureReason);
    }
}
