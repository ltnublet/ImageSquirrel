using System;
using ImageSquirrel.DataSources.External;

namespace ImageSquirrel.DataSources.FolderData.Tests.Mocks
{
    internal class MockFolderDataWatcherFactory : IFolderDataWatcherFactory
    {
        private Func<FilePath, IFolderDataWatcher> makeFunc;

        public MockFolderDataWatcherFactory(Func<FilePath, IFolderDataWatcher> makeFunc)
        {
            this.makeFunc = makeFunc;
        }

        public IFolderDataWatcher MakeFolderDataWatcher(FilePath path)
        {
            return this.makeFunc(path);
        }
    }
}
