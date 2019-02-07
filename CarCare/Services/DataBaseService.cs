using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

namespace CarCare
{
    public class DataBaseService : IDataBaseService
    {
        private readonly SQLiteAsyncConnection GetDBConnection;

        public DataBaseService()
        {
            GetDBConnection = App.DatabaseConnection;
            CreateTables();
        }

        public void CreateTables()
        {
            GetDBConnection.CreateTableAsync<User>().Wait();
            GetDBConnection.CreateTableAsync<Project>().Wait();
            GetDBConnection.CreateTableAsync<Photo>().Wait();
            GetDBConnection.CreateTableAsync<TimeLog>().Wait();
        }

        public async Task<object> FetchFromTable(string table)
        {
            if (table.Equals("ProjectDetail"))
            {
                var projectList = await GetDBConnection.GetAllWithChildrenAsync<Project>();
                return projectList;
            }
            else
            {
                var userList = await GetDBConnection.GetAllWithChildrenAsync<User>();
                return userList;
            }
        }

        public async Task SaveToDatabase(object item, object UpdateList = null)
        {
            if (item is Project)
            {
                await GetDBConnection.InsertOrReplaceWithChildrenAsync(item as Project, true);
            }
            else
            {
                await GetDBConnection.InsertOrReplaceWithChildrenAsync(item as User, true);
            }
        }
    }
}