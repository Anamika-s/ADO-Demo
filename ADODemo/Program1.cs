// Step 1
using ADODemo.Models;
using System.Data.SqlClient;

namespace ADODemo
{
    internal class Program1
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
                            GetEmployees();
                            break;
                        }
                    case 2:
                        {
                            //Console.WriteLine("Enter ID");
                            //byte id = Byte.Parse(Console.ReadLine());
                            Console.WriteLine("Enter NAme");
                            string name = Console.ReadLine();
                            Console.WriteLine("Enter Address");
                            string address = Console.ReadLine();
                            Console.WriteLine("Enter DAte of Joining");
                            DateOnly doj = DateOnly.Parse(Console.ReadLine());
                            Console.WriteLine("Enter Salary");
                            int salary = int.Parse(Console.ReadLine());
                            Employee employee = new Employee()
                            { 
                                Name = name,
                                Address = address,
                                Salary = salary,
                                Doj = doj
                            };
                            //AddRecord(id, name, address, doj, salary);
                            AddRecord(employee);
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Enter ID for which to edit recotrd");
                            byte id = Byte.Parse(Console.ReadLine());
                            
                            Console.WriteLine("Enter Address");
                            string address = Console.ReadLine();
                            
                            Console.WriteLine("Enter Salary");
                            int salary = int.Parse(Console.ReadLine());
                            EditEmployee(id, address, salary);
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("Enter ID for which to delete recotrd");
                            byte id = Byte.Parse(Console.ReadLine());

                            DeleteEmployee(id);
                            break;
                        }

                    case 5:
                        {
                            Console.WriteLine("Enter ID for which to search recotrd");
                            byte id = Byte.Parse(Console.ReadLine());

                            GetEmployeeByID(id);
                            break;
                        }
                    case 6:
                        {
                            connection = GetConnection();
                            command = new SqlCommand("Select count(*) from Employee", connection);
                            connection.Open();
                            int empCount = (int)command.ExecuteScalar();
                            connection.Close();
                            command.Dispose();
                            connection.Dispose();
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

        private static void EditEmployee(int id, string address, int salary)
        {
            connection = GetConnection();
            command = new SqlCommand($"update Employee set address= '{address}', salary ={salary}  where id= {id}", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            command.Dispose();
            connection.Dispose();
        }

        private static void DeleteEmployee(int id)
        {
            using (connection = GetConnection())
            {
                using (command = new SqlCommand($"delete Employee where id={id}", connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                } 
            }
            }

            private static void GetEmployeeByID(int id)
        {
            using (connection = GetConnection())
            {
                using (command = new SqlCommand($"select * from Employee where id={id}", connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            Console.WriteLine(reader[0] + " " + reader["name"]); ;


                        }
                        else
                            Console.WriteLine("There are no recirds");
                        connection.Close();
                    }
                }
            }
        }

        private static void GetEmployees()
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
                Console.WriteLine("There are no records");
            reader.Close();
            connection.Close();
            reader.DisposeAsync();
            command.Dispose();
            connection.Dispose();
        }

        //private static void AddRecord(int id, string name, string address, DateOnly doj, int salary)
        private static void AddRecord(Employee employee)

        {
            connection = GetConnection();
            command = new SqlCommand($"insert into Employee values('{employee.Name}','{employee.Address}' ,'{employee.Doj}', {employee.Salary})", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}