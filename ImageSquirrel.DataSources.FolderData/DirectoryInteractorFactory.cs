using System;

namespace ImageSquirrel.DataSources.FolderData
{
    public class DirectoryInteractorFactory : IDirectoryInteractorFactory
    {
        public IDirectoryInteractor MakeInteractor()
        {
            return new DirectoryInteractor();
        }
    }
}
