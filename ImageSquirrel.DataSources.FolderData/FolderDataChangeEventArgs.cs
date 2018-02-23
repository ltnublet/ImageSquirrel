using System;
using System.IO;
using ImageSquirrel.DataSources.External;
using ImageSquirrel.DataSources.ReferenceImplementation;
using ImageSquirrel.Formats.External;

namespace ImageSquirrel.DataSources.FolderData
{
    public class FolderDataChangeEventArgs : IFolderDataChangeEventArgs
    {
        private string name;

        public FolderDataChangeEventArgs(FileSystemEventArgs e)
        {
            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Created:
                    this.EventType = DataSourceChangeEventType.Added;
                    break;
                case WatcherChangeTypes.Changed:
                case WatcherChangeTypes.Renamed:
                    this.EventType = DataSourceChangeEventType.Changed;
                    break;
                case WatcherChangeTypes.Deleted:
                    this.EventType = DataSourceChangeEventType.Removed;
                    break;
                default:
                    throw new NotImplementedException();
            }

            this.name = e.Name;
            this.Path = new FilePath(e.FullPath);
        }

        public DataSourceChangeEventType EventType { get; private set; }

        public FilePath Path { get; private set; }

        public IDataInformation GetImageInformation()
        {
            FileInfo info = new FileInfo(this.Path.Path);
            return new DataInformation(
                this.name,
                info.CreationTimeUtc,
                info.LastWriteTimeUtc,
                () => info.OpenRead());
        }

        IDataInformation IDataSourceChangeEventArgs.GetImageInformation()
        {
            return this.GetImageInformation();
        }
    }
}
