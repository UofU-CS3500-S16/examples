using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
        // The connection string to the DB
        private static string ToDoDB;

        static ToDo()
        {
            // Saves the connection string for the database.  A connection string contains the
            // information necessary to connect with the database server.  When you create a
            // DB, there is generally a way to obtain the connection string.  From the Server
            // Explorer pane, obtain the properties of DB to see the connection string.

            // The connection string of my ToDoDB.mdf shows as
            //
            //    Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="C:\Users\zachary\Source\CS 3500 S16\examples\ToDoList\ToDoListDB\App_Data\ToDoDB.mdf";Integrated Security=True
            //
            // Unfortunately, this is absolute pathname on my computer, which means that it
            // won't work if the solution is moved.  Fortunately, it can be shorted to
            //
            //    Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="|DataDirectory|\ToDoDB.mdf";Integrated Security=True
            //
            // You should shorten yours this way as well.
            //
            // Rather than build the connection string into the program, I store it in the Web.config
            // file where it can be easily found and changed.  You should do that too.
            ToDoDB = ConfigurationManager.ConnectionStrings["BoggleDB"].ConnectionString;
        }

        /// <summary>
        /// Sets the status code for the next HTTP response.
        /// </summary>
        private static void SetStatus(HttpStatusCode code)
        {
            WebOperationContext.Current.OutgoingResponse.StatusCode = code;
        }

        public string Register(UserInfo user)
        {
            // This validates the user.Name property.  It is best to do any work that doesn't
            // involve the database before creating the DB connection or after closing it.
            if (user.Name == null || user.Name.Trim().Length == 0 || user.Name.Trim().Length > 50)
            {
                SetStatus(Forbidden);
                return null;
            }

            // The first step to using the DB is opening a connection to it.  Creating it in a
            // using block guarantees that the connection will be closed when control leaves
            // the block.  As you'll see below, I also follow this pattern for SQLTransactions,
            // SqlCommands, and SqlDataReaders.
            using (SqlConnection conn = new SqlConnection(ToDoDB))
            {
                // Connections must be opened
                conn.Open();

                // Database commands should be executed within a transaction.  When commands 
                // are executed within a transaction, either all of the commands will succeed
                // or all will be canceled.  You don't have to worry about some of the commands
                // changing the DB and others failing.
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    // An SqlCommand executes a SQL statement on the database.  In this case it is an
                    // insert statement.  The first parameter is the statement, the second is the
                    // connection, and the third is the transaction.  
                    //
                    // Note that I use symbols like @UserID as placeholders for values that need to appear
                    // in the statement.  You will see below how the placeholders are replaced.  You may be
                    // tempted to simply paste the values into the string, but this is a BAD IDEA that violates
                    // a cardinal rule of DB Security 101.  By using the placeholder approach, you don't have
                    // to worry about escaping special characters and you don't have to worry about one form
                    // of the SQL insertion attack.
                    using (SqlCommand command =
                        new SqlCommand("insert into Users (UserID, Name, Email) values(@UserID, @Nickname, @Email)",
                                        conn,
                                        trans))
                    {
                        // We generate the userID to use.
                        string userID = Guid.NewGuid().ToString();

                        // This is where the placeholders are replaced.
                        command.Parameters.AddWithValue("@UserID", userID);
                        command.Parameters.AddWithValue("@Nickname", user.Name.Trim());
                        command.Parameters.AddWithValue("@Email", user.Email.Trim());

                        // This executes the command within the transaction over the connection.  The number of rows
                        // that were modified is returned.  Perhaps I should check and make sure that 1 is returned
                        // as expected.
                        command.ExecuteNonQuery();
                        SetStatus(Created);

                        // Immediately before each return that appears within the scope of a transaction, it is
                        // important to commit the transaction.  Otherwise, the transaction will be aborted and
                        // rolled back as soon as control leaves the scope of the transaction. 
                        trans.Commit();
                        return userID;
                    }
                }
            }

            // The method can terminate with a thrown exception for any number of reasons.  For example:
            //
            //  - The DB connection might fail
            //  - The DB may find it necessary to abort the transaction because of a conflict with
            //     some other user's transaction
            //  - The insert may fail because it violates a primary key constraint 
            //
            // In a more robust implementation, we would want to catch and deal with exceptions if.  For example:
            //
            //  - If the DB aborts a transaction, we could just try it again.
            //  - If the userID violates a uniqueness constraint because it is a duplicate, we could generate
            //     a different one and try again.
            //
            // An unhandled exception in a REST request will not crash the server.  In a production server, we 
            // would want to record all unhandled exceptions to a log file for later examination.  Otherwise,
            // problems might remain undetected.  There is a probably a way to configure IIS so that it automtically 
            // logs exceptions.  
            //
            // For your purposes, it is best to simply let unhandled exceptions propagate.  This will make
            // debugging easier.
        }

        public string AddItem(ToDoItem item)
        {
            if (item.UserID == null)
            {
                SetStatus(Forbidden);
                return null;
            }

            using (SqlConnection conn = new SqlConnection(ToDoDB))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {

                    // Here, the SqlCommand is a select query.  We are interested in whether item.UserID exists in
                    // the Users table.
                    using (SqlCommand command = new SqlCommand("select UserID from Users where UserID = @UserID", conn, trans))
                    {
                        command.Parameters.AddWithValue("@UserID", item.UserID);

                        // This executes a query (i.e. a select statement).  The result is an
                        // SqlDataReader that you can use to iterate through the rows in the response.
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // In this we don't actually need to read any data; we only need
                            // to know whether a row was returned.
                            if (!reader.HasRows)
                            {
                                SetStatus(Forbidden);
                                trans.Commit();
                                return null;
                            }
                        }
                    }

                    // Here we are executing an insert command, but notice the "output inserted.ItemID" portion.  
                    // We are asking the DB to send back the auto-generated ItemID.
                    using (SqlCommand command = new SqlCommand("insert into Items (UserID, Description, Completed) output inserted.ItemID values(@UserID, @Desc, @Completed)", conn, trans))
                    {
                        command.Parameters.AddWithValue("@UserID", item.UserID);
                        command.Parameters.AddWithValue("@Desc", item.Description.Trim());
                        command.Parameters.AddWithValue("@Completed", item.Completed);

                        // We execute the command with the ExecuteScalar method, which will return to
                        // us the requested auto-generated ItemID.
                        string itemID = command.ExecuteScalar().ToString();
                        SetStatus(Created);
                        trans.Commit();
                        return itemID;
                    }
                }
            }
        }

        public void MarkCompleted(string itemID)
        {
            int itemNum;
            if (!Int32.TryParse(itemID, out itemNum))
            {
                SetStatus(Forbidden);
                return;
            }

            using (SqlConnection conn = new SqlConnection(ToDoDB))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    // In this case the command is an update.
                    using (SqlCommand command = new SqlCommand("update Items set Completed=@True where ItemID=@ItemID", conn, trans))
                    {
                        command.Parameters.AddWithValue("@True", true);
                        command.Parameters.AddWithValue("@ItemID", itemNum);

                        // We pay attention to the number of rows modified.  If no rows were modified,
                        // we know that there was no item with the given itemNum, and we report an error.
                        if (command.ExecuteNonQuery() == 0)
                        {
                            SetStatus(Forbidden);
                        }
                        else
                        {
                            SetStatus(OK);
                        }
                        trans.Commit();
                    }
                }
            }
        }

        public void DeleteItem(string itemID)
        {
            int itemNum;
            if (!Int32.TryParse(itemID, out itemNum))
            {
                SetStatus(Forbidden);
                return;
            }

            using (SqlConnection conn = new SqlConnection(ToDoDB))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    // Here we're doing a delete command.
                    using (SqlCommand command = new SqlCommand("delete from Items where ItemId = @ItemID", conn, trans))
                    {
                        command.Parameters.AddWithValue("@ItemID", itemNum);
                        if (command.ExecuteNonQuery() == 0)
                        {
                            SetStatus(Forbidden);
                        }
                        else
                        {
                            SetStatus(OK);
                        }
                        trans.Commit();
                    }
                }
            }
        }

        public IList<ToDoItem> GetAllItems(bool completedOnly, string userID)
        {
            using (SqlConnection conn = new SqlConnection(ToDoDB))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    if (userID != null)
                    {
                        // This is another select query, done to validate the userID
                        using (SqlCommand cmd = new SqlCommand("select UserID from Users where UserID = @UserID", conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@UserID", userID);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (!reader.HasRows)
                                {
                                    SetStatus(Forbidden);
                                    trans.Commit();
                                    return null;
                                }
                            }
                        }
                    }

                    // Notice that we have to work a bit to construct the proper query, since it depends on what
                    // options the user specified.
                    String query = "select Description, Completed, ItemID from Items, Users where Items.UserID = Users.UserID";
                    if (completedOnly)
                    {
                        query += " and Completed = 1";
                    }
                    if (userID != null)
                    {
                        query += " and Items.UserID = @UserID";
                    }

                    using (SqlCommand command = new SqlCommand(query, conn, trans))
                    {
                        if (userID != null)
                        {
                            command.Parameters.AddWithValue("@UserID", userID);
                        }

                        // We are going to be creating some ToDoItem objects and returning them in
                        // this list.
                        List<ToDoItem> result = new List<ToDoItem>();

                        // As with all queries, we use the ExecuteReader method:
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // This time we care about the rows being retuned.  The Read
                            // method lets us iterate through them one at a time.
                            while (reader.Read())
                            {
                                // Notice how we extract the values from each row by column name.
                                result.Add(new ToDoItem
                                {
                                    Description = (string)reader["Description"],
                                    Completed = (bool)reader["Completed"],
                                    ItemID = reader["ItemID"].ToString()
                                });
                            }
                        }
                        SetStatus(OK);
                        trans.Commit();
                        return result;
                    }
                }
            }
        }
    }
}

