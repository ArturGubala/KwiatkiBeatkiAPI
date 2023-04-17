using KwiatkiBeatkiAPI.DatabaseContext;

namespace KwiatkiBeatkiAPI.Services
{
    public interface ISeederService
    {
        void Seed();
    }
    public class SeederService : ISeederService
    {
        private readonly KwiatkiBeatkiDbContext _kwiatkiBeatkiDbContext;

        public SeederService(KwiatkiBeatkiDbContext kwiatkiBeatkiDbContext)
        {
            _kwiatkiBeatkiDbContext = kwiatkiBeatkiDbContext;
        }
        public void Seed()
        {
            if (!_kwiatkiBeatkiDbContext.Database.CanConnect())
                return;
        }
    }
}
