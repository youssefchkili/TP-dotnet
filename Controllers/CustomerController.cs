using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstApp.Models;

namespace MyFirstApp.Controllers;

public class CustomerController : Controller
{
    private readonly ApplicationdbContext _db;

    public CustomerController(ApplicationdbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        var customers = _db.customers.Include(c => c.MembershipType).ToList();
        return View(customers);
    }

    public IActionResult Details(int id)
    {
        var customer = _db.customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
        if (customer == null)
        {
            return NotFound();
        }
        return View(customer);
    }

    public IActionResult Create()
    {
        ViewBag.MembershipTypes = _db.membershipTypes.ToList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Customer customer)
    {
        if (ModelState.IsValid)
        {
            _db.customers.Add(customer);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Errors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();
        ViewBag.MembershipTypes = _db.membershipTypes.ToList();
        return View(customer);
    }

    public IActionResult Edit(int id)
    {
        var customer = _db.customers.SingleOrDefault(c => c.Id == id);
        if (customer == null)
        {
            return NotFound();
        }
        ViewBag.MembershipTypes = _db.membershipTypes.ToList();
        return View(customer);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Customer customer)
    {
        if (ModelState.IsValid)
        {
            _db.customers.Update(customer);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Errors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();
        ViewBag.MembershipTypes = _db.membershipTypes.ToList();
        return View(customer);
    }

    public IActionResult Delete(int id)
    {
        var customer = _db.customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
        if (customer == null)
        {
            return NotFound();
        }
        return View(customer);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var customer = _db.customers.SingleOrDefault(c => c.Id == id);
        if (customer != null)
        {
            _db.customers.Remove(customer);
            _db.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }
}
