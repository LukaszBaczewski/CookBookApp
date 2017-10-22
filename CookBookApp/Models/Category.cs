using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBookApp.Models
{
    public class Category
    {
        public Category() { }        
        
        public string Name { get; set; }

        public ICollection<SubCategory> SubCategories
        {
            get;
            set;
        }
    }
}
