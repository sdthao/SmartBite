namespace SmartBite.Common
{
    public static class Result
    {
        public static Result<TValue> Successful<TValue>(TValue? value, string message = "")
        {
            return new Result<TValue>(value, true, message);
        }

        public static Result<TValue> Failure<TValue>(string message)
        {
            return new Result<TValue>(default, false, message);
        }

        public static IResult Successful(string message = "")
        {
            return new ResultObject(true, message);
        }

        public static IResult Failure(string message)
        {
            return new ResultObject(false, message);
        }
    }

    internal class ResultObject : IResult
    {
        public bool Success { get; }
        public string Message { get; }
        public ResultObject(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }

    public class Result<TValue> : IResult<TValue>
    {
        public TValue? Value { get; }

        public bool Success { get; }

        public string Message { get; }

        internal Result(TValue? value, bool success, string message)
        {
            Value = value;
            Success = success;
            Message = message;
        }
    }

}
