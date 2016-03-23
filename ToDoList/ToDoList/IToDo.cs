using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

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
        /// Registers a new user.  Returns the UserID of the user.
        /// </summary>
        [WebInvoke(Method = "POST", UriTemplate = "/RegisterUser")]
        string Register(RegInfo reg);

        /// <summary>
        /// Returns a list of ToDoSummaries.  It returns a summary of just the incomplete
        /// tasks unless the includeCompleted parameter is true.  It returns summaries for all users,
        /// unless userID is provided.  The method and URL required to access the service appears
        /// in the WebInvoke annotation.  The response is encoded as JSON.
        /// </summary>
        [WebGet(UriTemplate = "/GetAllItems?completed={includeCompleted}&user={userID}")]
        IList<ToDoItem> GetAllItems(bool includeCompleted, string userID);

        /// <summary>
        /// Adds an item to the ToDo list.  The item should be a ToDoItem encoded in
        /// JSON (the supplied Uid is ignored).  The response is the ItemID, encoded as JSON.
        /// </summary>
        [WebInvoke(Method = "POST", UriTemplate = "/AddItem")]
        string AddItem(ToDoItem data);

        /// <summary>
        /// Marks an item with the specified ItemID as completed.
        /// </summary>
        [WebInvoke(Method = "PUT", UriTemplate = "/MarkCompleted/{itemID}")]
        void MarkCompleted(string itemID);

        /// <summary>
        /// Deletes the item with the specified itemID.
        /// </summary>
        [WebInvoke(Method = "DELETE", UriTemplate = "/DeleteItem/{itemID}")]
        void DeleteItem(string itemID);
    }
}
