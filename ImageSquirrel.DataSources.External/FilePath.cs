namespace ImageSquirrel.DataSources.External
{
    public class FilePath
    {
        public FilePath(string path)
        {
            this.Path = path;
        }

        public string Path { get; private set; }

        public override string ToString()
        {
            return this.Path;
        }
    }
}
