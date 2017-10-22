using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBookApp.Models
{
    public class Product
    {
        public Product() { }
        private string _name;
        public string Name
            {
                get
                {
                    return _name;
                }
                set
                {
                    if (_name != value)
                    {
                        _name = value;
                        
                    }
                }
            }

        private double? _quantity;
        public double? Quantity
            {
                get
                {
                    return _quantity;
                }
                set
                {
                    if (_quantity != value)
                    {
                        _quantity = value;
                        
                    }
                }
            }

        private string _measure;
        public string Measure
        {
            get
            {
                return _measure;
            }
            set
            {
                _measure = value;
               
            }
        }

        private string _prepNotes;
        public string PrepNotes
        {
            get
            {
                return _prepNotes;
            }
            set
            {
                _prepNotes = value;
                
            }
        }
    }
}

