using System;
using System.Collections.Generic;
using SQLite;

namespace SUP2021.Models
{
    public class CategoryModel
    {
        [PrimaryKey, AutoIncrement]
        public Guid CategoryId { get; set; }

       

        public string CategoryName { get; set; }


        public CategoryModel()
        {
            this.CategoryId = Guid.NewGuid();
          

         
             
    
        }
    }
}
