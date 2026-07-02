namespace backend_api.Common
{
    public class ServiceResult<T>
    {
        public bool Success { get; private set; }
        
        public T? Data { get; private set; }
        public string? Error { get; private set; }
        public string? ErrorCode { get; private set; }

        public static ServiceResult<T> Ok(T data) =>
            new() { Success = true, Data = data };

        public static ServiceResult<T> Fail(string error, string errorCode = "") =>
            new() { Success = false, Error = error, ErrorCode = errorCode };
    }
}