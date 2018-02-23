using ImageSquirrel.DataSources.External;

namespace ImageSquirrel.DataSources.FolderData
{
    public class FolderDataWatcherFactory : IFolderDataWatcherFactory
    {
        public FolderDataWatcherFactory(string filter = null, long changeFilterTicks = 0L)
        {
            this.Filter = filter;
            this.ChangeFilterTicks = changeFilterTicks;
        }

        public string Filter { get; private set; }
        public long ChangeFilterTicks { get; private set; }

        public IFolderDataWatcher MakeFolderDataWatcher(FilePath path)
        {
            return new FolderDataWatcher(path, this.Filter, this.ChangeFilterTicks);
        }
    }
}
