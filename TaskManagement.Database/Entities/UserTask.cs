using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Database.Entities
{
    public class UserTask
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsFavorite { get; set; }
        public string ImageUrl { get; set; }
        
        public Guid ColumnId { get; set; }
    }
}
