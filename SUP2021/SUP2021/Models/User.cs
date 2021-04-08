using System;
using SQLite;
namespace SUP2021.Models
{
    public class User
    {
       
       
            [PrimaryKey, AutoIncrement]
        public int UID { get; set; }
        public string adress { get; set; }
        public string Username { get; set; }
        public string firstname { get; set; }
        public string sername { get; set; }
        public string password { get; set; }
        public string nummber { get; set; }
        public string email { get; set; }
        public string URL { get; set; }
        public User() { }

    }
    }

