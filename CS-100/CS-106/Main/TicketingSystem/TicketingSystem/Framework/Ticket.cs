using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace TicketingSystem.Framework
{
    /// <summary>
    /// ticket for the ticketing system
    /// </summary>
    public class Ticket
    {
        // properties for the instance of a ticket
        #region properties
        private int id;
        private bool status; // false or 0 means closed - true or 1 means open
        private string callerID;
        private string creatorID;
        private string title;
        private int urgency;
        private RESOLVEREASON resolveReason;
        private DateTime created;
        private DateTime updated;
        private List<string> comments;
        #endregion
        #region Instance-Get
        public int GetID()
        {
            return id;
        }
        public bool GetStatus()
        {
            return status;
        }
        public string GetCallerID()
        {
            return callerID;
        }
        public string GetCreatorID()
        {
            return creatorID;
        }
        public string GetTitle()
        {
            return title;
        }
        public int GetUrgency()
        {
            return urgency;
        }
        public RESOLVEREASON GetResolveReason()
        {
            return resolveReason;
        }
        public DateTime GetCreatedTime()
        {
            return created;
        }
        public DateTime GetUpdatedTime()
        {
            return updated;
        }
        public List<string> GetComments()
        {
            return comments;
        }
        #endregion

        /// <summary>
        /// creates an instance of ticket with a known ID
        /// </summary>
        /// <param name="_id">known id</param>
        public Ticket(int _id, out bool worked)
        {
            this.id = _id;
            worked = GetTicketInfo(id);
        }

        /// <summary>
        /// constructor *****NOT TO BE USED*****
        /// </summary>
        private Ticket()
        {
            // does nothing - not to be used except by static functions
        }

        /// <summary>
        /// creates a new ticket to add to the data base
        /// </summary>
        /// <param name="callerID"></param>
        /// <param name="creatorID"></param>
        /// <param name="title"></param>
        /// <param name="urgency"></param>
        /// <param name="created"></param>
        /// <returns>ticket that is created</returns>
        public static Ticket CreateNew(string callerID, string creatorID, string title, int urgency, DateTime created)
        {
            try
            {
                Ticket ticket = new Ticket(); // create blank ticket
                ticket.id = NewID(); // get a new id for the ticket
                if(ticket.id < 0)
                {
                    throw new Exception("Invalid Ticket Number");
                } // make sure id is valid

                ticket.status = true; // open by default
                ticket.callerID = callerID;
                ticket.creatorID = creatorID;
                ticket.title = title;
                ticket.urgency = urgency;
                ticket.created = created;
                ticket.updated = created;
                ticket.resolveReason = RESOLVEREASON.None;
                ticket.comments = new List<string>();

                AddNewTicket(ticket); // add ticket into the data base
                return ticket; // returns the complete ticket
            }
            catch (Exception e) // catch any erros and return nothing
            {
                Debug.LogWarning("Operation Unsuccessful - " + e.Message);
                MessageBox.Show("Operation was not successful!\nPlease try again...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        /// <summary>
        /// gets a new id from the database
        /// </summary>
        /// <returns>new id</returns>
        private static int NewID()
        {
            try
            {
                SqlConnection connection = Server.GetConnection(Server.SOURCE_TICKET);
                string tableName = "AllTickets"; 
                string countQuery = $"SELECT COUNT(*) FROM {tableName}";
                SqlCommand command = new SqlCommand(countQuery, connection);
                int rowCount = (int)command.ExecuteScalar();
                Server.CloseConnection(connection);
                return rowCount + 1;
            }
            catch (Exception e)
            {
                Debug.LogWarning("Operation Unsuccessful - " + e.Message);
                //MessageBox.Show("Operation was not successful!\nPlease try again...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }
        }

        /// <summary>
        /// adds the ticket to the database
        /// </summary>
        /// <param name="t"></param>
        private static void AddNewTicket(Ticket t)
        {
            try
            {
                const string SPACE = ", ";

                #region format
                // format reason and comments
                string reason = "'";
                string commentsAll = "'";
                foreach (string s in t.comments)
                {
                    commentsAll += (s + "♦");
                }
                if (commentsAll.EndsWith("♦"))
                {
                    commentsAll = commentsAll.Remove(commentsAll.Length - 1, 1); // remove last symbol
                }
                commentsAll += "'";
                if (commentsAll == "''")
                {
                    commentsAll = "NULL";
                }

                if (t.resolveReason == RESOLVEREASON.None)
                {
                    reason = "NULL";
                }
                else
                {
                    reason = ((int)t.resolveReason).ToString();
                    reason += "'";
                }
                #endregion
                #region createCommand
                string commandText = "INSERT INTO AllTickets VALUES(";
                commandText += t.id.ToString();
                commandText += SPACE;
                commandText += "'" + t.status.ToString() + "'";
                commandText += SPACE;
                commandText += "'" + t.callerID + "'";
                commandText += SPACE;
                commandText += "'" + t.creatorID + "'";
                commandText += SPACE;
                commandText += "@tTitle";
                commandText += SPACE;
                commandText += "'" + t.urgency.ToString() + "'";
                commandText += SPACE;
                commandText += reason;
                commandText += SPACE;
                commandText += "'" + t.created.ToString() + "'";
                commandText += SPACE;
                commandText += "'" + t.updated.ToString() + "'"; ;
                commandText += SPACE;
                commandText += "@tCommentsAll";
                commandText += ");";
                #endregion
                Debug.Log(commandText);

                SqlConnection connection = Server.GetConnection(Server.SOURCE_TICKET);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.InsertCommand = new SqlCommand(commandText, connection);
                adapter.InsertCommand.Parameters.AddWithValue("@tTitle", t.title);
                adapter.InsertCommand.Parameters.AddWithValue("@tCommentsAll", commentsAll);
                adapter.InsertCommand.ExecuteNonQuery();
                Server.CloseConnection(connection);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Operation Unsuccessful - " + e.Message);
                MessageBox.Show("Operation was not successful!\nPlease try again...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        /// <summary>
        /// gets the instances ticket info of the current 
        /// </summary>
        /// <param name="_id"></param>
        /// <returns>true if the ticket info was gained successfully else false</returns>
        private bool GetTicketInfo(int _id)
        {
            try
            {
                SqlConnection connection = Server.GetConnection(Server.SOURCE_TICKET);

                SqlDataReader sqlReader;                //  FILESTREAM / READER, MAKES THE DATA INDEXABLE
                SqlCommand command = new SqlCommand();  //  USED TO SPECIFY THE SQL QUERY
                command.Connection = connection;        //  SPECIFIES THE CONNECTION THAT THE COMMAND WILL BE USED IN
                command.CommandText = "SELECT * FROM AllTickets WHERE ID='" + _id + "';";
                sqlReader = command.ExecuteReader();    //  TAKES THE OUTPUT INTO THE READER

                if (sqlReader.HasRows)  //  USER FOUND WITH MATCHING CREDENTIALS
                {
                    while (sqlReader.Read())
                    {
                        id = sqlReader.GetInt32(0);
                        status = sqlReader.GetBoolean(1);
                        callerID = sqlReader.GetString(2);
                        creatorID = sqlReader.GetString(3);
                        title = sqlReader.GetString(4);
                        urgency = sqlReader.GetInt32(5);
                        try
                        {
                            resolveReason = (RESOLVEREASON)sqlReader.GetInt32(6);
                        }
                        catch (System.Data.SqlTypes.SqlNullValueException)
                        {
                            resolveReason = RESOLVEREASON.None;
                        }
                        created = sqlReader.GetDateTime(7);
                        updated = sqlReader.GetDateTime(8);
                        try
                        {
                            comments = (sqlReader.GetString(9)).Split('♦').ToList();
                        }
                        catch (System.Data.SqlTypes.SqlNullValueException)
                        {
                            comments = new List<string>();
                        }

                        Server.CloseConnection(sqlReader, command, connection);
                        return true;
                    }
                }
                else                    //  INCORRECT / INVALID CREDENTIALS
                {
                    Server.CloseConnection(sqlReader, command, connection);
                    return false;
                }
                return false;
            }
            catch (Exception e)
            {
                Debug.LogWarning("Operation Unsuccessful - " + e.Message);
                MessageBox.Show("Operation was not successful!\nPlease try again...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        /// adds a comment to the ticket
        /// </summary>
        /// <param name="comment">comment to add</param>
        public void AddComment(string comment)
        {
            try
            {
                string amendedComment = "" + MainWindow.user.firstName + "¦" + MainWindow.user.lastName + "¦" + DateTime.Now.ToString() + "¦" + comment;
                comments.Add(amendedComment);
                amendedComment = string.Empty;
                foreach (string c in comments)
                {
                    amendedComment += c + '♦';
                }
                if (amendedComment.EndsWith("♦"))
                {
                    amendedComment = amendedComment.Remove(amendedComment.Length - 1, 1); // remove last symbol
                }

                using (SqlConnection connection = Server.GetConnection(Server.SOURCE_TICKET))
                {
                    //  FILESTREAM / WRITER, ALLOWS INSERTING / UPDATING ROWS IN SQL
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    string commandText = "UPDATE AllTickets SET COMMENTS=@comment WHERE ID='" + this.id + "';";
                    adapter.InsertCommand = new SqlCommand(commandText, connection);
                    adapter.InsertCommand.Parameters.AddWithValue("@comment", amendedComment);
                    adapter.InsertCommand.ExecuteNonQuery();
                    Server.CloseConnection(connection);
                }

                using (SqlConnection connection = Server.GetConnection(Server.SOURCE_TICKET))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    string commandText = "UPDATE AllTickets SET UPDATED='" + DateTime.Now.ToString() + "' WHERE ID='" + this.id + "';";
                    adapter.InsertCommand = new SqlCommand(commandText, connection);
                    adapter.InsertCommand.ExecuteNonQuery();
                    Server.CloseConnection(connection);
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("Operation Unsuccessful - " + e.Message);
                MessageBox.Show("Operation was not successful!\nPlease try again...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// changes the urgency of the ticket
        /// </summary>
        /// <param name="newUrgency"></param>
        public void ChangeUrgency(int newUrgency)
        {
            try
            {
                using (SqlConnection connection = Server.GetConnection(Server.SOURCE_TICKET))
                {
                    //  FILESTREAM / WRITER, ALLOWS INSERTING / UPDATING ROWS IN SQL
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    string commandText = "UPDATE AllTickets SET URGENCY=@urgency WHERE ID='" + this.id + "';";
                    adapter.InsertCommand = new SqlCommand(commandText, connection);
                    adapter.InsertCommand.Parameters.AddWithValue("@urgency", newUrgency);
                    adapter.InsertCommand.ExecuteNonQuery();
                    Server.CloseConnection(connection);
                }

                using (SqlConnection connection = Server.GetConnection(Server.SOURCE_TICKET))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    string commandText = "UPDATE AllTickets SET UPDATED='" + DateTime.Now.ToString() + "' WHERE ID='" + this.id + "';";
                    adapter.InsertCommand = new SqlCommand(commandText, connection);
                    adapter.InsertCommand.ExecuteNonQuery();
                    Server.CloseConnection(connection);
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("Operation Unsuccessful - " + e.Message);
                MessageBox.Show("Operation was not successful!\nPlease try again...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        /// <summary>
        /// gets a list of all the ticket ids
        /// </summary>
        /// <returns></returns>
        public static List<int> GetAllTicketIds()
        {
            try
            {
                SqlConnection connection = Server.GetConnection(Server.SOURCE_TICKET);
                SqlDataReader sqlReader;                    //  FILESTREAM / READER, MAKES THE DATA INDEXABLE
                SqlCommand command = new SqlCommand();      //  USED TO SPECIFY THE SQL QUERY

                command.Connection = connection;            //  SPECIFIES THE CONNECTION THAT THE COMMAND WILL BE USED IN
                command.CommandText = "SELECT * FROM AllTickets;";
                sqlReader = command.ExecuteReader();        //  TAKES THE OUTPUT INTO THE READER

                if (sqlReader.HasRows)                      //  USER FOUND WITH MATCHING CREDENTIALS
                {
                    List<int> ids = new List<int>();
                    while (sqlReader.Read())
                    {
                        int ID = sqlReader.GetInt32(0);         //  Sets this instance's ID to the the corresponding cell in the matching row
                        ids.Add(ID);
                    }
                    Server.CloseConnection(sqlReader, command, connection);
                    return ids;
                }
                else                    //  INCORRECT / INVALID CREDENTIALS
                {
                    Server.CloseConnection(sqlReader, command, connection);
                    return null;
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("Operation Unsuccessful - " + e.Message);
                MessageBox.Show("Operation was not successful!\nPlease try again...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            
        }

        /// <summary>
        /// gets a list of all the ticket ids based on the opa
        /// </summary>
        /// <param name="opa">open closed or all tickets of a user</param>
        /// <returns></returns>
        public static List<int> GetAllTicketIds(int opa)
        {
            try
            {
                User currentUser = MainWindow.user;
                string cmdAdd = "";
                if (opa == 1)
                {
                    cmdAdd = "AND Status='True'";
                }
                if (opa == 2)
                {
                    cmdAdd = "AND Status='False'";
                }

                SqlConnection connection = Server.GetConnection(Server.SOURCE_TICKET);
                SqlDataReader sqlReader;                    //  FILESTREAM / READER, MAKES THE DATA INDEXABLE
                SqlCommand command = new SqlCommand();      //  USED TO SPECIFY THE SQL QUERY

                command.Connection = connection;            //  SPECIFIES THE CONNECTION THAT THE COMMAND WILL BE USED IN
                command.CommandText = "SELECT * FROM AllTickets WHERE (CALLER='" + currentUser.ID.ToString() + "' OR CREATOR='" + currentUser.ID.ToString() + "')" + cmdAdd + ";";
                sqlReader = command.ExecuteReader();        //  TAKES THE OUTPUT INTO THE READER

                if (sqlReader.HasRows)                      //  USER FOUND WITH MATCHING CREDENTIALS
                {
                    List<int> ids = new List<int>();
                    while (sqlReader.Read())
                    {
                        int ID = sqlReader.GetInt32(0);         //  Sets this instance's ID to the the corresponding cell in the matching row
                        ids.Add(ID);
                    }
                    Server.CloseConnection(sqlReader, command, connection);
                    return ids;
                }
                else                    //  INCORRECT / INVALID CREDENTIALS
                {
                    Server.CloseConnection(sqlReader, command, connection);
                    return null;
                }

            }
            catch (Exception e)
            {
                Debug.LogWarning("Operation Unsuccessful - " + e.Message);
                MessageBox.Show("Operation was not successful!\nPlease try again...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            
        }

        /// <summary>
        /// resolve/close reason of the ticket
        /// </summary>
        public enum RESOLVEREASON : int
        {
            None = 0,
            FIXED = 1,
            NORESOLUTION = 2,
            ADVISEGIVEN = 3,
            NOLONGERREQUIRED = 4,
            NILRESPONSE = 5,
            OTHER = 6
        }

        /// <summary>
        /// resolves a ticket
        /// </summary>
        /// <param name="reason">reason to resolve ticket</param>
        public void Resolve(int reason)
        {
            try
            {
                using (SqlConnection connection = Server.GetConnection(Server.SOURCE_TICKET))
                {
                    //  FILESTREAM / WRITER, ALLOWS INSERTING / UPDATING ROWS IN SQL
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    string commandText = "UPDATE AllTickets SET RESOLVEREASON=@reason WHERE ID='" + this.id + "';";
                    adapter.InsertCommand = new SqlCommand(commandText, connection);
                    adapter.InsertCommand.Parameters.AddWithValue("@reason", reason);
                    adapter.InsertCommand.ExecuteNonQuery();
                    Server.CloseConnection(connection);
                }

                ChangeStatus(false);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Operation Unsuccessful - " + e.Message);
                MessageBox.Show("Operation was not successful!\nPlease try again...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// changes the status of the ticket
        /// </summary>
        /// <param name="newStatus">open or closed</param>
        public void ChangeStatus(bool newStatus)
        {
            try
            {
                using (SqlConnection connection = Server.GetConnection(Server.SOURCE_TICKET))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    string commandText = "UPDATE AllTickets SET STATUS=@status WHERE ID='" + this.id + "';";
                    adapter.InsertCommand = new SqlCommand(commandText, connection);
                    adapter.InsertCommand.Parameters.AddWithValue("@status", newStatus);
                    adapter.InsertCommand.ExecuteNonQuery();
                    Server.CloseConnection(connection);

                    status = newStatus;
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("Operation Unsuccessful - " + e.Message);
                MessageBox.Show("Operation was not successful!\nPlease try again...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
