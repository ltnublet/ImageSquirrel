using ImageSquirrel.DataSources.External;
using ImageSquirrel.Formats.External;

namespace ImageSquirrel.DataSources.FolderData
{
    public interface IFolderDataChangeEventArgs : IDataSourceChangeEventArgs
    {
        DataSourceChangeEventType EventType { get; }

        FilePath Path { get; }

        IDataInformation GetImageInformation();
    }
}
