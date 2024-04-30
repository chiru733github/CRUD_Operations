using System;
using System.Data.SqlClient;

namespace CRUD_Operations
{
    public class CRUD_Program
    {
        static SqlConnection? sqlconn;
        public static void Main(string[] args)
        {
            string connecting = @"Data Source=LAPTOP-SBDVLFF5\SQLEXPRESS;
                                  Initial Catalog=Market;Integrated Security=True";
            sqlconn = new SqlConnection(connecting);
            sqlconn.Open();
            Console.WriteLine("Enter the character to operation");
            Console.WriteLine("C => insert the data.\nR => read the data." +
                "\nU => update the data.\nD => delete the data.");
            char c = Convert.ToChar(Console.ReadLine().ToUpper());
            switch(c)
            {
                case 'C': InsertIntoDatabase();
                    break;
                case 'R': DisplayDetailsOfDatabase();
                    break;
                case 'U': UpdateNameByUsingId();
                    break;
                case 'D': DeleteByUsingName();
                    break;
                default:
                    Console.WriteLine("Invaild character try again");
                    break;
            }
            sqlconn.Close();
            Console.ReadKey();
        }
        public static void DeleteByUsingName() 
        {
            try
            {
                //Delete Operation
                Console.WriteLine("Enter the choice");
                Console.WriteLine("1) delete by using id\n2) delete by using Name" +
                                   "\n3) delete by using ItemName");
                string deleteQuery = "";
                int choice = Convert.ToInt32(Console.ReadLine());
                switch(choice)
                {
                    case 1:
                        {
                            Console.WriteLine("Enter the Id");
                            int Did = Convert.ToInt32(Console.ReadLine());
                            deleteQuery = "delete from Customer where Id=" + Did;
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Enter the Name");
                            string? DName = Console.ReadLine();
                            deleteQuery = "delete from Customer where CustomerName ='" + DName + "'";
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Enter the itemName");
                            string? DitemName = Console.ReadLine();
                            deleteQuery = $"delete from Customer where ItemName='{DitemName}'";
                            break;
                        }
                }
                
                SqlCommand deleteCommand = new SqlCommand(deleteQuery, sqlconn);
                deleteCommand.ExecuteNonQuery();
                Console.WriteLine("Delete Successfully");
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void UpdateNameByUsingId()
        {
            try
            {
                //Update Operation
                Console.WriteLine("Enter id that you would like to update");
                int U_id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the name to update");
                string? Name = Console.ReadLine();
                string updateQuery = $"update Customer set CustomerName = '{Name}' where id={U_id}";
                SqlCommand UpdateCommand = new SqlCommand(updateQuery,sqlconn);
                UpdateCommand.ExecuteNonQuery();
                Console.WriteLine("Update Successfully");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void DisplayDetailsOfDatabase()
        {
            try
            {
                //Read operation
                string displayQuery = "select * from Customer";
                SqlCommand displayCommand = new SqlCommand(displayQuery,sqlconn);
                SqlDataReader Datareader = displayCommand.ExecuteReader();
                while (Datareader.Read())
                {
                    Console.WriteLine("Id: "+Datareader.GetValue(0));
                    Console.WriteLine("CustomerName: "+Datareader.GetValue(1));
                    Console.WriteLine("ItemName: "+Datareader.GetValue(2));
                    Console.WriteLine("Price: "+Datareader.GetValue(3));
                }
                Datareader.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private static void InsertIntoDatabase()
        {
            try
            {
                //create as CRUD operation(Insertion)
                Console.WriteLine("Enter Id");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter name");
                string? Name = Console.ReadLine();
                Console.WriteLine("Enter itemname");
                string? itemName = Console.ReadLine();
                Console.WriteLine("Enter the price");
                int Price = Convert.ToInt32(Console.ReadLine());
                string InsertQuery = "insert into Customer values(" + id + ",'" + Name + "','" + itemName + "'," + Price + ")";
                SqlCommand InsertCommand = new SqlCommand(InsertQuery, sqlconn);
                InsertCommand.ExecuteNonQuery();
                Console.WriteLine("Insertion successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}