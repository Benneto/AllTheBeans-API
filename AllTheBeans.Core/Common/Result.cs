namespace AllTheBeans.Core.Common
{
    public enum ResultStatus
    {
        Success,
        Failure,
        Error
    }

    public class Result<T>
    {
        public T? Value { get; set; }
        public ResultStatus Status { get; set; }
        public string? ErrorMessage { get; set; }

        public static Result<T> Success() =>
        new Result<T> { Status = ResultStatus.Success };

        public static Result<T> Success(T value) =>
            new Result<T> { Status = ResultStatus.Success, Value = value };

        public static Result<T> Failure(string errorMessage) =>
            new Result<T> { Status = ResultStatus.Failure, ErrorMessage = errorMessage };

        public static Result<T> Error(string errorMessage) =>
            new Result<T> { Status = ResultStatus.Error, ErrorMessage = errorMessage };
    }
}
