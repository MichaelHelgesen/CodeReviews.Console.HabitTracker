    namespace habitTracker.MichaelHelgesen.Models;

    using Microsoft.Data.Sqlite;


    internal class HabitRepository
    {
        private static SqliteConnection Connection()
        {
            var connection = new SqliteConnection("Data Source=habits.db");
            return connection;
        }

        internal static void CreateTable()
        {
            using var connection = Connection();
            connection.Open();
            var createTableCmd = connection.CreateCommand();
            createTableCmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS habits (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                habitNormalized TEXT NOT NULL,
                habitOriginal TEXT NOT NULL,
                date TEXT NOT NULL
            );";
            createTableCmd.ExecuteNonQuery();
        }

        internal static void CreateHabit(string habitNormalized, string habitOriginal, string date)
        {
            using var connection = Connection();
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = """
                INSERT INTO habits (habitNormalized, habitOriginal, date)
                VALUES ($habitNormalized, $habitOriginal, $date);
            """;
            command.Parameters.AddWithValue("$habitNormalized", habitNormalized);
            command.Parameters.AddWithValue("$habitOriginal", habitOriginal);
            command.Parameters.AddWithValue("$date", date);
            command.ExecuteNonQuery();
        }

        internal static void CheckForUsers(int ID)
        {
            using var connection = Connection();
            using var command = connection.CreateCommand();
            command.CommandText = """
                SELECT habit
                FROM habits
                WHERE id = $id
            """;
            command.Parameters.AddWithValue("$id", ID);
            Execute(connection, command);
        }

        internal static void Execute(SqliteConnection connection, SqliteCommand command)
        {
            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var name = reader.GetString(0);
                Console.WriteLine($"Hello, {name}!");
            }
        }
    }