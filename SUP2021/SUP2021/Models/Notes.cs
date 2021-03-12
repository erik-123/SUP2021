using System;
using SQLite;

namespace SUP2021.Models
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; } //ändra med nya 

    }
}