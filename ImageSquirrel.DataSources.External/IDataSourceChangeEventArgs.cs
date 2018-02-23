using ImageSquirrel.Formats.External;

namespace ImageSquirrel.DataSources.External
{
    public interface IDataSourceChangeEventArgs
    {
        DataSourceChangeEventType EventType { get; }

        IDataInformation GetImageInformation();
    }
}
