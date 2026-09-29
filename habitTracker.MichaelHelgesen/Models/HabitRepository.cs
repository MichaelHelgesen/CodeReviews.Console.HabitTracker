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
            var createHabitTableCmd = connection.CreateCommand();
            var createHabitLogTableCmd = connection.CreateCommand();
            createHabitTableCmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS habits (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                habitNormalized TEXT NOT NULL,
                habitOriginal TEXT NOT NULL
            );";
            createHabitLogTableCmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS habitlog (
                date TEXT NOT NULL,
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                habit_id INTEGER NOT NULL,
                FOREIGN KEY (habit_id) REFERENCES habits(id)
            );";
            createHabitTableCmd.ExecuteNonQuery();
            createHabitLogTableCmd.ExecuteNonQuery();
        }

        internal static void CreateHabit(string habitNormalized, string habitOriginal)
        {
            using var connection = Connection();
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = """
                INSERT INTO habits (habitNormalized, habitOriginal)
                VALUES ($habitNormalized, $habitOriginal);
            """;
            command.Parameters.AddWithValue("$habitNormalized", habitNormalized);
            command.Parameters.AddWithValue("$habitOriginal", habitOriginal);
            command.ExecuteNonQuery();
        }

        internal static void CreateHabitLog(string date, int habit_id)
        {
            using var connection = Connection();
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = """
                INSERT INTO habitLog (date, habit_id)
                VALUES ($date, $habit_id);
            """;
            command.Parameters.AddWithValue("$date", date);
            command.Parameters.AddWithValue("$habit_id", habit_id);
            
            command.ExecuteNonQuery();
        }

        internal static void DeleteHabit(int id)
        {
            using var connection = Connection();
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = """

            """;
        }

        internal static void GetHabitByID(int ID)
        {
            using var connection = Connection();
            using var command = connection.CreateCommand();
            command.CommandText = """
                SELECT habitNormalized
                FROM habits
                WHERE id = $id
            """;
            command.Parameters.AddWithValue("$id", ID);
            Execute(connection, command);
        }

        internal static List<string> GetUniqueHabits()
        {
            var HabitList = new List<string>();
            using var connection = Connection();
            using var command = connection.CreateCommand();

            command.CommandText = """
                SELECT DISTINCT habitNormalized
                FROM habits
            """;
            
            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var title = reader.GetString(0);
                HabitList.Add(title);
            }
            return HabitList;
        }

        internal static List<HabitLog> GetHabitLog(int habitId)
        {
            var HabitList = new List<HabitLog>();
            using var connection = Connection();
            using var command = connection.CreateCommand();

            command.CommandText = """
                SELECT date, id, habit_id
                FROM habitLog
                WHERE habit_id = $id
            """;
            command.Parameters.AddWithValue("$id", habitId);
            
            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var HabitLog = new HabitLog
                {
                  DateTimeNow =  reader.GetString(0),
                  ID = reader.GetInt32(1),
                  HabitID = reader.GetInt32(2)
                };
                HabitList.Add(HabitLog);
            }
            return HabitList;
        }

        internal static void Execute(SqliteConnection connection, SqliteCommand command)
        {
            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var habit = reader.GetString(0);
                Console.WriteLine($"Hello, {habit}!");
            }
        }
    }