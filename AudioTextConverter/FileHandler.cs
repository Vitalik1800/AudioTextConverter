using System.IO;

public class FileHandler
{
    public Stream OpenFile(string filePath)
    {
        return File.OpenRead(filePath);
    }

    public void CloseFile(Stream fileStream)
    {
        fileStream.Close();
    }
}
