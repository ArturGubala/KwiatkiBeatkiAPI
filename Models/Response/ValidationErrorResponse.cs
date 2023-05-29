namespace KwiatkiBeatkiAPI.Models.Response
{
    public class ValidationErrorResponse
    {
        public string Message { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
    }
}
