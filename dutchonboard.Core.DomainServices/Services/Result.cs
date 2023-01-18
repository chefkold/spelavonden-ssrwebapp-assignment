namespace dutchonboard.Core.DomainServices.Services;

// A result class to return a result of a business rule check and wrap the result in a single object or store the error with message
#nullable disable
public class Result<T>
{
    private readonly T _value;
    private readonly string _errorMessage;

    public Result(T value)
    {
        _value = value;
        HasError = false;
    }

    public Result(string errorMessage)
    {
        _errorMessage = errorMessage;
        HasError = true;
    }

    public T Value
    {
        get
        {
            if (HasError)
                throw new InvalidOperationException("There was an error, get the error message instead");
            return _value;
        }
    }

    public string ErrorMessage
    {
        get
        {
            if (!HasError)
                throw new InvalidOperationException("There was no error, get the value instead");
            return _errorMessage;
        }
    }

    public bool HasError { get; }
}