
using Serilog;

namespace Domain.Models
{
    public class OperationResult<T>
    {
        public bool IsSuccessful { get; private set; }

        public string Message { get; private set; }

        public T Data { get; private set; }

        public string ErrorMessage { get; private set; }

        private OperationResult(bool isSuccessful, T data, string message, string errorMessage)
        {
            IsSuccessful = isSuccessful;
            Message = message;
            Data = data;
            ErrorMessage = errorMessage;

            LogOperation();
        }

        public static OperationResult<T> Successful(T data, string message = "Operation successful.")
        {
            return new OperationResult<T>(true, data, message, null);
        }

        public static OperationResult<T> Failed(string errorMessage, string message = "Operation failed.")
        {
            return new OperationResult<T>(false, default, message, errorMessage);
        }

        private void LogOperation()
        {
            if (IsSuccessful)
            {
                Log.Information("OperationResult created successfully. Message: {Message}, Data: {Data}", Message, Data);
            }
            else
            {
                Log.Error("OperationResult creation failed. ErrorMessage: {ErrorMessage}, Message: {Message}", ErrorMessage, Message);
            }
        }
    }
}
