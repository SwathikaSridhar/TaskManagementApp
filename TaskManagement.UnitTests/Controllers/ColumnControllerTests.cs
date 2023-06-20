using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Controllers;
using TaskManagement.Database.Entities;
using TaskManagement.Services.Contracts;

namespace TaskManagement.UnitTests.Controllers
{
    [TestClass]
    public class ColumnControllerTests
    {
        [TestMethod]
        public async Task GetColumns_ReturnsOk_WithTasks()
        {
            var automocker = new AutoMocker();
            var httpContext = new Mock<HttpContext>();
            var column = new List<Column>
            {
                new Column()
                {
                    Id = Guid.NewGuid(),
                    Name = "ToDo",
                    Tasks = new List<UserTask>()
                {
                    new UserTask()
                    {
                        Name = "Create Task",
                        Description = "Create new task",
                        ColumnId = Guid.NewGuid(),
                        ImageUrl = "",
                        IsFavorite = true,
                        Deadline = DateTime.Now,
                        Id = Guid.NewGuid()
                    }
                }
                }
            };


            //act
            var controller = automocker.CreateInstance<ColumnController>();
            controller.ControllerContext.HttpContext = httpContext.Object;

            automocker.Setup<IColumnService, IEnumerable<Column>>(x => x.GetAllColumns(It.IsAny<bool>())).Returns(column);

            var result = await controller.GetColumns(true);

            var columns = result as List<Column>;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(columns?.First().Tasks.Count, 0);
        }

        [TestMethod]
        public async Task GetColumns_ReturnsOk_WithoutTasks()
        {
            var automocker = new AutoMocker();
            var httpContext = new Mock<HttpContext>();
            var column = new List<Column>
            {
                new Column()
                {
                    Id = Guid.NewGuid(),
                    Name = "ToDo",
                    Tasks = new List<UserTask>()
                }
            };


            //act
            var controller = automocker.CreateInstance<ColumnController>();
            controller.ControllerContext.HttpContext = httpContext.Object;

            automocker.Setup<IColumnService, IEnumerable<Column>>(x => x.GetAllColumns(It.IsAny<bool>())).Returns(column);

            var result = await controller.GetColumns();

            var columns = (result as OkObjectResult).Value as List<Column>;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(columns?.First().Tasks.Count, 0);
        }


        [TestMethod]
        public async Task GetColumnById_ReturnsOk_WithTasks()
        {
            var automocker = new AutoMocker();
            var httpContext = new Mock<HttpContext>();
            var columnId = Guid.NewGuid();
            var column = new Column()
            {
                Id = Guid.NewGuid(),
                Name = "ToDo",
                Tasks = new List<UserTask>(){
                    new UserTask()
                    {
                        Name = "Create Task",
                        Description = "Create new task",
                        ColumnId = columnId,
                        ImageUrl = "",
                        IsFavorite = true,
                        Deadline = DateTime.Now,
                        Id = Guid.NewGuid()
                    }
                }
            };


            //act
            var controller = automocker.CreateInstance<ColumnController>();
            controller.ControllerContext.HttpContext = httpContext.Object;

            automocker.Setup<IColumnService, Column>(x => x.GetColumnById(It.IsAny<Guid>(), It.IsAny<bool>())).Returns(column);

            var result = await controller.GetColumnById(columnId,true);

            var columns = (result as OkObjectResult).Value as Column;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(columns?.Tasks.Count, 0);
        }

        [TestMethod]
        public async Task GetColumnById_ReturnsOk_WithoutTasks()
        {
            var automocker = new AutoMocker();
            var httpContext = new Mock<HttpContext>();
            var columnId = Guid.NewGuid();
            var column = new Column()
            {
                Id = Guid.NewGuid(),
                Name = "ToDo",
                Tasks = new List<UserTask>()
            };


            //act
            var controller = automocker.CreateInstance<ColumnController>();
            controller.ControllerContext.HttpContext = httpContext.Object;

            automocker.Setup<IColumnService, Column>(x => x.GetColumnById(It.IsAny<Guid>(), It.IsAny<bool>())).Returns(column);

            var result = await controller.GetColumnById(columnId);

            var columns = (result as OkObjectResult).Value as Column;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(columns?.Tasks.Count, 0);
        }
    }
}
