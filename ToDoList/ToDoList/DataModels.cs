using System;
using System.Runtime.Serialization;

namespace ToDoList
{
    public class RegInfo
    {
        public String UserID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }

    public class ToDoItem
    {
        public string ItemID { get; set; }

        public string UserID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }

        public bool Completed { get; set; }
    }
}
