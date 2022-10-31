namespace ICD.Exceptions.FileExceptions;

public class FileSizeException : FileException
{
    public FileSizeException(string message) : base(message) { }
}
