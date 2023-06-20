namespace TaskManagement.Database.Entities
{
    public class Column
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<UserTask> Tasks { get; set; } = new List<UserTask>();
    }
}
