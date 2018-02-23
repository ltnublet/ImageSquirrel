using System;
using System.IO;
using System.Reactive.Linq;
using ImageSquirrel.DataSources.External;

namespace ImageSquirrel.DataSources.FolderData
{
    public class FolderDataWatcher : IFolderDataWatcher
    {
        private FileSystemWatcher fileSystemWatcherBackingField;
        private string filterBackingField;
        private FilePath pathBackingField;

        private bool disposed;
        private object disposalLock;

        public FolderDataWatcher(FilePath path, string filter = null, long changeFilterTicks = 0L)
        {
            this.Path = path;
            this.Filter = filter;
            this.FileSystemWatcher =
                filter == null
                    ? new FileSystemWatcher(this.Path.Path)
                    : new FileSystemWatcher(this.Path.Path, this.Filter);

            this.disposed = false;
            this.disposalLock = new object();

            // Known issue - changed event fires multiple times for some applications, because they "write"
            // multiple times (ex. update file contents, then as a separate step update metadata).
            if (changeFilterTicks > 0L)
            {
                Observable
                    .FromEventPattern<FileSystemEventArgs>(
                        this.FileSystemWatcher,
                        nameof(this.FileSystemWatcher.Changed))
                    .Throttle(new TimeSpan(changeFilterTicks))
                    .Subscribe(e => this.Changed.Invoke(e.Sender, new FolderDataChangeEventArgs(e.EventArgs)));
            }
            else
            {
                this.FileSystemWatcher.Changed +=
                    (obj, e) => this.Changed.Invoke(obj, new FolderDataChangeEventArgs(e));
            }

            this.FileSystemWatcher.Created +=
                (obj, e) => this.Added.Invoke(obj, new FolderDataChangeEventArgs(e));
            this.FileSystemWatcher.Deleted +=
                (obj, e) => this.Removed.Invoke(obj, new FolderDataChangeEventArgs(e));
            this.FileSystemWatcher.Renamed +=
                (obj, e) => this.Changed.Invoke(obj, new FolderDataChangeEventArgs(e));
        }

        public virtual bool EnableRaisingEvents
        {
            get
            {
                return this.FileSystemWatcher.EnableRaisingEvents;
            }
            set
            {
                this.FileSystemWatcher.EnableRaisingEvents = value;
            }
        }

        protected virtual FileSystemWatcher FileSystemWatcher
        {
            get
            {
                return this.fileSystemWatcherBackingField;
            }
            set
            {
                this.fileSystemWatcherBackingField = value;
            }
        }

        protected virtual string Filter
        {
            get
            {
                return this.filterBackingField;
            }
            set
            {
                this.filterBackingField = value;
            }
        }

        protected virtual FilePath Path
        {
            get
            {
                return this.pathBackingField;
            }
            set
            {
                this.pathBackingField = value;
            }
        }

        public event EventHandler<IFolderDataChangeEventArgs> Added;
        public event EventHandler<IFolderDataChangeEventArgs> Changed;
        public event EventHandler<IFolderDataChangeEventArgs> Removed;

        public void Dispose()
        {
            lock (this.disposalLock)
            {
                if (!this.disposed)
                {
                    this.disposed = true;

                    this.Added = null;
                    this.Changed = null;
                    this.Removed = null;

                    this.FileSystemWatcher.Dispose();
                }
            }
        }
    }
}
