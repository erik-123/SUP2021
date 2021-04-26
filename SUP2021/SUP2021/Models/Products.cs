using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace SUP2021.Models
{
   public class Products
    {
        [PrimaryKey, AutoIncrement]
        public Guid ProductId { get; set; }       

        [ForeignKey(typeof(Products))]
        public int RefUserID { get; set; }

        public string Price { get; set; }
        public string ProductName { get; set; }
        public string URL { get; set; }
        public Products() { }

    }
}
