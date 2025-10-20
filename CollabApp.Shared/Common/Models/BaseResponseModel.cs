namespace CollabApp.Application.Common.Models
{
    internal class BaseResponseModel
    {
        public bool Status { get; private set; }
        public string? Message { get; private set; }
        public string[] Errors { get; private set; }
        public BaseResponseModel(string? message = "", bool status = true, string[]? errors = null)
        {
            Message = message;
            Status = status;
            Errors = errors ?? Array.Empty<string>();
        }
    }
}
