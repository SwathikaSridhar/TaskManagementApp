using Microsoft.EntityFrameworkCore;
using TaskManagement.Database.Entities;
using TaskManagement.Services.Contracts;
using TaskManagemnet.Database;

namespace TaskManagement.Services
{
    public class ColumnService : IColumnService
    {
        private readonly TaskManagementContext taskManagementContext;
        public ColumnService(TaskManagementContext taskManagementContext)
        {
            this.taskManagementContext = taskManagementContext;
        }

        public IEnumerable<Column> GetAllColumns(bool includeTasks)
        {
            return includeTasks ?  taskManagementContext.Columns.Include(t => t.Tasks) : taskManagementContext.Columns;
        }

        public Column? GetColumnById(Guid id, bool includeTasks)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("id not found.");
            return includeTasks ? taskManagementContext.Columns.Include(t => t.Tasks).FirstOrDefault(c => c.Id == id) : taskManagementContext.Columns.FirstOrDefault(c => c.Id == id);
        }
    }
}
