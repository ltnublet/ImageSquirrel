﻿using System.Collections.Generic;
using ImageSquirrel.DataSources.External;

namespace ImageSquirrel.DataSources.FolderData.Tests.Mocks
{
    internal class MockDirectoryInteractor : IDirectoryInteractor
    {
        private IEnumerable<FilePath> directories;
        private IEnumerable<FilePath> files;

        public MockDirectoryInteractor(IEnumerable<FilePath> files, IEnumerable<FilePath> directories)
        {
            this.directories = directories;
            this.files = files;
        }

        public IEnumerable<FilePath> EnumerateDirectories(FilePath path, SearchPattern pattern = null)
        {
            return this.directories;
        }

        public IEnumerable<FilePath> EnumerateFiles(FilePath path, SearchPattern pattern = null)
        {
            return this.files;
        }
    }
}
