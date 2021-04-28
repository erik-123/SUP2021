using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SUP2021.Models
{
    public class WishListModel
    {
        [PrimaryKey, AutoIncrement]
        public Guid WishId { get; set; }
        [ForeignKey(typeof(User))]
        [OneToMany]
        public List<User> user { get; set; }

        public int UID { get; set; }

        //UID




        [ForeignKey(typeof(Products))]
        public Guid ReferProductID { get; set; }
        public WishListModel() { }
    }
}
