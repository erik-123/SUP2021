using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using SUP2021.Models;
using System.IO;
using System;
using System.Reflection;


namespace SUP2021.Data
{
    public class NoteDatabase
    {
        readonly SQLiteAsyncConnection database;
        static SQLiteAsyncConnection Database;
        public NoteDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Note>().Wait();
            database.CreateTableAsync<User>().Wait();


            string Databasepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Test.db");

            Assembly assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            Stream embeddedDatabaseStream = assembly.GetManifestResourceStream("SUP2021.Test.db");

            //if (!File.Exists(Databasepath))
            //{
            //    FileStream fileStreamToWrite = File.Create(Databasepath);
            //    embeddedDatabaseStream.Seek(0, SeekOrigin.Begin);
            //    embeddedDatabaseStream.CopyTo(fileStreamToWrite); 
            //    fileStreamToWrite.Close();
            //}
            Database = new SQLiteAsyncConnection(Databasepath);
            Database.CreateTableAsync<User>().Wait();
        }

        public Task<int> AddUser(User user)
       //public Task<int> AddUser(User user)
        {
            //Database.SaveChangesAsync();
            return database.InsertAsync(user);
           // database.InsertAsync(user); 

            //return "success";
            // database.InsertAsync(user);
            
        }

        //public async static Task<List<User>> GetAllUsersAsync()
        //{
        //    MyDatabaseEntities db = new MyDatabaseEntities();
        //    var query = from item in db.Students
        //                select item;
        //    return await query.ToListAsync();
        //}


        public Task<List<User>> GetUsersAsync()
        {
            //Get all notes.
            return Database.Table<User>().ToListAsync();
        }


        public Task<List<Note>> GetNotesAsync()
        {
            //Get all notes.
            return database.Table<Note>().ToListAsync();
        }

        public Task<Note> GetNoteAsync(int id)
        {
            // Get a specific note.
            return database.Table<Note>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteAsync(Note note)
        {
            if (note.ID != 0)
            {
                // Update an existing note.
                return database.UpdateAsync(note);
            }
            else
            {
                // Save a new note.
                return database.InsertAsync(note);
            }
        }

        public Task<int> DeleteNoteAsync(Note note)
        {
            // Delete a note.
            return database.DeleteAsync(note);
        }
    }
}
