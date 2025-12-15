using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MyFirstApp.Models;
using System.Text.Json;

namespace MyFirstApp.Interceptors
{
   
    public class AuditLogInterceptor : SaveChangesInterceptor
    {
        
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            
            CaptureAuditLogs(eventData.Context);
            
            
            return base.SavingChanges(eventData, result);
        }

        
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            CaptureAuditLogs(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        
        private void CaptureAuditLogs(DbContext? context)
        {
            if (context == null) return;

           
            var entries = context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                           e.State == EntityState.Modified ||
                           e.State == EntityState.Deleted)
                .ToList();

            
            foreach (var entry in entries)
            {
               
                if (entry.Entity is AuditLog)
                    continue;

                var auditLog = new AuditLog
                {
                    
                    TableName = entry.Entity.GetType().Name,
                    
                    
                    Action = entry.State.ToString(),
                    
                    
                    EntityKey = GetEntityKey(entry),
                    
                    
                    Changes = GetChanges(entry),
                    
                    
                    Date = DateTime.UtcNow
                };

                // Ajouter le log d'audit au contexte
                context.Set<AuditLog>().Add(auditLog);
            }
        }

        
        private string GetEntityKey(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry)
        {
            // Trouver les propriétés qui constituent la clé primaire
            var keyValues = entry.Properties
                .Where(p => p.Metadata.IsPrimaryKey())
                .Select(p => p.CurrentValue?.ToString() ?? "null");

            // Combiner les valeurs avec un tiret (ex: "123" ou "1-ABC" pour clés composites)
            return string.Join("-", keyValues);
        }

        
        private string? GetChanges(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry)
        {
            // Capturer les changements seulement pour les modifications
            if (entry.State != EntityState.Modified)
                return null;

            // Créer un dictionnaire des changements
            var changes = new Dictionary<string, object>();

            foreach (var property in entry.Properties)
            {
                // Vérifier si la propriété a été modifiée
                if (property.IsModified)
                {
                    changes[property.Metadata.Name] = new
                    {
                        // Valeur avant la modification
                        OldValue = property.OriginalValue?.ToString() ?? "null",
                        
                        // Nouvelle valeur
                        NewValue = property.CurrentValue?.ToString() ?? "null"
                    };
                }
            }

            // Si aucun changement, retourner null
            if (changes.Count == 0)
                return null;

            // Sérialiser les changements en JSON
            return JsonSerializer.Serialize(changes, new JsonSerializerOptions 
            { 
                WriteIndented = true // Format lisible
            });
        }
    }
}
