namespace ToDoList.Entities
{
    public class ToDo
    {

        public int Id { get; set; }
        public required string Timeline { get; set; }
        public string Content { get; set; } = string.Empty;
        

    }
}
