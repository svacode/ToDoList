namespace ToDoList.Entities
{
    public class ToDo
    {

        public int Id { get; set; }
        public required string Timeline { get; set; } 
        public required string One { get; set; } 

        public string Two { get; set; } = string.Empty ;

        public string Three { get; set; } = string.Empty;

        public string Four { get; set; } = string.Empty;
        public string Five { get; set; } = string.Empty;
        public string Six { get; set; } = string.Empty;
        public string Seven { get; set; } = string.Empty;
        public string Eight { get; set; } = string.Empty;

        

    }
}
