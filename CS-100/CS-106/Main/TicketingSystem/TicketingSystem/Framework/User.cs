using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Windows;

namespace TicketingSystem.Framework
{
    /// <summary>
    /// user is responsible for a user within the ticketing system
    /// </summary>
    public class User
    {
        #region instance-members
        public int ID;
        public int userType;
        public string password;
        public string email;
        public string firstName;
        public string lastName;
        #endregion
        /// <summary>
        /// gets a user based on their ID
        /// </summary>
        /// <param name="ID">the users ID</param>
        /// <returns></returns>
        public static User GetUserFromID(int ID)
        {
            try
            {
                User user = new User();

                SqlConnection connection = Server.GetConnection(Server.SOURCE_USERS);
                SqlDataReader sqlReader;                    //  FILESTREAM / READER, MAKES THE DATA INDEXABLE
                SqlCommand command = new SqlCommand();      //  USED TO SPECIFY THE SQL QUERY

                command.Connection = connection;            //  SPECIFIES THE CONNECTION THAT THE COMMAND WILL BE USED IN
                command.CommandText = "SELECT * FROM Users WHERE (ID='" + ID.ToString() + "');";
                sqlReader = command.ExecuteReader();        //  TAKES THE OUTPUT INTO THE READER

                if (sqlReader.HasRows) //  USER FOUND WITH MATCHING CREDENTIALS
                {
                    while (sqlReader.Read())
                    {
                        user.ID = sqlReader.GetInt32(0);         //  Sets this instance's ID to the the corresponding cell in the matching row
                        user.password = sqlReader.GetString(1);  //  Sets this instance's password to the the corresponding cell in the matching row
                        user.userType = sqlReader.GetInt32(2);   //  Sets this instance's usertype to the the corresponding cell in the matching row
                        user.email = sqlReader.GetString(3);     //  Sets this instance's email to the the corresponding cell in the matching row
                        user.firstName = sqlReader.GetString(4); //  Sets this instance's first name to the the corresponding cell in the matching row
                        user.lastName = sqlReader.GetString(5);  //  Sets this instance's last name to the the corresponding cell in the matching row

                        Server.CloseConnection(sqlReader, command, connection);
                        return user;
                    }
                }
                else //  INCORRECT / INVALID CREDENTIALS
                {
                    Server.CloseConnection(sqlReader, command, connection);
                    return null;
                }
                return null;
            }
            catch (Exception e)
            {
                Debug.LogWarning("Operation Unsuccessful - " + e.Message);
                MessageBox.Show("Operation was not successful!\nPlease try again...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            
        }

        /// <summary>
        /// logs a user in and gets their details from the server
        /// </summary>
        /// <param name="_ID">user id</param>
        /// <param name="_NonHashedPassword">users raw password</param>
        /// <returns></returns>
        public bool Login(string _ID, string _NonHashedPassword)
        {
            try
            {
                string _Password = Server.HashString(_NonHashedPassword);

                SqlConnection connection = Server.GetConnection(Server.SOURCE_USERS);
                SqlDataReader sqlReader;                    //  FILESTREAM / READER, MAKES THE DATA INDEXABLE
                SqlCommand command = new SqlCommand();      //  USED TO SPECIFY THE SQL QUERY

                command.Connection = connection;            //  SPECIFIES THE CONNECTION THAT THE COMMAND WILL BE USED IN
                command.CommandText = "SELECT * FROM Users WHERE Password=@password AND Email=@email;";
                command.Parameters.AddWithValue("@id", _ID); //  ADDS THE ID TO THE QUERY
                command.Parameters.AddWithValue("@email", _ID); //  ADDS THE ID TO THE QUERY
                command.Parameters.AddWithValue("@password", _Password); //  ADDS THE PASSWORD TO THE QUERY
                sqlReader = command.ExecuteReader();        //  TAKES THE OUTPUT INTO THE READER


                if (sqlReader.HasRows) //  USER FOUND WITH MATCHING CREDENTIALS
                {
                    while (sqlReader.Read())
                    {
                        ID = sqlReader.GetInt32(0);         //  Sets this instance's ID to the the corresponding cell in the matching row
                        password = sqlReader.GetString(1);  //  Sets this instance's password to the the corresponding cell in the matching row
                        userType = sqlReader.GetInt32(2);   //  Sets this instance's usertype to the the corresponding cell in the matching row
                        email = sqlReader.GetString(3);     //  Sets this instance's email to the the corresponding cell in the matching row
                        firstName = sqlReader.GetString(4); //  Sets this instance's first name to the the corresponding cell in the matching row
                        lastName = sqlReader.GetString(5);  //  Sets this instance's last name to the the corresponding cell in the matching row

                        Server.CloseConnection(sqlReader, command, connection);
                        return true;
                    }
                }
                else //  INCORRECT / INVALID CREDENTIALS
                {
                    sqlReader.Close();
                    command.CommandText = "SELECT * FROM Users WHERE Password=@password AND ID=@id;";
                    sqlReader = command.ExecuteReader();        //  TAKES THE OUTPUT INTO THE READER

                    if (sqlReader.HasRows) //  USER FOUND WITH MATCHING CREDENTIALS
                    {
                        while (sqlReader.Read())
                        {
                            ID = sqlReader.GetInt32(0);         //  Sets this instance's ID to the the corresponding cell in the matching row
                            password = sqlReader.GetString(1);  //  Sets this instance's password to the the corresponding cell in the matching row
                            userType = sqlReader.GetInt32(2);   //  Sets this instance's usertype to the the corresponding cell in the matching row
                            email = sqlReader.GetString(3);     //  Sets this instance's email to the the corresponding cell in the matching row
                            firstName = sqlReader.GetString(4); //  Sets this instance's first name to the the corresponding cell in the matching row
                            lastName = sqlReader.GetString(5);  //  Sets this instance's last name to the the corresponding cell in the matching row

                            Server.CloseConnection(sqlReader, command, connection);
                            return true;
                        }
                    }
                    else
                    {
                        Server.CloseConnection(sqlReader, command, connection);
                        return false;
                    }
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
        /// changes the password of the current instance of the user if the old password matches the current password
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public bool ChangePassword(string oldPassword, string newPassword)
        {
            try
            {
                oldPassword = Server.HashString(oldPassword);
                newPassword = Server.HashString(newPassword);
                //  CHECKS IF THE NEW PASSWORDS MATCHES, AND IF THE OLD PASSWORD MATCHES THEIR CURRENT PASSWORD
                if (oldPassword == password)
                {
                    SqlConnection connection = Server.GetConnection(Server.SOURCE_USERS);
                    SqlDataAdapter adapter = new SqlDataAdapter();

                    string commandText = "UPDATE Users SET Password=@password WHERE ID='" + ID + "';";
                    adapter.InsertCommand = new SqlCommand(commandText, connection);
                    adapter.InsertCommand.Parameters.AddWithValue("@password", newPassword);
                    adapter.InsertCommand.ExecuteNonQuery();

                    Server.CloseConnection(connection);
                    password = newPassword;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("Operation Unsuccessful - " + e.Message);
                MessageBox.Show("Operation was not successful!\nPlease try again...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
        }

        /// <summary>
        /// changes the password of the current instance of the user if the old password matches the current password
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public bool AdminChangePassword(string newPassword)
        {
            try
            {
                SqlConnection connection = Server.GetConnection(Server.SOURCE_USERS);
                SqlDataAdapter adapter = new SqlDataAdapter();

                string commandText = "UPDATE Users SET Password=@password WHERE ID='" + ID + "';";
                adapter.InsertCommand = new SqlCommand(commandText, connection);
                adapter.InsertCommand.Parameters.AddWithValue("@password", newPassword);
                adapter.InsertCommand.ExecuteNonQuery();

                Server.CloseConnection(connection);
                password = newPassword;
                return true;
            }
            catch (Exception e)
            {
                Debug.LogWarning("Operation Unsuccessful - " + e.Message);
                MessageBox.Show("Operation was not successful!\nPlease try again...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }

        /// <summary>
        /// changes the email of the current instance of the user
        /// </summary>
        /// <param name="newEmail"></param>
        public void ChangeEmail(string newEmail)
        {
            try
            {
                //  FILESTREAM / WRITER, ALLOWS INSERTING / UPDATING ROWS IN SQL
                SqlConnection connection = Server.GetConnection(Server.SOURCE_USERS);
                SqlDataAdapter adapter = new SqlDataAdapter();
                string commandText = "UPDATE Users SET Email=@email WHERE ID='" + ID + "';";
                adapter.InsertCommand = new SqlCommand(commandText, connection);
                adapter.InsertCommand.Parameters.AddWithValue("@email", newEmail);
                adapter.InsertCommand.ExecuteNonQuery();
                Server.CloseConnection(connection);
                email = newEmail;
            }
            catch (Exception e)
            {
                Debug.LogWarning("Operation Unsuccessful - " + e.Message);
                MessageBox.Show("Operation was not successful!\nPlease try again...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        /// <summary>
        /// changes the account type of the current instance of the user
        /// </summary>
        /// <param name="newAccountType"></param>
        public void ChangeAccountType(int newAccountType)
        {
            try
            {
                //  FILESTREAM / WRITER, ALLOWS INSERTING / UPDATING ROWS IN SQL
                SqlConnection connection = Server.GetConnection(Server.SOURCE_USERS);
                SqlDataAdapter adapter = new SqlDataAdapter();
                string commandText = "UPDATE Users SET UserType=@type WHERE ID='" + ID + "';";
                adapter.InsertCommand = new SqlCommand(commandText, connection);
                adapter.InsertCommand.Parameters.AddWithValue("@type", newAccountType);
                adapter.InsertCommand.ExecuteNonQuery();
                Server.CloseConnection(connection);
                this.userType = newAccountType;
            }
            catch (Exception e)
            {
                Debug.LogWarning("Operation Unsuccessful - " + e.Message);
                MessageBox.Show("Operation was not successful!\nPlease try again...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public void ChangeAccountName(string newFirstName, string newLastName)
        {
            try
            {
                //  FILESTREAM / WRITER, ALLOWS INSERTING / UPDATING ROWS IN SQL
                SqlConnection connection = Server.GetConnection(Server.SOURCE_USERS);
                SqlDataAdapter adapter = new SqlDataAdapter();
                string commandText = "UPDATE Users SET FirstName=@first, LastName=@last WHERE ID='" + ID + "';";
                adapter.InsertCommand = new SqlCommand(commandText, connection);
                adapter.InsertCommand.Parameters.AddWithValue("@first", newFirstName);
                adapter.InsertCommand.Parameters.AddWithValue("@last", newLastName);
                adapter.InsertCommand.ExecuteNonQuery();
                Server.CloseConnection(connection);
                this.firstName = newFirstName;
                this.lastName = newLastName;
            }
            catch (Exception e)
            {
                Debug.LogWarning("Operation Unsuccessful - " + e.Message);
                MessageBox.Show("Operation was not successful!\nPlease try again...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// gets a list of all the account ids in the system
        /// </summary>
        /// <returns></returns>
        public static List<int> GetAllAccountIds()
        {
            try
            {
                SqlConnection connection = Server.GetConnection(Server.SOURCE_USERS);
                SqlDataReader sqlReader;                    //  FILESTREAM / READER, MAKES THE DATA INDEXABLE
                SqlCommand command = new SqlCommand();      //  USED TO SPECIFY THE SQL QUERY

                command.Connection = connection;            //  SPECIFIES THE CONNECTION THAT THE COMMAND WILL BE USED IN
                command.CommandText = "SELECT * FROM Users;";
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
                else //  INCORRECT / INVALID CREDENTIALS
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
        /// gets the amount of active tickets a user has created or assigned to
        /// </summary>
        /// <returns>the acitve tickets assigned to a user</returns>
        public int GetActiveTicketsAmount(User u)
        {
            try
            {
                User currentUser = u;
                string cmdAdd = "AND Status='True'";
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
                    return ids.Count();
                }
                else                    //  INCORRECT / INVALID CREDENTIALS
                {
                    Server.CloseConnection(sqlReader, command, connection);
                    return 0;
                }

            }
            catch (Exception e)
            {
                Debug.LogWarning("Operation Unsuccessful - " + e.Message);
                MessageBox.Show("Operation was not successful!\nPlease try again...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
        }

        /// <summary>
        /// the user type which defines a users privileges
        /// </summary>
        public enum Type : int
        {
            User = 1,
            Tech = 2,
            Admin = 3,
            Test = 4
        }

        /// <summary>
        /// gets usertype as a string
        /// </summary>
        /// <param name="user">user to check</param>
        /// <returns></returns>
        public static string TypeToString(User user)
        {
            Type type = (Type)user.userType;
            switch (type)
            {
                case Type.User:
                    {
                        return "User";
                    }
                case Type.Tech:
                    {
                        return "Tech";
                    }
                case Type.Admin:
                    {
                        return "Admin";
                    }
                case Type.Test:
                    {
                        return "Test";
                    }
                default:
                    {
                        Debug.LogError("User type invalid for user " + user.ID.ToString());
                        return "Error - Unknown User Type";
                    }
            }
        }

        /// <summary>
        /// creates a new user
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="emailAddress"></param>
        /// <param name="accountType"></param>
        /// <param name="rawPassword"></param>
        /// <returns>the new user</returns>
        public static User CreateNew(string firstName, string lastName, string emailAddress, Type accountType, string rawPassword)
        {
            try
            {
                User user = new User();
                user.ID = NewID();
                if (user.ID < 0)
                {
                    throw new Exception("Invalid User Number");
                }

                user.firstName = firstName;
                user.lastName = lastName;
                user.email = emailAddress;
                user.userType = (int)accountType;
                user.password = Server.HashString(rawPassword);

                bool success = AddNewUser(user);
                if (!success)
                {
                    throw new Exception("Unable to create account...");
                }
                return user;
            }
            catch (Exception e)
            {
                Debug.LogWarning("Operation Unsuccessful - " + e.Message);
                MessageBox.Show("Operation was not successful!\nPlease try again...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        /// <summary>
        /// gets a new user id from the database
        /// </summary>
        /// <returns>new user id</returns>
        private static int NewID()
        {
            try
            {
                SqlConnection connection = Server.GetConnection(Server.SOURCE_USERS);
                string tableName = "Users";
                string countQuery = $"SELECT MAX(ID) FROM {tableName}";
                SqlCommand command = new SqlCommand(countQuery, connection);
                int rowCount = (int)command.ExecuteScalar();
                Server.CloseConnection(connection);
                return rowCount + 1;
            }
            catch (Exception e)
            {
                Debug.LogWarning("Operation Unsuccessful - " + e.Message);
                return -1;
            }
        }

        /// <summary>
        /// adds a new user to the database
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        private static bool AddNewUser(User u)
        {
            try
            {
                const string SPACE = ", ";
                string emailAddress = "";
                if (u.email == null || u.email == "")
                {
                    emailAddress = "\'None\'";
                }
                else
                {
                    emailAddress += u.email;
                }
                #region createCommand
                string commandText = "INSERT INTO Users VALUES(";
                commandText += u.ID.ToString();
                commandText += SPACE;
                commandText += "'" + u.password + "'";
                commandText += SPACE;
                commandText += "'" + u.userType.ToString() + "'";
                commandText += SPACE;
                commandText += "@tEmail";
                commandText += SPACE;
                commandText += "@tFirstName";
                commandText += SPACE;
                commandText += "@tLastName";
                commandText += ");";
                #endregion

                SqlConnection connection = Server.GetConnection(Server.SOURCE_USERS);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.InsertCommand = new SqlCommand(commandText, connection);
                adapter.InsertCommand.Parameters.AddWithValue("@tEmail", emailAddress);
                adapter.InsertCommand.Parameters.AddWithValue("@tFirstName", u.firstName);
                adapter.InsertCommand.Parameters.AddWithValue("@tLastName", u.lastName);
                adapter.InsertCommand.ExecuteNonQuery();
                Server.CloseConnection(connection);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogWarning("Operation Unsuccessful - " + e.Message);
                MessageBox.Show("Operation was not successful!\nPlease try again...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }


        /// <summary>
        /// validates and email
        /// </summary>
        /// <returns></returns>
        public static bool ValidateEmail(string email)
        {
            try
            {
                SqlConnection connection = Server.GetConnection(Server.SOURCE_USERS);
                string countQuery = "SELECT COUNT(*) FROM Users WHERE Email=@email";
                SqlCommand command = new SqlCommand(countQuery, connection);
                command.Parameters.AddWithValue("@Email", email);
                int rowCount = (int)command.ExecuteScalar();
                Server.CloseConnection(connection);

                if (rowCount > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("Operation Unsuccessful - " + e.Message);
                //MessageBox.Show("Operation was not successful!\nPlease try again...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        /// generates a random password using lowercase UPPERCASE and numbers###
        /// </summary>
        /// <returns>the random password</returns>
        public static string GenerateRandomPassword()
        {
            string temp = "";
            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                // 0 -> 60 (random)
                
                int num = rnd.Next(61); 

                if(num <= 9) // 0 -> 9
                {
                    temp += num.ToString();
                }
                else if (num >= 10 && num <= 35) // 10 -> 35
                {
                    num -= 10;
                    num += 65;
                    char c = (char)num;
                    temp += c;
                }
                else if(num >= 36) // 36 -> 60
                {
                    num -= 36;
                    num += 97;
                    char c = (char)num;
                    temp += c;
                }
                else
                {
                    temp += '#';
                }
            }

            return temp;


        }

        /// <summary>
        /// deletes an account from the database
        /// </summary>
        /// <param name="u">user to delete</param>
        public static void DeleteAccount(User u)
        {
            try
            {
                SqlConnection connection = Server.GetConnection(Server.SOURCE_USERS);
                string tableName = "Users";
                string countQuery = $"DELETE FROM {tableName} WHERE ID={u.ID};";
                SqlCommand command = new SqlCommand(countQuery, connection);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.InsertCommand = command;
                adapter.InsertCommand.ExecuteNonQuery();
                Server.CloseConnection(connection);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Operation Unsuccessful - " + e.Message);
                MessageBox.Show("Operation was not successful!\nPlease try again...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}