namespace Seal.Models.DTOs
{
    public class ResponseResult<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
    }
}
