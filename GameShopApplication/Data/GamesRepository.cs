using GameShopApplication.Models;
using Newtonsoft.Json;
using System.Text;

namespace GameShopApplication.Data
{
    public class GamesRepository : IGamesRepository
    {
        private readonly HttpClient _httpClient;

        //paste the generated jwt token here to work the web app CRUD
        string jwtToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE2ODQ3NDEwNDcsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTI2NSIsImF1ZCI6IlVzZXIifQ.lhf6gP6PVcOzbA1F4UoDk2KHoqk4pwNcFzIh6BhfM20";

        public GamesRepository()
        {
            _httpClient = new HttpClient();

            _httpClient.BaseAddress = new Uri("http://localhost:5265");

        }

        public async Task<Game?> CreateGame(Game newGame)
        {
            var newGameAsString = JsonConvert.SerializeObject(newGame);
            var requestBody = new StringContent(newGameAsString, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Add("ApiKey", "ARandomApiKeyForGameShop");
            _httpClient.DefaultRequestHeaders.Add("Authorization","Bearer " + jwtToken);
            var response = await _httpClient.PostAsync("/games", requestBody);
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
            _httpClient.DefaultRequestHeaders.Add("ApiKey", "ARandomApiKeyForGameShop");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
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
            _httpClient.DefaultRequestHeaders.Add("ApiKey", "ARandomApiKeyForGameShop");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
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
            _httpClient.DefaultRequestHeaders.Add("ApiKey", "ARandomApiKeyForGameShop");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
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
             _httpClient.DefaultRequestHeaders.Add("ApiKey", "ARandomApiKeyForGameShop");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
            var response = await _httpClient.DeleteAsync($"/games/{gameId}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsByteArrayAsync();
                Console.WriteLine("Delete Game Response: ", data);
            }
        }

    }
}
