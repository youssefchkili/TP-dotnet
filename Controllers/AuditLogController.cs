using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstApp.Models;

namespace MyFirstApp.Controllers
{
    public class AuditLogController : Controller
    {
        private readonly ApplicationdbContext _db;

        public AuditLogController(ApplicationdbContext db)
        {
            _db = db;
        }

        // Afficher tous les logs d'audit
        public IActionResult Index()
        {
            var auditLogs = _db.auditLogs
                .OrderByDescending(a => a.Date)
                .ToList();
            
            return View(auditLogs);
        }

        // Afficher les détails d'un log spécifique
        public IActionResult Details(int id)
        {
            var auditLog = _db.auditLogs.Find(id);
            if (auditLog == null)
            {
                return NotFound();
            }
            return View(auditLog);
        }
    }
}
