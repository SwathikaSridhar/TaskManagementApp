using TaskManagement.Database.Entities;

namespace TaskManagement.Services.Contracts
{
    public interface IColumnService
    {
        IEnumerable<Column> GetAllColumns(bool includeTasks);
        Column GetColumnById(Guid id, bool includeTasks);
    }
}
