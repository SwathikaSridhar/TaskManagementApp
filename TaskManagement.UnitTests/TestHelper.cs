using Microsoft.EntityFrameworkCore;
using TaskManagemnet.Database;

namespace TaskManagement.UnitTests
{
    public static class TestHelper
    {
        public static DbContextOptions<TaskManagementContext> GetTaskManagementDataContextOptions([System.Runtime.CompilerServices.CallerMemberName] string name = "")
        {
            var options = new DbContextOptionsBuilder<TaskManagementContext>()
                .UseInMemoryDatabase(databaseName: name).EnableSensitiveDataLogging().Options;

            return options;
        }
    }
}
