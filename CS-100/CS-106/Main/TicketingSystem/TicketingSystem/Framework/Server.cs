using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace TicketingSystem.Framework
{
    /// <summary>
    /// server is responsible for any backend related code for dealing with the SQL server
    /// </summary>
    public class Server
    {
        // the ticket data source
        internal const string SOURCE_TICKET = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Server\Tickets.mdf;Integrated Security=True";
        // the user data source
        internal const string SOURCE_USERS  = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Server\Users.mdf;Integrated Security=True";

        /// <summary>
        /// hashes a string into md5 hash
        /// </summary>
        /// <param name="nonHashString">the non hashed string to hash</param>
        /// <returns></returns>
        internal static string HashString(string nonHashString)
        {
            byte[] tmpSource;
            byte[] tmpHash;
            tmpSource = ASCIIEncoding.ASCII.GetBytes(nonHashString);
            tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            StringBuilder sOutput = new StringBuilder(tmpHash.Length);
            for (int i = 0; i < tmpHash.Length; i++)
            {
                sOutput.Append(tmpHash[i].ToString("X2"));
            }
            return sOutput.ToString();
        }

        /// <summary>
        /// connects to the data base
        /// </summary>
        /// <param name="connectionString">the database to connect to</param>
        /// <returns>sql connection to the database</returns>
        internal static SqlConnection GetConnection(string connectionString)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                return connection;
            }
            catch (Exception e)
            {
                Debug.LogWarning("Unable to connect to database - " + e.Message);
                MessageBox.Show("Unable to connect to database!\nPlease try again...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return GetConnection(connectionString);
            }
        }

        /// <summary>
        /// closes a connection to a server
        /// </summary>
        /// <param name="sdr">reader to close</param>
        /// <param name="sc">command to null</param>
        /// <param name="connection">connection to close</param>
        internal static void CloseConnection(SqlDataReader sdr, SqlCommand sc, SqlConnection connection)
        {
            sdr.Close();  //  CLOSES THE READER
            sc.Dispose();  //  NULLS THE COMMAND
            connection.Close(); //  CLOSES OPEN CONNECTION TO SQL DATABASE
        }

        /// <summary>
        /// closes a connection to a server
        /// </summary>
        /// <param name="connection">connection to close</param>
        internal static void CloseConnection(SqlConnection connection)
        {
            connection.Close(); //  CLOSES OPEN CONNECTION TO SQL DATABASE
        }
    }
}
