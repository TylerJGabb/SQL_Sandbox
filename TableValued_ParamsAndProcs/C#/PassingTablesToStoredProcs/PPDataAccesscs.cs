using System;
using System.Collections;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;

namespace PolyPrintUtilities
{
	/// <summary>
	/// Provides static calls for access to Poly Print Database
	/// All calls to database should be directed through a method here.
	/// 
	/// MAKE SURE TO CONFIGURE CONNECTION STRING AT END OF THIS FILE.
	/// </summary>
	public class PPDataAccess
	{

        public SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

		//--------------------------------------------------------------------------
		// PPDataAccess
		//--------------------------------------------------------------------------
		// Default contstuctor. Not currently used
		//--------------------------------------------------------------------------
		public PPDataAccess()
		{
		}

		//--------------------------------------------------------------------------
		//  enum PPServerType
		//--------------------------------------------------------------------------
		// Identifies Develoment or Release environment
		//--------------------------------------------------------------------------
		public enum PPServerType {stProductionServer, stDevelopServer}
		
		
		//--------------------------------------------------------------------------
		// ServerType
		//--------------------------------------------------------------------------
		// public - static
		// Used to build connection string.
		// Expected values are "Ferrari" (Default)
		//                     "Server2003"  (Development)
		//					   "?????"   (Alternate Development)
		//--------------------------------------------------------------------------
		public static PPServerType ServerType
		{
			get {return serverType;}
			set {serverType = value;
				if (serverType == PPServerType.stProductionServer)
					serverName = "Ferrari";
				else
					serverName = "Server2003";			
				}
		}


		//--------------------------------------------------------------------------
		// ExecuteDataReader (overloaded)
		//--------------------------------------------------------------------------
		// Returns SqlDataReader containing results for the given SQL Select statement
		// Uses Default connection string
		// CALLER MUST CLOSE CONNECTION! 
		//--------------------------------------------------------------------------
		public static SqlDataReader ExecuteDataReader(String SQL)
		{
			SqlConnection con  = new SqlConnection(ConnectionString);
			SqlCommand command = new SqlCommand(SQL,con);
			con.Open();	
			SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
			return dataReader;			
		}

		//--------------------------------------------------------------------------
		// ExecuteDataReader (overloaded)
		//--------------------------------------------------------------------------
		// Returns SqlDataReader containing results for the given strored procedure.
		// Takes a single Parameter name and a single integer parameter value
		// Uses Default connection string
		// CALLER MUST CLOSE CONNECTION! 
		//--------------------------------------------------------------------------
		public static SqlDataReader ExecuteDataReader(string procName, string paramName, int paramValue)
		{
			SqlConnection con =new SqlConnection(ConnectionString);
			SqlCommand command = new SqlCommand(procName, con);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.AddWithValue(paramName, paramValue);
			con.Open();	
			SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
			return dataReader;
		}

        //--------------------------------------------------------------------------
        // ExecuteDataReader (overloaded)
        //--------------------------------------------------------------------------
        // Returns SqlDataReader containing results for the given strored procedure.
        // Takes a single Parameter name and a single string parameter value
        // Uses Default connection string
        // CALLER MUST CLOSE CONNECTION! 
        //--------------------------------------------------------------------------
        public static SqlDataReader ExecuteDataReader(string procName, string paramName, string paramValue)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand command = new SqlCommand(procName, con);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue(paramName, paramValue);
            con.Open();
            SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return dataReader;
        }

