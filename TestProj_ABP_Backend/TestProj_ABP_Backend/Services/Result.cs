namespace TestProj_ABP_Backend.Services;
public class Result<T>
{
    public Result(T data, bool success, string message)
    {
        Data = data;
        IsSuccess = success;
        Message = message;
    }

    public string Message { get; set; }
    public T Data { get; set; }
    public bool IsSuccess { get; set; }
}

