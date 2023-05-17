using GameShopApplication.Models;

namespace GameShopApplication.Data
{
    public interface IGamesRepository
    {
        Task<Game?> CreateGame(Game newGame);
        
        Task<List<Game>> GetAllGames();

        Task<Game?> GetGameById(int gameId);

        Task<Game?> UpdateGame(int gameId, Game updateGame);

        Task DeleteGame(int gameId);
    }
}
