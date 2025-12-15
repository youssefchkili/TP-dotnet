using Microsoft.AspNetCore.Mvc;
using MyFirstApp.Services;

namespace MyFirstApp.Controllers
{
    public class LinqDemoController : Controller
    {
        private readonly MovieService _movieService;

        public LinqDemoController(MovieService movieService)
        {
            _movieService = movieService;
        }

        // 1. Films Action en stock
        public async Task<IActionResult> ActionMoviesInStock()
        {
            var movies = await _movieService.GetActionMoviesInStock();
            return View(movies);
        }

        // 2. Films ordonnés par date et titre
        public async Task<IActionResult> MoviesOrderedByDateAndTitle()
        {
            var movies = await _movieService.GetMoviesOrderedByDateAndTitle();
            return View(movies);
        }

        // 3. Nombre total de films
        public async Task<IActionResult> TotalMoviesCount()
        {
            var count = await _movieService.GetTotalMoviesCount();
            ViewBag.TotalCount = count;
            return View();
        }

        // 4. Clients abonnés avec remise > 10%
        public async Task<IActionResult> SubscribedCustomersWithHighDiscount()
        {
            var customers = await _movieService.GetSubscribedCustomersWithHighDiscount();
            return View(customers);
        }

        // 5. Films avec leurs genres
        public async Task<IActionResult> MoviesWithGenres()
        {
            var moviesWithGenres = await _movieService.GetMoviesWithGenres();
            return View(moviesWithGenres);
        }

        // 6. Top 3 genres populaires
        public async Task<IActionResult> Top3PopularGenres()
        {
            var genres = await _movieService.GetTop3PopularGenres();
            return View(genres);
        }

        // Page d'index pour lister toutes les démos
        public IActionResult Index()
        {
            return View();
        }
    }
}
