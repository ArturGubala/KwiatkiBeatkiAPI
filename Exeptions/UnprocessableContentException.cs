namespace KwiatkiBeatkiAPI.Exeptions
{
    public class UnprocessableContentException : BaseException
    {
        public UnprocessableContentException(string code, string message) : base(code, message)
        {
        }
        public UnprocessableContentException(string code, string message, Exception inner) : base(code, message, inner)
        {
        }
    }
}
