using System.Collections.Generic;
using ImageSquirrel.DataSources.External;

namespace ImageSquirrel.DataSources.FolderData
{
    public interface IDirectoryInteractor
    {
        IEnumerable<FilePath> EnumerateFiles(FilePath path, SearchPattern pattern = null);

        IEnumerable<FilePath> EnumerateDirectories(FilePath path, SearchPattern pattern = null);
    }
}
