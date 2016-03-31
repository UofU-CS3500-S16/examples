using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.ServiceModel.Web;
using static System.Net.HttpStatusCode;

namespace ToDoList
{
    /// <summary>
    /// Provides implementations of the operations belonging to the service.
    /// </summary>
    public class ToDo : IToDo
    {
        // This amounts to a "poor man's" database.  The state of the service is
        // maintained in users and items.  The sync object is used
        // for synchronization (because multiple threads can be running
        // simultaneously in the service).  The entire state is lost each time
        // the service shuts down, so eventually we'll need to port this to
        // a proper database.
        private readonly static Dictionary<String, UserInfo> users = new Dictionary<String, UserInfo>();
        private readonly static Dictionary<String, ToDoItem> items = new Dictionary<String, ToDoItem>();
        private static readonly object sync = new object();

        /// <summary>
        /// Sets the status code for the next HTTP response.
        /// </summary>
        private static void SetStatus(HttpStatusCode code)
        {
            WebOperationContext.Current.OutgoingResponse.StatusCode = code;
        }

        public string Register(UserInfo user)
        {
            lock (sync)
            {
                if (user.Name == null || user.Name.Trim().Length == 0)
                {
                    SetStatus(Forbidden);
                    return null;
                }
                else
                {
                    string userID = Guid.NewGuid().ToString();
                    users.Add(userID, user);
                    SetStatus(Created);
                    return userID;
                }
            }
        }

        public string AddItem(ToDoItem item)
        {
            lock (sync)
            {
                if (item.UserID == null || !users.ContainsKey(item.UserID))
                {
                    SetStatus(Forbidden);
                    return null;
                }
                else
                {
                    string itemID = Guid.NewGuid().ToString();
                    item.ItemID = itemID;
                    items.Add(itemID, item);
                    SetStatus(Created);
                    return itemID;
                }
            }
        }

        public void MarkCompleted(string itemID)
        {
            lock (sync)
            {
                ToDoItem item;
                if (!items.TryGetValue(itemID, out item))
                {
                    SetStatus(Forbidden);
                }
                else
                {
                    item.Completed = true;
                    SetStatus(OK);
                }
            }
        }

        public void DeleteItem(string itemID)
        {
            lock (sync)
            {
                if (!items.ContainsKey(itemID))
                {
                    SetStatus(Forbidden);
                }
                else
                {
                    items.Remove(itemID);
                }
            }
        }


        public IList<ToDoItem> GetAllItems(bool completedOnly, string userID)
        {
            lock (sync)
            {
                if (userID != null && !users.ContainsKey(userID))
                {
                    SetStatus(Forbidden);
                    return null;
                }
                else
                {
                    List<ToDoItem> result = new List<ToDoItem>();
                    foreach (ToDoItem item in items.Values)
                    {
                        if (userID != null && item.UserID != userID) continue;
                        if (completedOnly && !item.Completed) continue;
                        result.Add(item);
                    }
                    SetStatus(OK);
                    return result;
                }
            }
        }
    }
}
