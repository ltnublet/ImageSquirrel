using ImageSquirrel.DataSources.External;

namespace ImageSquirrel.DataSources.FolderData
{
    public interface IFolderDataWatcherFactory
    {
        IFolderDataWatcher MakeFolderDataWatcher(FilePath path);
    }
}
