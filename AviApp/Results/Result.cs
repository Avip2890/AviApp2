namespace AviApp.Results;

public class Result<T>
{
    public bool IsSuccess { get; }
    public string Error { get; }
    public T Value { get; }

    private Result(T value, bool isSuccess, string error)
    {
        IsSuccess = isSuccess;
        Error = error;
        Value = value;
    }

    // הצלחה
    public static Result<T> Success(T value) => new Result<T>(value, true, null);

    // שגיאה
    public static Result<T> Failure(string error) => new Result<T>(default, false, error);
}