        //--------------------------------------------------------------------------
        // ExecuteDataReader (overloaded)
        //--------------------------------------------------------------------------
        // Returns SqlDataReader containing results for the given strored procedure.
        // Takes a Procedure Name and SqlParameter Array as arguments
        // Uses Default connection string
        // CALLER MUST CLOSE CONNECTION! 
        //--------------------------------------------------------------------------
        public static SqlDataReader ExecuteDataReader(string procName, SqlParameter[] parameters)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand command = new SqlCommand(procName, con);
            command.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    command.Parameters.Add(parameters[i]);
                }
            }
            con.Open();
            SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return dataReader;
        }


		//--------------------------------------------------------------------------
		// ExecuteNonQuery (overloaded)
		//--------------------------------------------------------------------------
		// Executes the given SQL statement which is not expected to return a result set.
		// eg. Update, Insert, Delete
		// Uses Default connection string
		// Return true on success, false on failure.
		//--------------------------------------------------------------------------	
		public static bool ExecuteNonQuery(string SQL)
		{
			try
			{
				SqlConnection con  = new SqlConnection(ConnectionString);
				SqlCommand command = new SqlCommand(SQL,con);
				con.Open();	
				command.ExecuteNonQuery();
				con.Close();
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message); // TODO: Development only
				return false;
			}
		}


		//--------------------------------------------------------------------------
		// ExecuteNonQuery (overloaded)
		//--------------------------------------------------------------------------
		// Executes the given stored procedure (by name) and paramater list, which 
		// is not expected to return a result set.
		// eg. Update, Insert, Delete.
		// Uses Default connection string
		// Does not return results (TODO?)
		//--------------------------------------------------------------------------	
		public static void ExecuteNonQuery(string procName, SqlParameter[] parameters) 
		{
			SqlConnection con  = new SqlConnection(ConnectionString);
			SqlCommand command = new SqlCommand(procName, con);
			con.Open();
			command.CommandType = CommandType.StoredProcedure;
			if(parameters != null )
			{
           	for( int i = 0; i < parameters.Length; i++)
				{
				command.Parameters.Add(parameters[i]);
        		}
			}
			try
			{
				command.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message); // todo:
			}
			finally
			{
				con.Close();
			}

		}


		//--------------------------------------------------------------------------
		// ExecuteInteger
		//--------------------------------------------------------------------------
		// Returns single integer result for the given SQL Select query.
		// Uses Default connection string
		// returns 0 on any exception. 
		//--------------------------------------------------------------------------
		public static int ExecuteInteger(string SQL)
		{
			try
			{
				SqlConnection con  = new SqlConnection(ConnectionString);
				SqlCommand command = new SqlCommand(SQL,con);
				con.Open();	
				SqlDataReader dataReader;
				dataReader =  command.ExecuteReader(CommandBehavior.CloseConnection);
				try
				{
					dataReader.Read();				
					return  dataReader.GetInt32(0);
				}
				finally
				{
					dataReader.Close();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine (e.Message); // todo:
				return 0;
			}
		}


        //--------------------------------------------------------------------------
        // ExecuteInteger
        //--------------------------------------------------------------------------
        // Returns single integer result for the given SQL Select query.
        // Uses Default connection string
        // returns 0 on any exception. 
        //--------------------------------------------------------------------------
        public static int ExecuteInteger(string SQL, SqlParameter[] parameters)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConnectionString);
                SqlCommand command = new SqlCommand(SQL, con);

                if (parameters != null)
                {
                    foreach (SqlParameter parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }


                con.Open();
                SqlDataReader dataReader;
                dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                try
                {
                    dataReader.Read();
                    return dataReader.GetInt32(0);
                }
                finally
                {
                    dataReader.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); // todo:
                return 0;
            }
        }
		//--------------------------------------------------------------------------
		// ExecuteString
		//--------------------------------------------------------------------------
		// Returns single string result for the given SQL Select query.
		// Uses Default connection string
		// returns Empty string on any exception. 
		//--------------------------------------------------------------------------
		public static string ExecuteString(string SQL)
		{
			try
			{
				SqlConnection con  = new SqlConnection(ConnectionString);
				SqlCommand command = new SqlCommand(SQL,con);
				con.Open();	
				SqlDataReader dataReader;
				dataReader =  command.ExecuteReader(CommandBehavior.CloseConnection);
				try
				{
					dataReader.Read();				
					return  dataReader.GetString(0);
				}
				finally
				{
					dataReader.Close();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine (e.Message); // todo:
				return string.Empty;
			}
		}

		//--------------------------------------------------------------------------
		// ExecuteDataSet (overloaded)
		//--------------------------------------------------------------------------
		// Returns DataSet containing single Table as result for the given SQL Select query.
		// Uses Default connection string
		//--------------------------------------------------------------------------
		public static DataSet ExecuteDataSet(String SQL)
		{
			SqlConnection con = new SqlConnection(ConnectionString);
			SqlDataAdapter ad = new SqlDataAdapter(SQL,con);
			con.Open();	
			DataSet ds=new DataSet();
			try
			{
				ad.Fill(ds,"table"); //May be refered to as Tables[0] by caller
			}
			catch (Exception e)
			{
				Console.WriteLine (e.Message); // todo:
				throw; // TOOD: Development
			}
			finally
			{	
				con.Close();
			}
			return ds;
		}


		//--------------------------------------------------------------------------
		// ExecuteDataSet (overloaded)
		//--------------------------------------------------------------------------
		// Returns DataSet containing single Table as result for the given SQL Select query.
		// with ArrayList of parameters.
		// Uses Default connection string
		//--------------------------------------------------------------------------
		public static DataSet ExecuteDataSet(String SQL, ArrayList parameters)
		{
			SqlConnection con = new SqlConnection(ConnectionString);
			SqlCommand command = new SqlCommand(SQL, con);
			if(parameters != null )
			{
				foreach (SqlParameter parameter in parameters )
				{
					command.Parameters.Add(parameter);
				}
			}
			SqlDataAdapter ad = new SqlDataAdapter(command);
			con.Open();	
			DataSet ds=new DataSet();
			try
			{
				ad.Fill(ds,"table"); //May be refered to as Tables[0] by caller
			}
			catch
			{
				throw; // TODO:
			}
			finally
			{	
				con.Close();
			}
			return ds;
		}


        //--------------------------------------------------------------------------
        // ExecuteDataSetProc
        //--------------------------------------------------------------------------
        // Returns DataSet containing single Table as result for the stored Proc
        // Uses Default connection string
        //--------------------------------------------------------------------------
        public static DataSet ExecuteDataSetProc(string procName, SqlParameter[] parameters) 
        {
			SqlConnection con  = new SqlConnection(ConnectionString);
			SqlCommand command = new SqlCommand(procName, con);
            SqlDataAdapter ad  = new SqlDataAdapter(command);
            con.Open();
			command.CommandType = CommandType.StoredProcedure;
			if(parameters != null )
			{
				for( int i = 0; i < parameters.Length; i++)
				{
					command.Parameters.Add(parameters[i]);
				}
			}


            DataSet ds = new DataSet();
            try
            {
                ad.Fill(ds, "table"); //May be refered to as Tables[0] by caller
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); // todo:
                throw; // TOOD: Development
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        private static string ReadServerName()
        {
            XmlTextReader xmlTextReader = new XmlTextReader("connection.xml");
            while (xmlTextReader.Read() )
            {
                if (xmlTextReader.Name == "server")
                    for (int i = 0; i < xmlTextReader.AttributeCount; i++)
                    {
                        xmlTextReader.MoveToAttribute(i);
                        if (xmlTextReader.Name == "name")
                            return xmlTextReader.Value;
                    }
            }
            return "";
        }
											
					  						
/****************************************************/
		public static string ConnectionString
		{
            get
            {
                return _connectionString;
            }
        }
        private static string serverName = "";
        private static PPServerType serverType = PPServerType.stProductionServer;

        private static string PRODUCTION = "NOPE";
        private static string MUSTANG = "NOPE";
        private static string TYLERSDB = "NOPE";

        private static string _connectionString = null;

        public static void SetToProduction()
        {
            _connectionString = PRODUCTION;
        }

        public static void SetToMustang()
        {
            _connectionString = MUSTANG;
        } 

        public static void SetToTylersDB()
        {
            _connectionString = TYLERSDB;
        }
    }



        
    

}


