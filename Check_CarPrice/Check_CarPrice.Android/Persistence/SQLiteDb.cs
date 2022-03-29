using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using Check_CarPrice.Droid;
using Check_CarPrice.Droid.Persistence;
using Check_CarPrice.Persistence;
using Check_CarPrice.Model;

[assembly:Dependency(typeof(SQLiteDb))]

namespace Check_CarPrice.Droid.Persistence
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "MySQLite.db3");

            return new SQLiteAsyncConnection(path);
        }

      
    }
}