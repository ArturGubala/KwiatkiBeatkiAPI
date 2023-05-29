namespace KwiatkiBeatkiAPI.Exeptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string code, string message) : base(code, message)
        {
        }
        public NotFoundException(string code, string message, Exception inner) : base(code, message, inner)
        {
        }
    }
}
