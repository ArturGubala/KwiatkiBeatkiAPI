namespace KwiatkiBeatkiAPI.Exeptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string code, string message) : base(code, message)
        {
        }
        public BadRequestException(string code, string message, Exception inner) : base(code, message, inner)
        {
        }
    }
}
