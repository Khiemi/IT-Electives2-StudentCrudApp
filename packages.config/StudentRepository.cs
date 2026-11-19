using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentCrudApp
{
    public class StudentRepository
    {
        private const string ConnectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=SchoolDb;Integrated Security=True;TrustServerCertificate=True;";

        public int Insert(string id, string name, string course)
        {
            const string sql = @"
                INSERT INTO Students (StudentId, Name, Course)
                VALUES (@StudentId, @Name, @Course);";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@StudentId", SqlDbType.NVarChar, 20).Value = id;
                command.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = name;
                command.Parameters.Add("@Course", SqlDbType.NVarChar, 50).Value = course;

                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        public DataTable GetAll()
        {
            const string sql = "SELECT StudentId, Name, Course FROM Students;";
            DataTable table = new DataTable();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    table.Load(reader);
                }
            }

            return table;
        }

        public DataTable Search(string keyword)
        {
            DataTable table = new DataTable();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand(@"
                SELECT StudentId, Name, Course
                FROM Students
                WHERE StudentId LIKE @Keyword
                   OR Name LIKE @Keyword
                   OR Course LIKE @Keyword;", connection))
            {
                command.Parameters.Add("@Keyword", SqlDbType.NVarChar, 100).Value =
                    "%" + keyword.Trim() + "%";

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    table.Load(reader);
                }
            }

            return table;
        }

        public int Update(string id, string name, string course)
        {
            const string sql = @"
                UPDATE Students
                SET Name = @Name, Course = @Course
                WHERE StudentId = @StudentId;";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@StudentId", SqlDbType.NVarChar, 20).Value = id;
                command.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = name;
                command.Parameters.Add("@Course", SqlDbType.NVarChar, 50).Value = course;

                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        public int Delete(string id)
        {
            const string sql = "DELETE FROM Students WHERE StudentId = @StudentId;";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@StudentId", SqlDbType.NVarChar, 20).Value = id;

                connection.Open();
                return command.ExecuteNonQuery();
            }
        }
    }
}