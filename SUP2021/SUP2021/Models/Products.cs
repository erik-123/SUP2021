using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SUP2021.Models
{
   public class Products
    {
        [PrimaryKey, AutoIncrement]
        public int PID { get; set; }
        public string Price { get; set; }
        public string ProductName { get; set; }
        public string URL { get; set; }
        public Products() { }

    }
}
