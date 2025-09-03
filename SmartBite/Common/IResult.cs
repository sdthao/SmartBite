namespace SmartBite.Common
{
    /// <summary>
    /// Represents the result of an operation, including its success status and an associated message.
    /// </summary>
    /// <remarks>This interface is commonly used to standardize the representation of outcomes for operations,
    /// providing a boolean success indicator and an optional descriptive message.</remarks>
    public interface IResult
    {
        /// <summary>
        /// The result sucess status.
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// The result optional message.
        /// </summary>
        string Message { get; }
    }

    /// <summary>
    /// Represents the result of an operation that returns a value, including its success status and an associated message.
    /// </summary>
    /// <typeparam name="TValue">IF successful, the result value.</typeparam>
    public interface IResult<TValue>
    {
        /// <summary>
        /// Tje result value.
        /// </summary>
        TValue Value { get; }

        /// <summary>
        /// The result sucess status.
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// The result optional message.
        /// </summary>
        string Message { get; }
    }
}
