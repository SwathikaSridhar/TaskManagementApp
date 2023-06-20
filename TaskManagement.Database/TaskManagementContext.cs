using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Database.Entities;

namespace TaskManagemnet.Database
{
    public class TaskManagementContext : DbContext
    {
        public TaskManagementContext(DbContextOptions<TaskManagementContext> options) : base(options)
        {

        }

        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<Column> Columns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
        }
    }
}
