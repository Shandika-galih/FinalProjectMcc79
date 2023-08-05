namespace Client.Utilities;

public static class UploadFile
{
    public static string ConvertToBase64(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return null;

        using (var memoryStream = new MemoryStream())
        {
            file.CopyTo(memoryStream);
            byte[] fileBytes = memoryStream.ToArray();
            return Convert.ToBase64String(fileBytes);
        }
    }
}
