namespace ImageSquirrel.Formats.External
{
    public interface IFormatReader
    {
        IImage Read(IDataInformation data);
    }
}
