using System.Threading.Tasks;

namespace CarCare
{
    public interface IDataBaseService
    {
        Task<object> FetchFromTable(string table);

        Task SaveToDatabase(object item, object UpdateList = null);
    }
}