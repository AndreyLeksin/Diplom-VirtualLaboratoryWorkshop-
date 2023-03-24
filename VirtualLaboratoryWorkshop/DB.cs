﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace VirtualLaboratoryWorkshop
{
    class DB
    {
        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-D8HTPE7;Initial Catalog=VirtualLaboratoryWorkshop;Integrated Security=True");

        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public SqlConnection getconnection()
        {
            return connection;
        }
    }
}
