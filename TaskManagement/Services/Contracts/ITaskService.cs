using TaskManagement.Database.Entities;


namespace TaskManagement.Services.Contracts
{
    public interface ITaskService
    {
        
        UserTask CreateTask(UserTask task);
        UserTask UpdateTask(UserTask task);
        bool DeleteTask(Guid taskId);
        UserTask MoveTask(Guid taskId, Guid destinationColumnId);
    }
}
