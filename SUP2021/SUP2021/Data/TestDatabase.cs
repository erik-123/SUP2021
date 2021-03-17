using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.IO;
using System.Reflection;
using SUP2021.Models;
using System.Threading.Tasks;

namespace SUP2021.Data
{
   public class TestDatabase
    {
        
            static SQLiteAsyncConnection Database;
            public TestDatabase()
            {
               string Databasepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TestDatabase.db");

                Assembly assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
                Stream embeddedDatabaseStream = assembly.GetManifestResourceStream("SUP2021.TestDatabase.db");

                if (!File.Exists(Databasepath))
                {
                    FileStream fileStreamToWrite = File.Create(Databasepath);
                    embeddedDatabaseStream.Seek(0, SeekOrigin.Begin);
                    embeddedDatabaseStream.CopyTo(fileStreamToWrite);
                    fileStreamToWrite.Close();
                }
                Database = new SQLiteAsyncConnection(Databasepath);
                Database.CreateTableAsync<Item>().Wait();
            }

            public Task<List<Item>> GetUsersAsync()
            {
                //Get all notes.
                return Database.Table<Item>().ToListAsync();
            }

    }
}
