using Microsoft.AspNetCore.Mvc;
using MyFirstApp.Models;

namespace MyFirstApp.Controllers;

public class MembershipTypeController : Controller
{
    private readonly ApplicationdbContext _db;

    public MembershipTypeController(ApplicationdbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        var membershipTypes = _db.membershipTypes.ToList();
        return View(membershipTypes);
    }

    public IActionResult Details(int id)
    {
        var membershipType = _db.membershipTypes.SingleOrDefault(m => m.Id == id);
        if (membershipType == null)
        {
            return NotFound();
        }
        return View(membershipType);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(MembershipType membershipType)
    {
        if (ModelState.IsValid)
        {
            _db.membershipTypes.Add(membershipType);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Errors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();
        return View(membershipType);
    }

    public IActionResult Edit(int id)
    {
        var membershipType = _db.membershipTypes.SingleOrDefault(m => m.Id == id);
        if (membershipType == null)
        {
            return NotFound();
        }
        return View(membershipType);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(MembershipType membershipType)
    {
        if (ModelState.IsValid)
        {
            _db.membershipTypes.Update(membershipType);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Errors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();
        return View(membershipType);
    }

    public IActionResult Delete(int id)
    {
        var membershipType = _db.membershipTypes.SingleOrDefault(m => m.Id == id);
        if (membershipType == null)
        {
            return NotFound();
        }
        return View(membershipType);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var membershipType = _db.membershipTypes.SingleOrDefault(m => m.Id == id);
        if (membershipType != null)
        {
            _db.membershipTypes.Remove(membershipType);
            _db.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }
}
