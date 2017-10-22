using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBookApp.Models
{
    public class SubCategory
    {
        public string Name { get; set; }

        public Category Category { get; set; }
    }
}
