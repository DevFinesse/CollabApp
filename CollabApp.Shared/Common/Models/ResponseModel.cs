namespace CollabApp.Application.Common.Models
{
    internal class ResponseModel<T> : BaseResponseModel
    {
        public ResponseModel(string? message, T? data, bool status = true, string[]? errors = null) : base(message, status, errors)
        {
            Data = data;
        }

        public T? Data { get; private set; }
    }
}
