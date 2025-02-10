using System.Data;
using Microsoft.Data.SqlClient;

namespace NetCoreLinqToSqlInjection.Repositories
{
    public class RepositoryDoctoresSQLServer
    {
        private DataTable tablaDoctores;
        private SqlConnection _connection;
        private SqlCommand _command;

        public RepositoryDoctoresSQLServer()
        {
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Encrypt=True;Trust Server Certificate=True";
            string sql = "select * from DOCTOR";
            SqlDataAdapter ad = new SqlDataAdapter(sql, connectionString);
            this.tablaDoctores = new DataTable();
            ad.Fill(this.tablaDoctores);

            this._connection = new SqlConnection(connectionString);
            this._command = new SqlCommand();
            this._command.Connection = this._connection;
        }
    }
}
