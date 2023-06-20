using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TaskManagement.Database.Entities;
using TaskManagement.Services.Contracts;
using TaskManagemnet.Database;

namespace TaskManagement.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskManagementContext taskManagementContext;

        public TaskService(TaskManagementContext taskManagementContext)
        {
            this.taskManagementContext = taskManagementContext;
        }

        public UserTask CreateTask(UserTask task)
        {
            if (task == null)
                throw new ArgumentException("Column not found.");

            task.Id = Guid.NewGuid(); 
            taskManagementContext.UserTasks.Add(task);
            taskManagementContext.SaveChanges();
            return task;
        }

        public UserTask UpdateTask(UserTask task)
        {
            var existingTask = GetTask(task.Id);
            if (existingTask == null)
                throw new ArgumentException("Task not found.");

            
            existingTask.Name = task.Name;
            existingTask.Description = task.Description;
            existingTask.Deadline = task.Deadline;
            existingTask.IsFavorite = task.IsFavorite;
            existingTask.ImageUrl = task.ImageUrl;
            existingTask.ColumnId = task.ColumnId;

            
            taskManagementContext.SaveChanges();
            return existingTask;
            
        }

        public bool DeleteTask(Guid taskId)
        {
            var task = GetTask(taskId);
            if (task == null)
                throw new ArgumentException("Task not found.");

            taskManagementContext.UserTasks.Remove(task);
            taskManagementContext.SaveChanges();
            return true;
        }

        public UserTask MoveTask(Guid taskId, Guid destinationColumnId)
        {
            var task = GetTask(taskId);
            if (task == null)
                throw new ArgumentException("Task not found.");

            task.ColumnId = destinationColumnId;
            taskManagementContext.SaveChanges();
            return task;
        }  
        
        private UserTask GetTask(Guid taskId)
        {
            return taskManagementContext.UserTasks.FirstOrDefault(t => t.Id == taskId);
        }
    }
}
