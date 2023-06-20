using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Database.Entities;
using TaskManagement.Services;
using TaskManagemnet.Database;

namespace TaskManagement.UnitTests.Services
{
    [TestClass]
    public class ColumnServiceTests
    {
        [TestMethod]
        public async Task GetAllColumns_ReturnValue_WithTask()
        {
            var automocker = new AutoMocker();

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

            using (var dataContext = new TaskManagementContext(TestHelper.GetTaskManagementDataContextOptions()))
            {
                dataContext.Columns.AddRange(column);
                dataContext.SaveChanges();
                automocker.Use(dataContext);

                var service = automocker.CreateInstance<ColumnService>();

                var result = service.GetAllColumns(true);

                Assert.IsNotNull(result);
                Assert.AreNotEqual(result.First().Tasks.Count, 0);               
            }
        }


        [TestMethod]
        public async Task GetAllColumns_ReturnValue_WithoutTask()
        {
            var automocker = new AutoMocker();

            var column = new List<Column>
            {
                new Column()
                {
                    Id = Guid.NewGuid(),
                    Name = "ToDo",
                    Tasks = new List<UserTask>()
                }
            };

            using (var dataContext = new TaskManagementContext(TestHelper.GetTaskManagementDataContextOptions()))
            {
                dataContext.Columns.AddRange(column);
                dataContext.SaveChanges();
                automocker.Use(dataContext);

                var service = automocker.CreateInstance<ColumnService>();

                var result = service.GetAllColumns(false);

                Assert.IsNotNull(result);
                Assert.AreEqual(result.First().Tasks.Count, 0);
            }
        }

        [TestMethod]
        public async Task GetColumnById_ReturnValue_WithTask()
        {
            var automocker = new AutoMocker();
            var columnId= Guid.NewGuid();
            var column = new List<Column>
            {
                new Column()
                {
                    Id = columnId,
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

            using (var dataContext = new TaskManagementContext(TestHelper.GetTaskManagementDataContextOptions()))
            {
                dataContext.Columns.AddRange(column);
                dataContext.SaveChanges();
                automocker.Use(dataContext);

                var service = automocker.CreateInstance<ColumnService>();

                var result = service.GetColumnById(columnId,true);

                Assert.IsNotNull(result);
                Assert.AreNotEqual(result.Tasks.Count, 0);
            }
        }

        [TestMethod]
        public async Task GetColumnById_ReturnValue_WithoutTask()
        {
            var automocker = new AutoMocker();
            var columnId = Guid.NewGuid();
            var column = new List<Column>
            {
                new Column()
                {
                    Id = columnId,
                    Name = "ToDo",
                    Tasks = new List<UserTask>()
                }
            };

            using (var dataContext = new TaskManagementContext(TestHelper.GetTaskManagementDataContextOptions()))
            {
                dataContext.Columns.AddRange(column);
                dataContext.SaveChanges();
                automocker.Use(dataContext);

                var service = automocker.CreateInstance<ColumnService>();

                var result = service.GetColumnById(columnId,false);

                Assert.IsNotNull(result);
                Assert.AreEqual(result.Tasks.Count, 0);
            }
        }
    }
}
