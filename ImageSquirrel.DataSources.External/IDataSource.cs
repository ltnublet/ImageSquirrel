using System;
using System.Collections.Generic;
using ImageSquirrel.Formats.External;

namespace ImageSquirrel.DataSources.External
{
    public interface IDataSource : IEnumerable<IDataInformation>, IDisposable
    {
        event EventHandler<IDataSourceChangeEventArgs> OnChange;

        int Count { get; }

        string Name { get; }

        IReadOnlyList<IDataSource> GetSubDataSources();
    }
}
