using System;
using System.Collections.Generic;
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
        private readonly static List<RegInfo> users = new List<RegInfo>();
        private readonly static List<ToDoItem> items = new List<ToDoItem>();
        private static readonly object sync = new object();

        /// <summary>
        /// Sets the status code for the next HTTP response.
        /// </summary>
        private static void SetStatus (HttpStatusCode code)
        {
            WebOperationContext.Current.OutgoingResponse.StatusCode = code;
        }

        public string Register(RegInfo reg)
        {
            lock (sync)
            {
                if (reg.Name == null)
                {
                    SetStatus(Forbidden);
                }
                reg.UserID = Guid.NewGuid().ToString();
                users.Add(reg);
                return reg.UserID;
            }
        }

        public IList<ToDoItem> GetAllItems(bool includeCompleted, string userID)
        {
            List<ToDoItem> result = new List<ToDoItem>();
            lock (sync)
            {
                foreach (ToDoItem item in items)
                {
                    if ((!item.Completed || includeCompleted) && ((userID == "") || (item.UserID == userID)))
                    {
                        result.Add(item);
                    }
                }
            }
            return result;
        }

        public string AddItem(ToDoItem item)
        {
            //WebOperationContext.Current.OutgoingResponse.StatusCode = Forbidden;
            RegInfo reg = LookupUser(item.UserID);
            if (reg == null)
            {
                return "";
            }

            lock (sync)
            {
                item.ItemID = Guid.NewGuid().ToString();
                items.Add(item);
                return item.ItemID;
            }
        }

        public RegInfo LookupUser(string userID)
        {
            foreach (RegInfo r in users)
            {
                if (r.UserID == userID) return r;
            }
            return null;
        }

        public void MarkCompleted(string itemID)
        {
            lock (sync)
            {
                foreach (ToDoItem item in items)
                {
                    if (item.ItemID == itemID)
                    {
                        item.Completed = true;
                        return;
                    }
                }
            }
        }

        public void DeleteItem(string uid)
        {
            lock (sync)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].ItemID.ToString() == uid)
                    {
                        items.RemoveAt(i);
                        return;
                    }
                }
            }
        }
    }
}
