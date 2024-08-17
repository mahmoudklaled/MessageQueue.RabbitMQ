namespace Common.Result
{
    public class TResult<T> 
    {
        public T result {  get; set; }

        public bool IsSuccess { get; set; }

        public string  ErrorMessage { get; set; }
    }
}
