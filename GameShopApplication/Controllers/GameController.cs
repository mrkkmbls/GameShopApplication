using GameShopApplication.Data;
using GameShopApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameShopApplication.Controllers;

public class GameController : Controller
{
    private readonly IGamesRepository _gamesRepository;

    public GameController(IGamesRepository gamesRepository)
    {
        _gamesRepository = gamesRepository;
    }

    public async Task<IActionResult> Index()
    {
        var viewModel = new IndexViewModel
        {
            Games = await _gamesRepository.GetAllGames()
        };

        return View(viewModel);
    }

    public IActionResult Create() 
    {
        return View();    
    }

    [HttpPost]
    public async Task<IActionResult> Create(Game newGame)
    {
        await _gamesRepository.CreateGame(newGame);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int gameId)
    {
        var game = await _gamesRepository.GetGameById(gameId);

        if (game is null)
            return NotFound();

        return View(game);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Game updatedGame)
    {
        var game = await _gamesRepository.UpdateGame(updatedGame.Id, updatedGame);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int gameId)
    {
        await _gamesRepository.DeleteGame(gameId);
        return RedirectToAction("Index");
    }
}
