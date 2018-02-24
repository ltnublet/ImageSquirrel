﻿using System;

namespace ImageSquirrel.DataSources.FolderData
{
    /// <summary>
    /// Represents an <see cref="IDirectoryInteractorFactory"/> implementation which produces
    /// <see cref="DirectoryInteractor"/>s.
    /// </summary>
    public class DirectoryInteractorFactory : IDirectoryInteractorFactory
    {
        /// <inheritdoc />
        public IDirectoryInteractor MakeInteractor()
        {
            return new DirectoryInteractor();
        }
    }
}
