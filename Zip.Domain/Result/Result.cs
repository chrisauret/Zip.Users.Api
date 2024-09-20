namespace Zip.Domain.Result;
public class Result<T>
{
    public bool IsSuccessful { get; private set; }
    public T Value { get; private set; }
    public string Error { get; private set; }

    public Result(T value)
    {
        IsSuccessful = true;
        Value = value;
    }

    public Result(string error)
    {
        IsSuccessful = false;
        Error = error;
    }
}
