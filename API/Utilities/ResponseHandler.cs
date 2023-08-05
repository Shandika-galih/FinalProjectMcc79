using MimeKit.Cryptography;

namespace API.Utilities;

public class ResponseHandler<TEntity>
{
    public int Code { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
    public TEntity? Data { get; set; }
    public Errors Errors { get; set; }
}

public class Errors
{
    public string[] Email {  get; set; }
    public string[] PhoneNumber { get; set; }
}