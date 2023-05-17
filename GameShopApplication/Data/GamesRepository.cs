using GameShopApplication.Models;
using Newtonsoft.Json;
using System.Text;

namespace GameShopApplication.Data
{
    public class GamesRepository : IGamesRepository
    {
        private readonly HttpClient _httpClient;

        public GamesRepository()
        {
            _httpClient = new HttpClient();

            _httpClient.BaseAddress = new Uri("http://localhost:5265");
        }

        public async Task<Game?> CreateGame(Game newGame)
        {
            var newGameAsString = JsonConvert.SerializeObject(newGame);
            var responseBody = new StringContent(newGameAsString, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/games", responseBody);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var game = JsonConvert.DeserializeObject<Game>(data);
                return game;
            }
            return null;
        }

        public async Task<List<Game>> GetAllGames()
        {
            var response = await _httpClient.GetAsync("/games");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var games = JsonConvert.DeserializeObject<List<Game>>(content);
                return games ?? new();
            }

            return new();
        }

        public async Task<Game?> GetGameById(int gameId)
        {
            var response = await _httpClient.GetAsync($"/games/{gameId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var game = JsonConvert.DeserializeObject<Game>(content);
                return game;
            }

            return null;
        }



        public async Task<Game?> UpdateGame(int gameId, Game updatedGame)
        {
            var newTodoAsString = JsonConvert.SerializeObject(updatedGame);
            var responseBody = new StringContent(newTodoAsString, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/games/{gameId}", responseBody);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var game = JsonConvert.DeserializeObject<Game>(content);
                return game;
            }

            return null;
        }






        public async Task DeleteGame(int gameId)
        {
            var response = await _httpClient.DeleteAsync($"/games/{gameId}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsByteArrayAsync();
                Console.WriteLine("Delete Game Response: ", data);
            }
        }

    }
}
