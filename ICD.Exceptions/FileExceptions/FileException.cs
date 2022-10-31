using System;

namespace ICD.Exceptions.FileExceptions;

public class FileException : Exception
{
    public FileException(string message) : base(message) { }
}
