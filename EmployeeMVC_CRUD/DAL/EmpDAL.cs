//ADO.Net


using MySqlConnector;
using System;
using System.Collections.Generic;
using EmployeeMVC_CRUD.Models;

namespace EmployeeMVC_CRUD.DAL
{
    public class EmpDAL
    {
        private static string ConnectionString = "server=localhost;uid=root;pwd=root@123;database=dotnet";

        public static List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();
            using (MySqlConnection con = new MySqlConnection(ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select * from StudentTable";
                cmd.CommandType = System.Data.CommandType.Text;

                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Gender = (string)reader["Gender"],
                        Class = (string)reader["Class"]
                    });
                }
                con.Close();
            }
            return students;
        }

        public static void InsertStudent(Student student)
        {
            using (MySqlConnection con = new MySqlConnection(ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "INSERT INTO StudentTable (Id, Name, Gender, Class) VALUES (@Id, @Name, @Gender, @Class)";
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@Id", student.Id);
                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Gender", student.Gender);
                cmd.Parameters.AddWithValue("@Class", student.Class);

                con.Open();
                int res = cmd.ExecuteNonQuery();
                Console.WriteLine($"{res} student inserted successfully.");
                con.Close();
            }
        }

        public static void DeleteStudent(int id)
        {
            using (MySqlConnection con = new MySqlConnection(ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "DELETE FROM StudentTable WHERE Id = @Id";
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                int res = cmd.ExecuteNonQuery();
                Console.WriteLine($"{res} student(s) deleted successfully.");
                con.Close();
            }
        }

        public static void UpdateStudent(Student student)
        {
            using (MySqlConnection con = new MySqlConnection(ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "UPDATE StudentTable SET Name = @Name, Gender = @Gender, Class = @Class WHERE Id = @Id";
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Gender", student.Gender);
                cmd.Parameters.AddWithValue("@Class", student.Class);
                cmd.Parameters.AddWithValue("@Id", student.Id);

                con.Open();
                int res = cmd.ExecuteNonQuery();
                Console.WriteLine($"{res} student(s) updated successfully.");
                con.Close();
            }
        }

        public static Student GetStudentById(int id)
        {
            Student student = null;
            using (MySqlConnection con = new MySqlConnection(ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM StudentTable WHERE Id = @Id";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    student = new Student
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Gender = (string)reader["Gender"],
                        Class = (string)reader["Class"]
                    };
                }
                con.Close();
            }
            return student;
        }
    }
}