using SQLite;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DataBase
{
    class DataBaseConnection
    {
        public static async Task<SQLiteAsyncConnection> GetConnetion<T>() where T : new()
        {
            var dataBasePath = Path.Combine(FileSystem.AppDataDirectory, "DiagnosticApp.db");

            SQLiteAsyncConnection connection = new SQLiteAsyncConnection(dataBasePath);

            await connection.CreateTableAsync<T>();

            return connection;
        }
    }
}
