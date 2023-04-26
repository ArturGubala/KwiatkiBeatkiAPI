using System.Security.Claims;

namespace KwiatkiBeatkiAPI.Services
{
    public interface IItemsService
    {
        string SayHello();
    }
    public class ItemsService : IItemsService
    {
        private readonly IUserContextService _userContextService;
        public ItemsService(IUserContextService userContextService)
        {
            _userContextService = userContextService;
        }
        public string SayHello()
        {
            string name = _userContextService.User.FindFirst(c => c.Type == ClaimTypes.Name).Value;
            var welcomeMessage = $"Witaj {name}, miło Cię widzieć :)";

            return welcomeMessage;
        }
    }
}
