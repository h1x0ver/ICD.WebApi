using Microsoft.AspNetCore.Http;

namespace ICD.Business.Helper;

public static class Helper
{
    public static bool CheckFileType(this IFormFile file, string fileType)
    {
        if (file.ContentType.Contains(fileType))
        {
            return true;
        }
        return false;
    }
    public static bool CheckFileSize(this IFormFile file, int fileSize)
    {
        if (fileSize > file.Length / 1024)
        {
            return true;
        }
        return false;
    }
    private static string FileGuidName(this IFormFile file)
    {
        string path = Path.Combine(Guid.NewGuid().ToString() + file.FileName);
        return path;
    }
    public static async Task<string> FileSaveAsync(this IFormFile file, string root, params string[] folders)
    {
        string fileNewName = FileGuidName(file);
        string rootInFolder = folders.Aggregate((result, folder) => Path.Combine(result, folder));
        string path = Path.Combine(root, rootInFolder, fileNewName);
        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(fs);
        }
        return fileNewName;
    }

}

