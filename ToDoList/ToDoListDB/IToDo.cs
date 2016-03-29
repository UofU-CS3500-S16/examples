using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace ToDoList
{
    /// <summary>
    /// This interface defines a collection of operations provided by ToDo.svc.  Each method that 
    /// is annotated as with [WebGet] or [WebInvoke] will be exposed by the service
    /// </summary>
    [ServiceContract]
    public interface IToDo
    {
        /// <summary>
        /// Registers a new user.
        /// If either user.Name or user.Email is null or is empty after trimming, responds with status code Forbidden.
        /// Otherwise, creates a user, returns the user's token, and responds with status code Created. 
        /// </summary>
        [WebInvoke(Method = "POST", UriTemplate = "/RegisterUser")]
        string Register(UserInfo user);

        /// <summary>
        /// Adds an item to the ToDo list.  
        /// If item.UserID isn't known, responds with status code Forbidden.
        /// Othewise, adds the item to the list, returns the new ItemID, and responds with status code Created.
        /// </summary>
        [WebInvoke(Method = "POST", UriTemplate = "/AddItem")]
        string AddItem(ToDoItem item);

        /// <summary>
        /// Marks an item as completed.
        /// If itemID is unknown, responds with status code Forbidden.
        /// Otherwise, marks the item as completeed and responds with status code OK.
        /// </summary>
        [WebInvoke(Method = "PUT", UriTemplate = "/MarkCompleted/{itemID}")]
        void MarkCompleted(string itemID);

        /// <summary>
        /// Deletes an item.
        /// If itemID is unknown, responds with status code Forbidden.
        /// Otherwise, deletes the item and responds with status code OK.
        /// </summary>
        [WebInvoke(Method = "DELETE", UriTemplate = "/DeleteItem/{itemID}")]
        void DeleteItem(string itemID);

        /// <summary>
        /// Returns a list of ToDoItems.  
        /// If userID is provided and doesn't exist, responds with status code Forbidden.
        /// Otherwise:
        ///   Returns all ToDoItems if completedOnly is false or missing and userID is missing
        ///   Returns all completed ToDoItems if completedOnly is true and userID is missing
        ///   Returns all of userID's ToDoItems if completedOnly is false or missing
        ///   Returns userID's completed ToDoItems if completedOnly is true
        ///   In all cases responds with status code OK.
        /// </summary>
        [WebGet(UriTemplate = "/GetAllItems?completed={completedOnly}&user={userID}")]
        IList<ToDoItem> GetAllItems(bool completedOnly, string userID);


    }
}
