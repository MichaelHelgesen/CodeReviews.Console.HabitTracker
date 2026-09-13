    namespace habitTracker.MichaelHelgesen.Models;

    using Microsoft.Data.Sqlite;


    internal class HabitRepository
    {
        private static SqliteConnection Connection()
        {
            var connection = new SqliteConnection("Data Source=hello.db");
            return connection;
        }
        
        internal static void CreateTable()
        {
            using var connection = Connection();
            connection.Open();
            var createTableCmd = connection.CreateCommand();
            createTableCmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS brukere (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                navn TEXT NOT NULL UNIQUE
            );";
            createTableCmd.ExecuteNonQuery();
        }

        internal static void CreateUser(string name, int ID)
        {
            using var connection = Connection();
            using var test = connection.CreateCommand();
            test.CommandText = $"""
                INSERT INTO brukere (navn, id)
                VALUES ('{name}', {ID}); // Må være unike her.
            """;
            test.ExecuteNonQuery();
        }

        internal static void CheckForUsers(int ID)
        {
            using var connection = Connection();
            using var command = connection.CreateCommand();
            command.CommandText = """
                SELECT navn
                FROM brukere
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