using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
        // The connection string to the DB
        private static string ToDoDB;

        static ToDo()
        {
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
            if (user.Name == null || user.Name.Trim().Length == 0)
            {
                SetStatus(Forbidden);
                return null;
            }

            using (SqlConnection conn = new SqlConnection(ToDoDB))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    // Insert a new user into the Users table
                    SqlCommand command = new SqlCommand("insert into Users (UserID, Name, Email) values(@UserID, @Name, @Email)", conn, trans);
                    string userID = Guid.NewGuid().ToString();
                    command.Parameters.AddWithValue("@UserToken", userID);
                    command.Parameters.AddWithValue("@Nickname", user.Name.Trim());
                    command.Parameters.AddWithValue("@Email", user.Email.Trim());
                    command.ExecuteNonQuery();

                    // Send back the new UserToken
                    SetStatus(Created);
                    return userID;
                }
                catch (Exception)
                {
                    trans.Rollback();
                    SetStatus(InternalServerError);
                    return null;
                }
                finally
                {
                    trans.Commit();
                }
            }
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
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    // Insert a new user into the Items table
                    SqlCommand command = new SqlCommand("select UserID from Users where UserID = @UserID", conn, trans);
                    command.Parameters.AddWithValue("@UserID", item.UserID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            SetStatus(Forbidden);
                            return null;
                        }
                    }

                    command = new SqlCommand("insert into Items (UserID, Description, Completed) output inserted.ItemID values(@UserID, @Desc, @Completed)", conn, trans);
                    command.Parameters.AddWithValue("@UserID", item.UserID);
                    command.Parameters.AddWithValue("@Desc", item.Description.Trim());
                    command.Parameters.AddWithValue("@Completed", item.Completed);
                    SetStatus(OK);
                    return command.ExecuteScalar().ToString();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    SetStatus(InternalServerError);
                    return null;
                }
                finally
                {
                    trans.Commit();
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
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    SqlCommand command = new SqlCommand("update Items set Completed=@True where ItemID=@ItemID", conn, trans);
                    command.Parameters.AddWithValue("@True", true);
                    command.Parameters.AddWithValue("@ItemID", itemNum);
                    if (command.ExecuteNonQuery() == 0)
                    {
                        SetStatus(Forbidden);
                    }
                    else
                    {
                        SetStatus(OK);
                    }
                }
                catch (Exception)
                {
                    trans.Rollback();
                    SetStatus(InternalServerError);
                }
                finally
                {
                    trans.Commit();
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
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    SqlCommand command = new SqlCommand("delete from Items where ItemId = @ItemID", conn, trans);
                    command.Parameters.AddWithValue("@ItemID", itemNum);
                    if (command.ExecuteNonQuery() == 0)
                    {
                        SetStatus(Forbidden);
                    }
                    else
                    {
                        SetStatus(OK);
                    }
                }
                catch (Exception)
                {
                    trans.Rollback();
                    SetStatus(InternalServerError);
                }
                finally
                {
                    trans.Commit();
                }
            }
        }


        public IList<ToDoItem> GetAllItems(bool completedOnly, string userID)
        {
            if (userID == null)
            {
                SetStatus(Forbidden);
                return null;
            }

            using (SqlConnection conn = new SqlConnection(ToDoDB))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    if (userID != null)
                    {
                        SqlCommand cmd = new SqlCommand("select UserID from Users where UserID = @UserID", conn, trans);
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                SetStatus(Forbidden);
                                return null;
                            }
                        }
                    }

                    String query = "select Description, Completed, ItemID from Items, Users where Items.UserID = Users.UserID";
                    if (completedOnly)
                    {
                        query += " and Completed = 1";
                    }
                    if (userID != null)
                    {
                        query += " and UserID = @UserID";
                    }

                    SqlCommand command = new SqlCommand(query, conn, trans);
                    command.Parameters.AddWithValue("@UserID", userID);

                    List<ToDoItem> result = new List<ToDoItem>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.HasRows)
                        {
                            result.Add(new ToDoItem
                            {
                                Description = (string)reader["Description"],
                                Completed = (bool)reader["Completed"],
                                ItemID = reader["ItemID"].ToString()
                            });
                        }
                    }
                    SetStatus(OK);
                    return result;
                }
                catch (Exception)
                {
                    trans.Rollback();
                    SetStatus(InternalServerError);
                    return null;
                }
                finally
                {
                    trans.Commit();
                }
            }
        }
    }
}

