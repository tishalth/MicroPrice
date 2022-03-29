using Check_CarPrice.Model;
using SQLite;


namespace Check_CarPrice.Persistence
{
    public interface ISQLiteDb
    {
         SQLiteAsyncConnection GetConnection();
    
    }
}
