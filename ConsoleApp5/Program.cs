using System;
using MySql.Data.MySqlClient;
public class Program
{
    public static void Main(string[] args)
    {
        MyDatabaseClass db = new MyDatabaseClass();
        try
        {
            db.ConnectToDatabase();
            // Пример запроса:
            db.ExecuteQuery("SELECT * FROM username"); // Замените 'your_table' на имя вашей таблицы
            Console.WriteLine("Запрос выполнен успешно."); db.CloseConnection();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }
}
public class MyDatabaseClass
{
    private MySqlConnection connection;
    private string connectionString;
    public MyDatabaseClass()
    {
          connectionString = "server=sql12.freesqldatabase.com;user=sql12746670;password=GVUgGjLkrB;database=sql12746670";
    }
    public void ConnectToDatabase()
    {
        connection = new MySqlConnection(connectionString); try
        {
            connection.Open();
            Console.WriteLine("Подключение установлено.");
        }
        catch (MySqlException ex)
        {
            Console.WriteLine($"Ошибка подключения: {ex.Message}"); throw; // Передаём исключение дальше, чтобы его можно было обработать в Main
        }
    }
    public void ExecuteQuery(string query)
    {
        if (connection == null || connection.State != System.Data.ConnectionState.Open)
        {
            ConnectToDatabase();
        }
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            try
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Обработка данных из reader (зависит от структуры вашей таблицы)
                        Console.WriteLine($"Столбец 1: {reader.GetString(0)}"); // Пример, измените индексы и типы данных по необходимости
                        Console.WriteLine($"Столбец 2: {reader.GetString(1)}");
                        Console.WriteLine($"Столбец 3: {reader.GetString(2)}");
                        Console.WriteLine($"Столбец 4: {reader.GetString(3)}");
                        Console.WriteLine($"Столбец 5: {reader.GetString(4)}");// ... обработка других столбцов ...
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Ошибка выполнения запроса: {ex.Message}");
                throw; // Передаём исключение дальше, чтобы его можно было обработать в Main            }
            }
        }
    }
    public void CloseConnection()
    {
        if (connection != null && connection.State == System.Data.ConnectionState.Open)
        {
            connection.Close();
            Console.WriteLine("Подключение закрыто.");
        }
    }
}
