using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyFirstApp.Models;

namespace MyFirstApp.Controllers;

public class GenreController : Controller
{
    private readonly ApplicationdbContext _db;

    public GenreController(ApplicationdbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        var genres = _db.genres.ToList();
        return View(genres);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Genre genre)
    {
        if (ModelState.IsValid)
        {
            _db.genres.Add(genre);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Errors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();
        return View(genre);
    }
}
