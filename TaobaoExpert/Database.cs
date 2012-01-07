using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace TaobaoSpider.BLL
{
	public class Database
	{
		private static string connectionString;
		static Database()
		{
			connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
		}
		public static DbConnection GetConnection()
		{
			MySqlConnection conn = new MySqlConnection(connectionString);
			conn.Open();
			return conn;
		}
	}
}
