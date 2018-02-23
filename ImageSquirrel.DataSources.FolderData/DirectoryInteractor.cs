using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ImageSquirrel.DataSources.External;

namespace ImageSquirrel.DataSources.FolderData
{
    public class DirectoryInteractor : IDirectoryInteractor
    {
        public IEnumerable<FilePath> EnumerateFiles(FilePath path, SearchPattern pattern = null)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            IEnumerable<string> enumerationResult;
            if (pattern == null)
            {
                enumerationResult = Directory.EnumerateFiles(path.Path);
            }
            else if (pattern.Option.HasValue)
            {
                enumerationResult = Directory.EnumerateFiles(
                    path.Path,
                    pattern.Pattern,
                    (System.IO.SearchOption)(int)pattern.Option.Value);
            }
            else
            {
                enumerationResult = Directory.EnumerateFiles(path.Path, pattern.Pattern);
            }

            return enumerationResult.Select(x => new FilePath(x));
        }

        public IEnumerable<FilePath> EnumerateDirectories(FilePath path, SearchPattern pattern = null)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            IEnumerable<string> enumerationResult;
            if (pattern == null)
            {
                enumerationResult = Directory.EnumerateDirectories(path.Path);
            }
            else if (pattern.Option.HasValue)
            {
                enumerationResult = Directory.EnumerateDirectories(
                    path.Path,
                    pattern.Pattern,
                    (System.IO.SearchOption)(int)pattern.Option.Value);
            }
            else
            {
                enumerationResult = Directory.EnumerateDirectories(path.Path, pattern.Pattern);
            }

            return enumerationResult.Select(x => new FilePath(x));
        }
    }
}
