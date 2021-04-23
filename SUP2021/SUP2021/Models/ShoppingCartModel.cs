using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace SUP2021.Models
{
    public class ShoppingCartModel
    {

        [PrimaryKey, AutoIncrement]
        public Guid ShoppingId { get; set; }
        [ForeignKey(typeof(User))]
        [OneToOne]
        public List<User> user { get; set; }//user

        public int UID { get; set; }//UID




        [ForeignKey(typeof(Products))]
        public Guid ProductID { get; set; }
        public ShoppingCartModel() { }
    }

}