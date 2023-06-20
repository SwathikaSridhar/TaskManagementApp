using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.AutoMock;
using System;
using System.Threading.Tasks;
using TaskManagement.Controllers;
using TaskManagement.Database.Entities;
using TaskManagement.Services.Contracts;

namespace TaskManagement.UnitTests.Controllers
{
    [TestClass]
    public class TaskManagementControllerTests
    {
        [TestMethod]
        public async Task CreateTask_ReturnsOk()
        {
            var automocker = new AutoMocker();
            var httpContext = new Mock<HttpContext>();
            var userTask = new UserTask()
            {
                Name = "Create Task",
                Description = "Create new task",
                ColumnId = Guid.NewGuid(),
                ImageUrl = "",
                IsFavorite = true,
                Deadline = DateTime.Now,
                Id = Guid.NewGuid()
            };

            //act
            var controller = automocker.CreateInstance<TaskController>();
            controller.ControllerContext.HttpContext = httpContext.Object;

            automocker.Setup<ITaskService, UserTask>(x => x.CreateTask(It.IsAny<UserTask>())).Returns(userTask);

            var result = await controller.CreateTask(userTask);
            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CreatedResult));
        }

        [TestMethod]
        public async Task CreateTask_ReturnsBadRequest()
        {
            var automocker = new AutoMocker();
            UserTask userTask = null;

            //act
            var controller = automocker.CreateInstance<TaskController>();

            var result = await controller.CreateTask(userTask);
            // Assert


            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task UpdateTask_ReturnsOk()
        {
            var automocker = new AutoMocker();
            var userTask = new UserTask()
            {
                Name = "Create Task",
                Description = "Create new task",
                ColumnId = Guid.NewGuid(),
                ImageUrl = "",
                IsFavorite = true,
                Deadline = DateTime.Now,
                Id = Guid.NewGuid()
            };

            //act
            var controller = automocker.CreateInstance<TaskController>();

            automocker.Setup<ITaskService, UserTask>(x => x.CreateTask(It.IsAny<UserTask>())).Returns(userTask);

            await controller.CreateTask(userTask);

            userTask.Description = "Description Updated";

            automocker.Setup<ITaskService, UserTask>(x => x.UpdateTask(It.IsAny<UserTask>())).Returns(userTask);

            var result = await controller.UpdateTask(userTask);

            UserTask updateUserTask = (UserTask)(result as OkObjectResult).Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userTask, updateUserTask);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task UpdateTask_ReturnsBadRequest()
        {
            var automocker = new AutoMocker();
            UserTask userTask = null;

            //act
            var controller = automocker.CreateInstance<TaskController>();

            var result = await controller.UpdateTask(userTask);
            // Assert


            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task DeleteTask_ReturnsOk()
        {
            var automocker = new AutoMocker();
            var userTask = new UserTask()
            {
                Name = "Create Task",
                Description = "Create new task",
                ColumnId = Guid.NewGuid(),
                ImageUrl = "",
                IsFavorite = true,
                Deadline = DateTime.Now,
                Id = Guid.NewGuid()
            };

            //act
            var controller = automocker.CreateInstance<TaskController>();

            automocker.Setup<ITaskService, UserTask>(x => x.CreateTask(It.IsAny<UserTask>())).Returns(userTask);

            await controller.CreateTask(userTask);

            userTask.Description = "Description Updated";

            automocker.Setup<ITaskService, bool>(x => x.DeleteTask(It.IsAny<Guid>())).Returns(true);

            var result = await controller.DeleteTask(userTask.Id);

            var res = (result as OkObjectResult).Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(res, "Task deleted");
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task DeleteTask_ReturnsBadRequest()
        {
            var automocker = new AutoMocker();
            UserTask userTask = new UserTask();

            //act
            var controller = automocker.CreateInstance<TaskController>();

            var result = await controller.DeleteTask(userTask.Id);
            // Assert


            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task MoveTask_ReturnsOk()
        {
            var automocker = new AutoMocker();
            var userTask = new UserTask()
            {
                Name = "Create Task",
                Description = "Create new task",
                ColumnId = Guid.NewGuid(),
                ImageUrl = "",
                IsFavorite = true,
                Deadline = DateTime.Now,
                Id = Guid.NewGuid()
            };

            //act
            var controller = automocker.CreateInstance<TaskController>();

            automocker.Setup<ITaskService, UserTask>(x => x.CreateTask(It.IsAny<UserTask>())).Returns(userTask);

            await controller.CreateTask(userTask);

           

            automocker.Setup<ITaskService, UserTask>(x => x.MoveTask(It.IsAny<Guid>(),It.IsAny<Guid>())).Returns(userTask);

            var result = await controller.MoveTask(userTask.Id,Guid.NewGuid());  

            // Assert
            Assert.IsNotNull(result);           
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task MoveTask_ReturnsBadRequest()
        {
            var automocker = new AutoMocker();
            var id = Guid.Empty;
            var columnId = Guid.Empty;

            //act
            var controller = automocker.CreateInstance<TaskController>();

            var result = await controller.MoveTask(id, columnId);
            // Assert


            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

    }
}
