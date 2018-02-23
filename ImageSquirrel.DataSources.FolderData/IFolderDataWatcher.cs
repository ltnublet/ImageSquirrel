using System;

namespace ImageSquirrel.DataSources.FolderData
{
    public interface IFolderDataWatcher : IDisposable
    {
        event EventHandler<IFolderDataChangeEventArgs> Added;
        event EventHandler<IFolderDataChangeEventArgs> Changed;
        event EventHandler<IFolderDataChangeEventArgs> Removed;

        bool EnableRaisingEvents { get; set; }
    }
}
