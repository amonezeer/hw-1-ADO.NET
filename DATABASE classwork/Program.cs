using System;
using System.Data.SqlClient;
using System.Threading;

class Program
{
    static void Main()
    {
        // зробив з аналогу с++ , розповiм на парi 
        Console.OutputEncoding = System.Text.Encoding.GetEncoding("windows-1251"); 
        Console.InputEncoding = System.Text.Encoding.GetEncoding("windows-1251"); 

        //-----------------------------------------------------------------------------------------------------------//
        string connectionString = "Server=DESKTOP-OBRUJ32;Database=FruitsAndVegetables;Integrated Security=True;";

        Console.WriteLine("Натисніть Enter для підключення до бази даних...");
        Console.ReadLine();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine(" [Підключення успішне!]\n");
                Thread.Sleep(1000);
                Console.Clear();

                string query = "SELECT Name, Type, Color, Calories FROM Products";
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine(" Список овочів та фруктів:");
                    Console.WriteLine("|----------------------------------------------------------|");

                    while (reader.Read())
                    {
                        string name = reader.GetString(0);
                        string type = reader.GetString(1);
                        string color = reader.GetString(2);
                        int calories = reader.GetInt32(3);

                        Console.WriteLine($" {name} ({type}) - Колір: {color}, Калорійність: {calories} ккал");
                        Console.WriteLine("|----------------------------------------------------------|");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Помилка підключення: " + ex.Message);
            }
        }

        Console.WriteLine("\nНатисніть Enter для виходу...");
        Console.ReadLine();
    }
}
// посилання на бд ( без вiрусiв ( надiюсь xddd ) ) --> https://prnt.sc/D8XmNg2XMJKw