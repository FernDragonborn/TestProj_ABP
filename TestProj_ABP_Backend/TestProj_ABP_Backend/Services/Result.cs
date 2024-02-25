namespace TestProj_ABP_Backend.Services;
public class Result<T>
{
    public Result() { }
    public Result(bool isSuccess, T data, string message)
    {
        Data = data;
        IsSuccess = isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; set; }
    public T Data { get; set; }
    public string Message { get; set; }
}

public class Result
{
    public Result() { }

    public Result(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}
