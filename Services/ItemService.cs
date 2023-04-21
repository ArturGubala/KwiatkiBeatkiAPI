using System.Security.Claims;

namespace KwiatkiBeatkiAPI.Services
{
    public interface IItemService
    {
        string SayHello();
    }
    public class ItemService : IItemService
    {
        private readonly IUserContextService _userContextService;
        public ItemService(IUserContextService userContextService)
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
