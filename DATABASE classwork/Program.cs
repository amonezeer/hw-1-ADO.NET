using System;
using System.Data.SqlClient;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // зробив з аналогу с++ , розповiм на парi 
        Console.OutputEncoding = System.Text.Encoding.UTF8;

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

                // UPD : список який зберiгае всi продукти
                List<(string Name, string Type, string Color, int Calories)> products = new List<(string, string, string, int)>();

                string queryAll = "SELECT Name, Type, Color, Calories FROM Products";
                using (SqlCommand command = new SqlCommand(queryAll, connection))
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

                        products.Add((name, type, color, calories));
                        Console.WriteLine($" {name} ({type}) - Колір: {color}, Калорійність: {calories} ккал");
                    }
                    Console.WriteLine("|----------------------------------------------------------|");
                }

                // UPD : Завдання N3 (Показати мінімальну калорійність, Показати кількість овочів. Показати овочі та фрукти з калорійністю нижче вказаної)

                int minCalories = products.Count > 0 ? products.Min(p => p.Calories) : 0;
                Console.WriteLine($"\nМінімальна калорійність: {minCalories} ккал");

                int vegetablesCount = products.Count(p => p.Type.ToLower() == "овоч");
                Console.WriteLine($"Кількість овочів: {vegetablesCount}");

                int fruitCount = products.Count(p => p.Type.ToLower() == "фрукт");
                Console.WriteLine($"Кількість фруктiв: {fruitCount}");

                // вибiр для користувача ( вище / нижче )
                Console.WriteLine("\nВиберіть тип фільтрації:");
                Console.WriteLine("1. Показати продукти з калорійністю нижче вказаної");
                Console.WriteLine("2. Показати продукти з калорійністю вище вказаної");
                Console.Write("Ваш вибір (1 або 2): ");

                string filterChoice = Console.ReadLine();

                Console.WriteLine("\nВведіть значення калорійності для фільтрації:");
                int userCalories;
                while (!int.TryParse(Console.ReadLine(), out userCalories))
                {
                    Console.WriteLine("Будь ласка, введіть коректне число:");
                }

                Console.WriteLine(filterChoice == "1"
                    ? $"\nПродукти з калорійністю нижче {userCalories} ккал:"
                    : $"\nПродукти з калорійністю вище {userCalories} ккал:");
                Console.WriteLine("|----------------------------------------------------------|");

                var filteredProducts = filterChoice == "1"
                    ? products.Where(p => p.Calories < userCalories)
                    : products.Where(p => p.Calories > userCalories);

                foreach (var product in filteredProducts)
                {
                    Console.WriteLine($" {product.Name} ({product.Type}) - Колір: {product.Color}, Калорійність: {product.Calories} ккал");
                }
                Console.WriteLine("|----------------------------------------------------------|");

            }
            catch (Exception ex)
            {
                Console.WriteLine(" Помилка: " + ex.Message);
            }
        }

        Console.WriteLine("\nНатисніть Enter для виходу...");
        Console.ReadLine();
    }
}
// посилання на бд ( без вiрусiв ( надiюсь xddd ) ) --> https://prnt.sc/D8XmNg2XMJKw
// UPD : посилання на бд ( додав ще продукти ) --> https://prnt.sc/2CE0iexJyE03