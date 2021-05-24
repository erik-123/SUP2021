using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SUP2021.Models
{
  public class ShippingModel
    {
        [PrimaryKey, AutoIncrement]
        public Guid ShippingId { get; set; }
        
        public string Postalcode { get; set; }
        public string City { get; set; }
        public string Address { get; set; }


        [ForeignKey(typeof(User))]
        public int UID { get; set; }
        public ShippingModel() { }

    }
}
