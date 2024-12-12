namespace Domain.Models
{
    public class OperationResult<T>
    {
        public bool Success { get; private set; }

        public string Message { get; private set; }

        public T Data { get; private set; }

        public string ErrorMessage { get; private set; }

        private OperationResult(bool success, T data, string message, string errorMessage)
        {
            Success = success;
            Message = message;
            Data = data;
            ErrorMessage = errorMessage;
        }

        public static OperationResult<T> Successful(T data, string message = "Operation successful.")
        {
            return new OperationResult<T>(true, data, message, null);
        }

        public static OperationResult<T> Failed(string errorMessage, string message = "Operation failed.")
        {
            return new OperationResult<T>(false, default, message, errorMessage);
        }
    }
}
