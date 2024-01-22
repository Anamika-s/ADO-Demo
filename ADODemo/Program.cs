// Step 1
using System.Data.SqlClient;

namespace ADODemo
{
    internal class Program
    {
        static SqlConnection connection = null;
        static SqlCommand command = null;

        static string GetCOnnectionString()
        {
            return @"data source=ANAMIKA\SQLSERVER;initial catalog=EmpDb;integrated security=true";
        }
        static SqlConnection GetConnection()
        {
            return new SqlConnection(GetCOnnectionString());
        }
        static int Menu()
        {
                Console.WriteLine("1. List of EMployees");
                Console.WriteLine("2. Add New Record");
                Console.WriteLine("3. Edit Record");
                Console.WriteLine("4. Delete Record");
                Console.WriteLine("5. Search Record");
                Console.WriteLine("6. Get Total EMployees");
                Console.WriteLine("ENter choice");
                int ch = Byte.Parse(Console.ReadLine());
                return ch;

         
            }
        
         static void Main(string[] args)
        {
            string choice = "y";
            while (choice == "y")
            {


                int ch = Menu();

                switch (ch)

                {
                    case 1:
                        {
                            connection = GetConnection();
                            command = new SqlCommand("Select * from Employee", connection);

                            SqlDataReader reader = null;
                            connection.Open();
                            reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine(reader[0] + " " + reader["name"]); ;
                                }

                            }
                            else
                                Console.WriteLine("There are no recirds");
                            reader.Close();
                            connection.Close();
                            break;
                        }
                    case 2:
                        {
                            connection = GetConnection();
                            command = new SqlCommand("insert into Employee values(1,'ajay','new delhi', '12/12/2005', 12999)", connection);
                            connection.Open();
                            command.ExecuteNonQuery();
                            connection.Close();
                            break;
                        }
                    case 3:
                        {
                            connection = GetConnection();
                            command = new SqlCommand("update Employee set address='old delhi' where id=1", connection);
                            connection.Open();
                            command.ExecuteNonQuery();
                            connection.Close();
                            break;
                        }
                    case 4:
                        {
                            connection = GetConnection();
                            command = new SqlCommand("delete Employee where id=1", connection);
                            connection.Open();
                            command.ExecuteNonQuery();
                            connection.Close();
                            break;
                        }

                    case 5:
                        {
                            connection = GetConnection();
                            command = new SqlCommand("select * from Employee where id=1", connection);
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {

                                Console.WriteLine(reader[0] + " " + reader["name"]); ;


                            }
                            else
                                Console.WriteLine("There are no recirds");
                            connection.Close();
                            break;
                        }
                    case 6:
                        {
                            connection = GetConnection();
                            command = new SqlCommand("Select count(*) from Employee", connection);
                            connection.Open();
                            int empCount = (int)command.ExecuteScalar();
                            connection.Close();
                            Console.WriteLine("Total Employees are " + empCount);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invaild choice"); break;
                        }
                }
                Console.WriteLine("Want to repeat");
                choice = Console.ReadLine();
            }

                    // Step 2
                    //SqlConnection connection = new SqlConnection();
                    //connection.ConnectionString = "data source=ANAMIKA\\SQLSERVER;initial catalog=EmpDb;integrated security=true";
                    //// Step 3

                    //SqlCommand command = new SqlCommand();
                    //command.CommandText = "Select * from Employee";
                    //command.Connection= connection;

                    // Step 3


                }
            }
}