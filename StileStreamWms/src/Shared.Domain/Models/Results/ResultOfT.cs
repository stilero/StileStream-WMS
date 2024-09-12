namespace Shared.Domain.Models.Results;

public class Result<T> : Result
{
    private readonly T? _value;

    protected internal Result(T value)
        : base()
    {
        _value = value;
    }

    protected internal Result(ErrorResult error)
        : base(error)
    {
        ArgumentNullException.ThrowIfNull(error);

        _value = default;
    }

    public T Value => IsSuccess ? _value! : throw new InvalidOperationException();

    public static implicit operator Result<T>(ErrorResult error) => new(error);

    public static implicit operator Result<T>(T value) => new(value);
}