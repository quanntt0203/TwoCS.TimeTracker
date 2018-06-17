namespace TwoCS.TimeTracker.Core.Results
{
    public class ApiResultOk
    {
        public ApiResultOk()
        {

        }

        public ApiResultOk(string message) : this(message, null)
        {
            Message = message;
        }

        public ApiResultOk(object data) : this (null, data)
        {
        }

        public ApiResultOk(string message, object data)
        {
            Message = message ?? "Ok";
            Result = data;
        }

        public string Message { get; set; } = "Ok";

        public object Result { get; set; }
    }
}
