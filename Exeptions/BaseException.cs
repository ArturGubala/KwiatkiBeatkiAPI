namespace KwiatkiBeatkiAPI.Exeptions
{
    public class BaseException : Exception
    {
        public string Code { get; }
        public BaseException(string code, string message) : base(message)
        {
            Code = code;
        }
        public BaseException(string code, string message, Exception inner) : base(message, inner)
        {
            Code = code;
        }
    }
}
