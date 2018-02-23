using ImageSquirrel.DataSources.External;
using ImageSquirrel.Formats.External;

namespace ImageSquirrel.DataSources.FolderData.Tests.Mocks
{
    internal class MockFolderDataChangeEventArgs : IFolderDataChangeEventArgs
    {
        private IDataInformation information;

        public MockFolderDataChangeEventArgs(
            FilePath path,
            DataSourceChangeEventType type,
            IDataInformation information)
        {
            this.Path = path;
            this.EventType = type;
            this.information = information;
        }

        public DataSourceChangeEventType EventType { get; private set; }

        public FilePath Path { get; private set; }

        public IDataInformation GetImageInformation() => this.information;
    }
}
