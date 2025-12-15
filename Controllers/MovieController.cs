using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstApp.Models;
using MyFirstApp.ViewModels;
using MyFirstApp.Repositories;

namespace MyFirstApp.Controllers;

public class MovieController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public MovieController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index(string sortOrder, int pageNumber = 1)
    {
        int pageSize = 5; // Nombre d'éléments par page
        
        ViewData["CurrentSort"] = sortOrder;
        ViewData["IdSortParam"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
        ViewData["NameSortParam"] = sortOrder == "name" ? "name_desc" : "name";
        
        var movies = (await _unitOfWork.Movies.GetAllWithGenreAsync()).AsQueryable();
        
        if (movies != null)
        {
            switch (sortOrder)
            {
                case "id_desc":
                    movies = movies.OrderByDescending(m => m.Id);
                    break;
                case "name":
                    movies = movies.OrderBy(m => m.Name);
                    break;
                case "name_desc":
                    movies = movies.OrderByDescending(m => m.Name);
                    break;
                default:
                    movies = movies.OrderBy(m => m.Id);
                    break;
            }
            
            int totalItems = movies.Count();
            ViewData["CurrentPage"] = pageNumber;
            ViewData["HasNextPage"] = (pageNumber * pageSize) < totalItems;
            
            var moviesList = movies.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return View(moviesList);
        }
        
        return View(new List<Movie>());
    }

    public async Task<IActionResult> Details(int id)
    {
        var movie = await _unitOfWork.Movies.GetByIdWithGenreAsync(id);
        if (movie == null)
        {
            return NotFound();
        }
        return View(movie); 
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Genres = (await _unitOfWork.Genres.GetAllAsync()).ToList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MovieVM model, IFormFile? photo)
    {
        if (ModelState.IsValid)
        {
            try
            {
                string? imageFileName = null;
                
                // Vérifier si un fichier a été téléchargé
                if (photo != null && photo.Length > 0)
                {
                    // Combine trois chaînes dans un seul path
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", photo.FileName);
                    
                    // Fournit un stream pour la lecture et écriture dans un fichier
                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        photo.CopyTo(stream);
                    }
                    imageFileName = photo.FileName;
                }
                
                // Mapping entre ViewModel et Model
                var movie = new Movie
                {
                    Name = model.movie.Name,
                    DateAjoutMovie = model.movie.DateAjoutMovie ?? DateTime.Now,
                    ImageFile = imageFileName,
                    GenreId = model.movie.GenreId,
                    Stock = model.movie.Stock
                };
                
                await _unitOfWork.Movies.AddAsync(movie);
                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erreur lors du téléchargement: {ex.Message}");
                ViewBag.Errors = new List<string> { $"Erreur lors du téléchargement: {ex.Message}" };
                ViewBag.Genres = (await _unitOfWork.Genres.GetAllAsync()).ToList();
                return View(model);
            }
        }
        
        // Récupérer toutes les erreurs de validation
        ViewBag.Errors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();
        ViewBag.Genres = (await _unitOfWork.Genres.GetAllAsync()).ToList();
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var movie = await _unitOfWork.Movies.GetByIdAsync(id);
        if (movie == null)
        {
            return NotFound();
        }
        ViewBag.Genres = (await _unitOfWork.Genres.GetAllAsync()).ToList();
        var model = new MovieVM { movie = movie };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(MovieVM model, IFormFile? photo)
    {
        if (ModelState.IsValid)
        {
            try
            {
                // Récupérer le film existant
                var existingMovie = await _unitOfWork.Movies.GetByIdAsync(model.movie.Id);
                if (existingMovie == null)
                {
                    return NotFound();
                }
                
                // Mettre à jour les propriétés
                existingMovie.Name = model.movie.Name;
                existingMovie.DateAjoutMovie = model.movie.DateAjoutMovie;
                existingMovie.GenreId = model.movie.GenreId;
                existingMovie.Stock = model.movie.Stock;
                
                // Vérifier si une nouvelle photo a été téléchargée
                if (photo != null && photo.Length > 0)
                {
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", photo.FileName);
                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        photo.CopyTo(stream);
                    }
                    existingMovie.ImageFile = photo.FileName;
                }
                // Si pas de nouvelle photo, on garde l'ancienne (déjà dans existingMovie.ImageFile)
                
                _unitOfWork.Movies.Update(existingMovie);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erreur lors de la modification: {ex.Message}");
                ViewBag.Errors = new List<string> { $"Erreur lors de la modification: {ex.Message}" };
                ViewBag.Genres = (await _unitOfWork.Genres.GetAllAsync()).ToList();
                return View(model);
            }
        }
        
        // Récupérer toutes les erreurs de validation
        ViewBag.Errors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();
        
        ViewBag.Genres = (await _unitOfWork.Genres.GetAllAsync()).ToList();
        return View(model);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var movie = await _unitOfWork.Movies.GetByIdAsync(id);
        if (movie == null)
        {
            return NotFound();
        }
        return View(movie);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var movie = await _unitOfWork.Movies.GetByIdAsync(id);
        if (movie != null)
        {
            _unitOfWork.Movies.Remove(movie);
            await _unitOfWork.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
