using ImageSquirrel.DataSources.External;

namespace ImageSquirrel.DataSources.FolderData
{
    /// <summary>
    /// Represents a change in an <see cref="FolderDataSource"/>.
    /// </summary>
    public interface IFolderDataChangeEventArgs : IDataSourceChangeEventArgs
    {
        /// <summary>
        /// The local file path associated with this change.
        /// </summary>
        FilePath Path { get; }
    }
}
