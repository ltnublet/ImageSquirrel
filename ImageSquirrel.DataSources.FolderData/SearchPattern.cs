using System;

namespace ImageSquirrel.DataSources.FolderData
{
    public class SearchPattern
    {
        public SearchPattern(string pattern, SearchOption? option = null)
        {
            this.Pattern = pattern ?? throw new ArgumentNullException(nameof(pattern));
            this.Option = option;
        }

        public string Pattern { get; set; }
        public SearchOption? Option { get; set; }
    }
}
