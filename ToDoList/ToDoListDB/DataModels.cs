namespace ToDoList
{
    public class UserInfo
    {
        public string Name { get; set; }

        public string Email { get; set; }
    }

    public class ToDoItem
    {
        public string UserID { get; set; }

        public string Description { get; set; }

        public bool Completed { get; set; }

        public string ItemID { get; set; }
    }
}
