﻿using System.Text.Json.Serialization;

namespace ToDoList.Entities
{
    public class ToDo
    {
        [JsonIgnore]
        public int Id { get; set; }
        public required string Timeline { get; set; } 
        public required string Header { get; set; } 

        public string Description { get; set; } = string.Empty ;

        public string TimeToFinish { get; set; } = string.Empty;

        public string Deadline { get; set; } = string.Empty;

        public bool? Done { get; set; }

        public Priority Priority { get; set; }


        

    }
    public enum Priority
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
}
