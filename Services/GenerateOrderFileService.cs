using KwiatkiBeatkiAPI.Models.Document;

namespace KwiatkiBeatkiAPI.Services
{
    public interface IGenerateFile<TDto>
    {
        Task<byte[]> GenerateFile(TDto dto);
    }
    public class GenerateOrderFileService : IGenerateFile<GenerateDocumentDto>
    {
        public Task<byte[]> GenerateFile(GenerateDocumentDto generateDocumentDto)
        {
            throw new NotImplementedException();
        }
    }
}
