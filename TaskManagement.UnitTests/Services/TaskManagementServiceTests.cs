using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq.AutoMock;
using System;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Database.Entities;
using TaskManagement.Services;
using TaskManagemnet.Database;

namespace TaskManagement.UnitTests.Services
{
    [TestClass]
    public class TaskServiceTests
    {
        [TestMethod]
        public async Task CreateTask_ReturnValue()
        {
            var automocker = new AutoMocker();
            
            var userTask = new UserTask()
            {
                Name = "Create Task",
                Description = "Create new task",
                ColumnId = Guid.NewGuid(),
                ImageUrl = "",
                IsFavorite = true,
                Deadline = DateTime.Now                
            };

            using (var dataContext = new TaskManagementContext(TestHelper.GetTaskManagementDataContextOptions()))
            {
                automocker.Use(dataContext);

                var service = automocker.CreateInstance<TaskService>();

                var result = service.CreateTask(userTask);

                Assert.IsNotNull(result);
                Assert.AreEqual(result, userTask);
               
            }
        }

        [TestMethod]
        public async Task UpdateTask_ReturnValue()
        {
            var automocker = new AutoMocker();

            var userTask = new UserTask()
            {
                Name = "Create Task",
                Description = "Create new task",
                ColumnId = Guid.NewGuid(),
                ImageUrl = "",
                IsFavorite = true,
                Deadline = DateTime.Now
            };

            using (var dataContext = new TaskManagementContext(TestHelper.GetTaskManagementDataContextOptions()))
            {
                dataContext.UserTasks.Add(userTask);
                await dataContext.SaveChangesAsync();
                automocker.Use(dataContext);

                var service = automocker.CreateInstance<TaskService>();

                userTask.Description = "Description updated";

                var result = service.UpdateTask(userTask);

                Assert.IsNotNull(result);
                Assert.AreEqual(result, userTask);

            }
        }

        [TestMethod]
        public async Task DeleteTask_ReturnValue()
        {
            var automocker = new AutoMocker();

            var userTask = new UserTask()
            {
                Name = "Create Task",
                Description = "Create new task",
                ColumnId = Guid.NewGuid(),
                ImageUrl = "",
                IsFavorite = true,
                Deadline = DateTime.Now
            };

            using (var dataContext = new TaskManagementContext(TestHelper.GetTaskManagementDataContextOptions()))
            {
                dataContext.UserTasks.Add(userTask);
                await dataContext.SaveChangesAsync();
                automocker.Use(dataContext);

                var availableTask = dataContext.UserTasks.FirstOrDefault(c=>c.Id == userTask.Id);

                Assert.IsNotNull(availableTask);

                var service = automocker.CreateInstance<TaskService>();               

                var result = service.DeleteTask(userTask.Id);

                Assert.IsNotNull(result);

                availableTask = dataContext.UserTasks.FirstOrDefault(c => c.Id == userTask.Id);

                Assert.IsNull(availableTask);

            }
        }

        [TestMethod]
        public async Task MoveTask_ReturnValue()
        {
            var automocker = new AutoMocker();

            var userTask = new UserTask()
            {
                Name = "Create Task",
                Description = "Create new task",
                ColumnId = Guid.NewGuid(),
                ImageUrl = "",
                IsFavorite = true,
                Deadline = DateTime.Now
            };

            using (var dataContext = new TaskManagementContext(TestHelper.GetTaskManagementDataContextOptions()))
            {
                dataContext.UserTasks.Add(userTask);
                await dataContext.SaveChangesAsync();
                automocker.Use(dataContext);

                var availableTask = dataContext.UserTasks.FirstOrDefault(c => c.Id == userTask.Id);

                Assert.IsNotNull(availableTask);

                var service = automocker.CreateInstance<TaskService>();
                var newColumnId = Guid.NewGuid();
                var result = service.MoveTask(userTask.Id, newColumnId);

                Assert.IsNotNull(result);
                Assert.AreEqual(result.ColumnId, newColumnId);               

            }
        }
    }
}
